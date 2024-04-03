using Tetrix.Cli.UI;
using Tetrix.Cli.UI.Text;

namespace Tetrix.Cli;

public class PlayfieldRenderer
{

	public int X { get; set; }

	public int Y { get; set; }

	private readonly Scoreboard _scoreboard;
	private readonly Playfield _playfield;
	private readonly IRenderer _renderer;

	public PlayfieldRenderer(int x, int y, IRenderer renderer, Playfield playfield, Scoreboard sb)
	{
		X = x;
		Y = y;
		_scoreboard = sb;
		_renderer = renderer;
		_playfield = playfield;
		_playfield.PlayfieldGridChanging += (_, e) => { var m = new GridMutation(); m.AddSources(e.Select(b => new Point(b.X, b.Y))); _renderer.Render(m); };
		_playfield.PlayfieldGridChanged += (_, e) => { var m = new GridMutation(); m.AddTargets(e.Select(b => new DrawablePoint(b.X, b.Y, '#'))); _renderer.Render(m); };
	}

	// Renders the entire screen
	public void Render()
	{
		// Top border line
		var points = new List<DrawablePoint>
		{
			new(X, Y, '\u2554'),
			new(X + _playfield.W + 1, Y, '\u2557')
		};

		for (int x = 1; x < _playfield.W + 1; x++)
		{
			int xPoint = X + x;
			if (xPoint == 1)
				points.Add(new(xPoint, Y, '\u255D'));
			else if (xPoint == _playfield.W)
				points.Add(new(xPoint, Y, '\u255A'));
			else
				points.Add(new(xPoint, Y, ' '));
		}

		// For each row
		for (int y = Y + 1; y <= _playfield.H + Y; y++)
		{
			// Left border line
			points.Add(new(X, y, '\u2551'));

			// Right border line
			points.Add(new(X + _playfield.W + 1, y, '\u2551'));
		}

		// Bottom border line
		points.Add(new(X, Y + _playfield.H + 1, '\u255A'));
		for (int x = X + 1; x <= _playfield.W + X + 1; x++)
			points.Add(new(x, Y + _playfield.H + 1, '\u2550'));
		points.Add(new(X + _playfield.W + 1, Y + _playfield.H + 1, '\u255D'));


		// Generate single mutation for blocks and playfield borders
		var mutation = new GridMutation();
		foreach (Block b in _playfield.GetBlocks())
			mutation.AddTarget(new DrawablePoint(b.X, b.Y, '#'));

		// Add playfield border points
		foreach (DrawablePoint p in points)
			mutation.AddTarget(p);

		_renderer.Render(mutation);

		_renderer.WriteText(17, 4, $"Score: {_scoreboard.GetScore()}");
	}
}
