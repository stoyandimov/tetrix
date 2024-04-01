using Tetrix.GameEngine.Tetroes;

namespace Tetrix.GameEngine;

public class Playfield(int h, int w, Func<int> randomizer)
{
	// Height of the playfield
	public int H { get; private set; } = h;

	// Width of the playfield
	public int W { get; private set; } = w;

	public Func<int> _randomizer;

	public Tetro CurrentTetro { get; private set; }
	public Tetro NextTetro { get; private set; }

	// List of all block from all tetrominoes
	private List<Block> _blocks = [];

	public event EventHandler<int> RowRemoved;
	public event EventHandler<PlayfieldGridMutation> PlayfieldGridChanged;
	public event EventHandler<Tetro> CurrentTetroChanged;
	public event EventHandler<Tetro> NextTetroChanged;

	protected void OnRowRemoved(int e) => RowRemoved?.Invoke(this, e);

	// Progresses the game - 1 move.
	public bool Progress()
	{
		if (CurrentTetro == null)
			ResetCurrentTetro(randomizer());

		if (!CurrentTetro.CanMoveDown())
		{
			// Checks for full rows and removes if Any
			RemoveFullRowsIfAny();
			// Reset the current tetromino
			ResetCurrentTetro(randomizer());
			// If the new tetro cannot move done - game over
			return CurrentTetro.CanMoveDown();
		}

		MoveDown();
		return true;
	}

	public void Rotate() => CurrentTetro.Rotate();

	public void SetCurrentTetro(Tetro tetro) => CurrentTetro = tetro;

	public void MoveLeft()
	{
		if (CurrentTetro.CanMoveLeft())
			CurrentTetro.MoveLeft();
	}

	public void MoveRight()
	{
		if (CurrentTetro.CanMoveRight())
			CurrentTetro.MoveRight();
	}

	public void MoveDown()
	{
		if (CurrentTetro.CanMoveDown())
			CurrentTetro.MoveDown();
	}

	public Tetro GenerateTetro(int i) => Tetro.CreateTetro((TetroTypes)i, this);


	// Set current tetromino and generate the next one
	public void ResetCurrentTetro(int i)
	{
		if (NextTetro == null)
			NextTetro = GenerateTetro(i);
		CurrentTetro = NextTetro;
		CurrentTetroChanged?.Invoke(this, CurrentTetro);
		NextTetro = GenerateTetro(i);
		NextTetroChanged?.Invoke(this, NextTetro);

		// Add all blocks from all tetrominoes to single List
		// for faster rendering and colision detection
		foreach (Block b in CurrentTetro.Blocks)
			_blocks.Add(b);
	}

	public void RemoveFullRowsIfAny()
	{
		// Check for full rows
		var rowsToRemove = new List<int>();
		for (int y = 0; y <= H; y++)
		{
			var row = _blocks.Where(b => b.Y == y);
			int count = row.Count();
			if (count == W)
				rowsToRemove.Add(y);
		}

		OnRowRemoved(rowsToRemove.Count);
	}

	// Check if a single block location (x, y) is available/empty 
	public bool IsLocationAvailable(int x, int y)
	{
		// check out of boundries
		if (x >= W + 1 || x < 1 || y >= H + 1)
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

	public IEnumerable<Block> GetBlocks()
		=> _blocks.Where(b => !CurrentTetro.Blocks.Contains(b));

	public void SetBlocks(IEnumerable<Block> blocks)
		=> _blocks = blocks.ToList();
}
