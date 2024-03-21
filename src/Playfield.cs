using Tetrix.Tetroes;
using Tetrix.UI;
using Tetrix.UI.Text;
using Tetrix.Storage;

namespace Tetrix;

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

	private Tetro _curTetro;

	// List of all block from all tetrominoes
	private List<Block> _blocks = [];

	public event EventHandler<RowRemovedEventArgs> RowRemoved;

	protected void OnRowRemoved(RowRemovedEventArgs e)
		=> RowRemoved?.Invoke(this, e);

	// Progresses the game - 1 move.
	public void Progress(object state)
	{
		if (!keepRuuning)
			return;

		if (_curTetro == null)
			ResetCurrentTetro();

		if (!_curTetro.CanMoveDown())
		{
			// Checks for full rows and removes if Any
			RemoveFullRowsIfAny();
			// Reset the current tetromino
			ResetCurrentTetro();
			// If the new tetro cannot move done - game over
			if (!_curTetro.CanMoveDown())
			{
				keepRuuning = false;
				_renderer.WriteText(17, 13, "Game Over");
				_renderer.WriteText(17, 15, "[press a key to go to menu]");
			}

			return;
		}

		MoveDown();
	}

	public void Rotate()
	{
		_curTetro.BeginMutation();
		_curTetro.Rotate();
		_renderer.Render(_curTetro.EndMupation());
	}

	bool keepRuuning = true;

	public void Start(int speed, InputQueue inputQueue)
	{
		Render();
		using var timer = new Timer(Progress, null, 0, 1100 - (speed * 100));
		while (keepRuuning)
		{
			ConsoleKey input = inputQueue.GetNextInput();
			switch (input)
			{
				case ConsoleKey.Q:
				case ConsoleKey.X: keepRuuning = false; break;
				case ConsoleKey.UpArrow: Rotate(); break;
				case ConsoleKey.LeftArrow: MoveLeft(); break;
				case ConsoleKey.RightArrow: MoveRight(); break;
				case ConsoleKey.DownArrow: MoveDown(); break;
				case ConsoleKey.F5: Render(); break;
				case ConsoleKey.Escape:
					timer.Change(0, Timeout.Infinite);
					var next = new InGameMenu(_renderer, inputQueue).WhatsNext();
					switch (next)
					{
						case MenuOptions.SaveGame:
							JsonRepository.Save(new SavableData()
							{
								NextTetro = _stage.Scoreboard.NextTetro.Type,
								CurrentTetro = _curTetro.Type,
								Blocks = GetBlocks(),
								Score = _stage.Scoreboard.GetScore(),
							});
							// Show game saved message for 2 seconds and quit
							_renderer.WriteText(17, 15, "Game saved!");
							Thread.Sleep(1000);
							_renderer.WriteText(17, 15, "		   ");
							keepRuuning = false;
							break;
						case MenuOptions.ResumeGame:
							_renderer.Clear();
							Render();
							timer.Change(100, 1100 - (speed * 100));
							break;
						case MenuOptions.QuitGame:
							keepRuuning = false;
							break;
						default: break;
					}
					break;
				case ConsoleKey.Enter:
					if (!keepRuuning)
						return;
					break;
			}
		}
	}

	public void SetCurrentTetro(Tetro tetro) => _curTetro = tetro;

	public void MoveLeft()
	{
		if (!_curTetro.CanMoveLeft())
			return;

		_curTetro.BeginMutation();
		_curTetro.MoveLeft();
		_renderer.Render(_curTetro.EndMupation());
	}

	public void MoveRight()
	{
		if (!_curTetro.CanMoveRight())
			return;

		_curTetro.BeginMutation();
		_curTetro.MoveRight();
		_renderer.Render(_curTetro.EndMupation());
	}

	public void MoveDown()
	{
		if (!_curTetro.CanMoveDown())
			return;

		_curTetro.BeginMutation();
		_curTetro.MoveDown();
		_renderer.Render(_curTetro.EndMupation());
	}

	// Set current tetromino and generate the next one
	protected void ResetCurrentTetro()
	{
		_curTetro = _stage.GetNextTetro();

		// Add all blocks from all tetrominoes to single List
		// for faster rendering and colision detection
		foreach (Block b in _curTetro.Blocks)
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
			if (!_curTetro.Blocks.Any(_b => _b == b) && b.X == x && b.Y == y)
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
		=> _blocks.Where(b => !_curTetro.Blocks.Contains(b));

	public void SetBlocks(IEnumerable<Block> blocks)
		=> _blocks = blocks.ToList();
}
