using Tetrix.GameEngine.Tetroes;
using Tetrix.GameEngine.UI;
using Tetrix.GameEngine.UI.Text;
using UiTextWriter = Tetrix.GameEngine.UI.Text.TextWriter;

namespace Tetrix.GameEngine;

public class Scoreboard(int x, int y, IRenderer renderer)
{
	readonly IRenderer _renderer = renderer;
	public Tetro NextTetro { get; private set; }
	int _score;
	readonly int _x = x;
	readonly int _y = y;

	public int GetScore()
		=> _score;

	public void IncrementScore(int count)
	{
		_score += count;
		RenderScore();
	}

	public void SetNextTetro(Tetro next)
	{
		if (NextTetro != null)
			_renderer.Render(ClearTetro(NextTetro));

		NextTetro = next;
		_renderer.Render(RenderNextTetro(NextTetro));
	}

	public void RenderScore()
		=> _renderer.WriteText(_x, _y + 8, $"score: {_score}");

	private GridMutation RenderNextTetro(Tetro tetro)
	{
		var m = new UiTextWriter().WriteText(_x, _y + 2, "next:");
		foreach (Block b in tetro.Blocks)
			m.AddTarget(new DrawablePoint(b.X + _x + 2, b.Y + 4, b.ForeColor, b.Symbol, b.Debug));

		return m;
	}

	private GridMutation ClearTetro(Tetro tetro)
	{
		var m = new GridMutation();
		foreach (DrawablePoint b in tetro.Blocks)
			m.AddSource(b.X + _x + 2, b.Y + 4);

		return m;
	}
}
