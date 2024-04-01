using Tetrix.Cli.UI;
using Tetrix.Cli.UI.Text;

namespace Tetrix.Cli;

public class PlayfieldRenderer(int x, int y, IRenderer renderer, Playfield playfield)
{

	public int X { get; set; } = x;

	public int Y { get; set; } = y;

	private readonly Playfield _playfield = playfield;
	private readonly IRenderer _renderer = renderer;

	// List of all block from all tetrominoes
	private List<Block> _blocks = [];

	private readonly Random _randomizer = new();

	// Progresses the game - 1 move.
	public bool Progress()
	{
		if (_playfield.CurrentTetro == null)
			_playfield.ResetCurrentTetro(_randomizer.Next(7));

		if (!_playfield.CurrentTetro.CanMoveDown())
		{
			// Checks for full rows and removes if Any
			_playfield.RemoveFullRowsIfAny();
			// Reset the current tetromino
			_playfield.ResetCurrentTetro(_randomizer.Next(7));
			// If the new tetro cannot move done - game over
			if (!_playfield.CurrentTetro.CanMoveDown())
			{
				_renderer.WriteText(17, 13, "Game Over");
				_renderer.WriteText(17, 15, "[press a key to go to menu]");
				return false;
			}
			return true;
		}

		MoveDown();
		return true;
	}

	public void Rotate()
	{
		_playfield.CurrentTetro.BeginMutation();
		_playfield.CurrentTetro.Rotate();
		_renderer.Render(_playfield.CurrentTetro.EndMutation());
	}


	public void MoveLeft()
	{
		if (!_playfield.CurrentTetro.CanMoveLeft())
			return;

		_playfield.CurrentTetro.BeginMutation();
		_playfield.CurrentTetro.MoveLeft();
		_renderer.Render(_playfield.CurrentTetro.EndMutation());
	}

	public void MoveRight()
	{
		if (!_playfield.CurrentTetro.CanMoveRight())
			return;

		_playfield.CurrentTetro.BeginMutation();
		_playfield.CurrentTetro.MoveRight();
		_renderer.Render(_playfield.CurrentTetro.EndMutation());
	}

	public void MoveDown()
	{
		if (!_playfield.CurrentTetro.CanMoveDown())
			return;

		_playfield.CurrentTetro.BeginMutation();
		_playfield.CurrentTetro.MoveDown();
		_renderer.Render(_playfield.CurrentTetro.EndMutation());
	}

	public void RemoveFullRowsIfAny()
	{
		// Check for full rows
		var rowsToRemove = new List<int>();
		for (int y = Y; y <= _playfield.H + Y; y++)
		{
			var row = _blocks.Where(b => b.Y == y);
			int count = row.Count();
			if (count == _playfield.W)
				rowsToRemove.Add(y);
		}

		var mutation = new GridMutation();
		// If full rows remove the blocks
		foreach (int row in rowsToRemove)
		{
			// Select blocks to remove
			var blocksToRemove = new List<Block>();
			foreach (Block b in _blocks.Where(b => b.Y == row))
				blocksToRemove.Add(b);

			// Remove blocks
			foreach (Block b in blocksToRemove)
			{
				_blocks.Remove(b);
				mutation.AddSource(b);
			}

			// Shift upper blocks down
			foreach (Block b in _blocks.Where(_b => _b.Y < row))
			{
				mutation.AddSource(new DrawablePoint(b.X, b.Y, ' '));
				++b.Y;
				mutation.AddTarget(b);
			}

		}
		_renderer.Render(mutation);
	}

	// Check if a single block location (x, y) is available/empty 
	public bool IsLocationAvailable(int x, int y)
	{
		// check out of boundries
		if (x >= _playfield.W + X + 1 || x < X + 1 || y >= _playfield.H + Y + 1)
			return false;

		// check block colisions
		foreach (Block b in _blocks)
			if (!_playfield.CurrentTetro.Blocks.Any(_b => _b == b) && b.X == x && b.Y == y)
				return false;

		return true;
	}

	// Returns true if ALL locations are available
	public bool AreLocationAvailale(params (int, int)[] locations)
	{
		foreach ((int x, int y) in locations)
			if (!IsLocationAvailable(x, y))
				return false;

		return true;
	}

	// Renders the entire screen
	public void Render()
	{
		var points = new List<DrawablePoint>
		{
			// Top border line
			new(X, Y, '\u2554'),
			new(X + _playfield.W + 1, Y, '\u2557')
		};

		for (int x = 1; x < _playfield.W + 1; x++)
		{
			int xPoint = X + x;
			int halfW = _playfield.W / 2;
			if (xPoint < (halfW - 2) || xPoint > (halfW + 3))
				points.Add(new(xPoint, Y, '\u2550'));
			else if (xPoint == (halfW - 2))
				points.Add(new(xPoint, Y, '\u255D'));
			else if (xPoint == (halfW + 3))
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
		foreach (var p in _blocks)
			mutation.AddTarget(p);

		// Add playfield border points
		foreach (DrawablePoint p in points)
			mutation.AddTarget(p);

		_renderer.Render(mutation);
	}

	public IEnumerable<Block> GetBlocks()
		=> _blocks.Where(b => !_playfield.CurrentTetro.Blocks.Contains(b));

	public void SetBlocks(IEnumerable<Block> blocks)
		=> _blocks = blocks.ToList();
}
