using Tetrix.GameEngine.Tetroes;

namespace Tetrix.GameEngine;

public class Playfield(int h, int w, Func<int> randomizer)
{
	// Height of the playfield
	public int H { get; private set; } = h;

	// Width of the playfield
	public int W { get; private set; } = w;

	public Tetro CurrTetro { get; private set; }
	public Tetro NextTetro { get; private set; }

	// List of all block from all tetrominoes
	private List<Block> _blocks = [];

	public event EventHandler<int> RowRemoved;
	public event EventHandler<Block[]> PlayfieldGridChanging;
	public event EventHandler<Block[]> PlayfieldGridChanged;
	public event EventHandler<Tetro> CurrentTetroChanging;
	public event EventHandler<Tetro> CurrentTetroChanged;
	public event EventHandler<Tetro> NextTetroChanging;
	public event EventHandler<Tetro> NextTetroChanged;
	public event EventHandler GameOver;


	// Progresses the game - 1 move.
	public void Progress()
	{
		if (CurrTetro == null) SetCurrTetro(GenerateRandomTetro());
		if (NextTetro == null) SetNextTetro(GenerateRandomTetro());

		if (!CurrTetro.CanMoveDown())
		{
			// Reset the current and next tetromino
			ResetCurrentTetro(nextTetro: GenerateRandomTetro());
			// Checks for full rows and removes if Any
			RemoveFullRowsIfAny();
			// If the new tetro cannot move done - game over
			if (!CurrTetro.CanMoveDown())
				OnGameOver();
		}

		MoveDown();
	}

	public Tetro GenerateRandomTetro() => Tetro.CreateTetro((TetroTypes)randomizer(), this);

	public void SetCurrTetro(Tetro tetro)
	{
		if (CurrTetro is not null)
			OnCurrentTetroChanging(CurrTetro);
		CurrTetro = tetro;
		foreach (Block b in CurrTetro.Blocks) _blocks.Add(b);
		OnCurrentTetroChanged(CurrTetro);
		OnPlayfieldChanged(CurrTetro.Blocks);
	}

	public void SetNextTetro(Tetro tetro)
	{
		if (NextTetro is not null)
			OnNextTetroChanging(NextTetro);
		NextTetro = tetro;
		OnNextTetroChanged(NextTetro);
	}

	public void Rotate()
	{
		OnPlayfieldChanging(CurrTetro.Blocks);
		CurrTetro.Rotate();
		OnPlayfieldChanged(CurrTetro.Blocks);
	}

	public void MoveLeft()
	{
		if (!CurrTetro.CanMoveLeft()) return;
		OnPlayfieldChanging(CurrTetro.Blocks);
		CurrTetro.MoveLeft();
		OnPlayfieldChanged(CurrTetro.Blocks);
	}

	public void MoveRight()
	{
		if (!CurrTetro.CanMoveRight()) return;
		OnPlayfieldChanging(CurrTetro.Blocks);
		CurrTetro.MoveRight();
		OnPlayfieldChanged(CurrTetro.Blocks);
	}

	public void MoveDown()
	{
		if (!CurrTetro.CanMoveDown()) return;
		OnPlayfieldChanging(CurrTetro.Blocks);
		CurrTetro.MoveDown();
		OnPlayfieldChanged(CurrTetro.Blocks);
	}

	private void OnCurrentTetroChanging(Tetro tetro) => CurrentTetroChanging?.Invoke(this, tetro);
	private void OnCurrentTetroChanged(Tetro tetro) => CurrentTetroChanged?.Invoke(this, tetro);
	private void OnNextTetroChanging(Tetro tetro) => NextTetroChanging?.Invoke(this, tetro);
	private void OnNextTetroChanged(Tetro tetro) => NextTetroChanged?.Invoke(this, tetro);
	private void OnPlayfieldChanging(Block[] blocks) => PlayfieldGridChanging?.Invoke(this, blocks);
	private void OnPlayfieldChanged(Block[] blocks) => PlayfieldGridChanged?.Invoke(this, blocks);
	private void OnRowRemoved(int n) => RowRemoved?.Invoke(this, n);
	private void OnGameOver() => GameOver?.Invoke(this, EventArgs.Empty);

	// Set current tetromino and generate the next one
	public void ResetCurrentTetro(Tetro nextTetro)
	{
		SetCurrTetro(NextTetro);
		SetNextTetro(nextTetro);
	}

	public void RemoveFullRowsIfAny()
	{
		// Check for full rows
		var rowsToRemove = new List<int>();
		for (int y = 0; y <= H; y++)
		{
			var blocksInRow = _blocks.Where(b => b.Y == y).Count();
			if (blocksInRow == W)
				rowsToRemove.Add(y);
		}

		// If full rows remove the blocks
		var blocksToRemove = new List<Block>();
		var blocksToMoveDown = new List<Block>();
		foreach (int y in rowsToRemove)
		{
			// Select blocks to remove
			foreach (Block b in _blocks.Where(_b => _b.Y == y))
				blocksToRemove.Add(b);

			// Shift upper blocks down
			foreach (Block b in _blocks.Where(_b => _b.Y < y))
				blocksToMoveDown.Add(b);
		}

		OnPlayfieldChanging([.. blocksToMoveDown]);
		OnPlayfieldChanging([.. blocksToRemove]);

		blocksToRemove.ForEach(b => _blocks.Remove(b));
		blocksToMoveDown.ForEach(b => b.Y++);

		OnPlayfieldChanged([.. blocksToMoveDown]);
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
			if (!CurrTetro.Blocks.Any(_b => _b == b) && b.X == x && b.Y == y)
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
		=> _blocks.Where(b => !CurrTetro.Blocks.Contains(b));

	public void SetBlocks(IEnumerable<Block> blocks)
	{
		OnPlayfieldChanging([.. _blocks]);
		_blocks = blocks.ToList();
		OnPlayfieldChanged([.. blocks]);
	}

}
