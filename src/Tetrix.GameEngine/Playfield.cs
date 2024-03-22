using System.Collections;
using Tetrix.GameEngine.Tetroes;
using Tetrix.GameEngine.UI;
using Tetrix.GameEngine.UI.Text;

namespace Tetrix.GameEngine;

public class Playfield(int x, int y, IRenderer renderer, TetrisStage stage)
{
	// Height of the playfield
	public int H { get; private set; } = 20;

	// Width of the playfield
	public int W { get; private set; } = 10;

	public int X { get; set; } = x;

	public int Y { get; set; } = y;

	private readonly TetrisStage _stage = stage;

	private readonly IRenderer _renderer = renderer;

	public Tetro CurrentTetro { get; private set; }

	// List of all block from all tetrominoes
	private List<Block> _blocks = [];

	public event EventHandler<RowRemovedEventArgs> RowRemoved;

	protected void OnRowRemoved(RowRemovedEventArgs e)
		=> RowRemoved?.Invoke(this, e);

	// Progresses the game - 1 move.
	public bool Progress()
	{
		if (CurrentTetro == null)
			ResetCurrentTetro();

		if (!CurrentTetro.CanMoveDown())
		{
			// Checks for full rows and removes if Any
			RemoveFullRowsIfAny();
			// Reset the current tetromino
			ResetCurrentTetro();
			// If the new tetro cannot move done - game over
			if (!CurrentTetro.CanMoveDown())
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
		CurrentTetro.BeginMutation();
		CurrentTetro.Rotate();
		_renderer.Render(CurrentTetro.EndMupation());
	}

	public void SetCurrentTetro(Tetro tetro) => CurrentTetro = tetro;

	public void MoveLeft()
	{
		if (!CurrentTetro.CanMoveLeft())
			return;

		CurrentTetro.BeginMutation();
		CurrentTetro.MoveLeft();
		_renderer.Render(CurrentTetro.EndMupation());
	}

	public void MoveRight()
	{
		if (!CurrentTetro.CanMoveRight())
			return;

		CurrentTetro.BeginMutation();
		CurrentTetro.MoveRight();
		_renderer.Render(CurrentTetro.EndMupation());
	}

	public void MoveDown()
	{
		if (!CurrentTetro.CanMoveDown())
			return;

		CurrentTetro.BeginMutation();
		CurrentTetro.MoveDown();
		_renderer.Render(CurrentTetro.EndMupation());
	}

	// Set current tetromino and generate the next one
	protected void ResetCurrentTetro()
	{
		CurrentTetro = _stage.GetNextTetro();

		// Add all blocks from all tetrominoes to single List
		// for faster rendering and colision detection
		foreach (Block b in CurrentTetro.Blocks)
			_blocks.Add(b);
	}

	public void RemoveFullRowsIfAny()
	{
		// Check for full rows
		var rowsToRemove = new List<int>();
		for (int y = Y; y <= H + Y; y++)
		{
			var row = _blocks.Where(b => b.Y == y);
			int count = row.Count();
			if (count == W)
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

		OnRowRemoved(RowRemovedEventArgs.Create(rowsToRemove.Count));
	}

	// Check if a single block location (x, y) is available/empty 
	public bool IsLocationAvailable(int x, int y)
	{
		// check out of boundries
		if (x >= W + X + 1 || x < X + 1 || y >= H + Y + 1)
			return false;

		// check block colisions
		foreach (Block b in _blocks)
			if (!CurrentTetro.Blocks.Any(_b => _b == b) && b.X == x && b.Y == y)
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
			new(X + W + 1, Y, '\u2557')
		};

		for (int x = 1; x < W + 1; x++)
		{
			int xPoint = X + x;
			int halfW = W / 2;
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
		for (int y = Y + 1; y <= H + Y; y++)
		{
			// Left border line
			points.Add(new(X, y, '\u2551'));

			// Right border line
			points.Add(new(X + W + 1, y, '\u2551'));
		}

		// Bottom border line
		points.Add(new(X, Y + H + 1, '\u255A'));
		for (int x = X + 1; x <= W + X + 1; x++)
			points.Add(new(x, Y + H + 1, '\u2550'));
		points.Add(new(X + W + 1, Y + H + 1, '\u255D'));


		// Generate single mutation for blocks and playfield borders
		var mutation = new GridMutation();
		foreach (DrawablePoint p in _blocks)
			mutation.AddTarget(p);

		// Add playfield border points
		foreach (DrawablePoint p in points)
			mutation.AddTarget(p);

		_renderer.Render(mutation);
	}

	public IEnumerable<Block> GetBlocks()
		=> _blocks.Where(b => !CurrentTetro.Blocks.Contains(b));

	public void SetBlocks(IEnumerable<Block> blocks)
		=> _blocks = blocks.ToList();
}
