namespace Tetrix.GameEngine.Tetroes;

public abstract class Tetro(int x, int y, Playfield playfield)
{
	protected const char SYMBOL = '\u2588';
	public Block[] Blocks { get; protected set; }
	public int X { get; set; } = x;
	public int Y { get; set; } = y;
	public TetroTypes Type { get; protected set; }

	protected Playfield _playfield = playfield;

	public abstract void Rotate();

	public void MoveLeft()
	{
		foreach (Block b in Blocks)
			b.X--;
	}

	public void MoveRight()
	{
		foreach (Block b in Blocks)
			b.X++;
	}

	public void MoveDown()
	{
		foreach (Block b in Blocks)
			b.Y++;
	}

	public bool CanMoveLeft()
	{
		foreach (Block b in Blocks)
		{
			// Check other element
			if (!_playfield.IsLocationAvailable(b.X - 1, b.Y))
				return false;
		}

		return true;
	}

	public bool CanMoveRight()
	{
		foreach (Block b in Blocks)
		{
			// Check other element
			if (!_playfield.IsLocationAvailable(b.X + 1, b.Y))
				return false;
		}

		return true;
	}

	public bool CanMoveDown()
	{
		foreach (Block b in Blocks)
		{
			// Check other element
			if (!_playfield.IsLocationAvailable(b.X, b.Y + 1))
				return false;
		}

		return true;
	}

	// Creates tetromino
	public static Tetro CreateTetro(TetroTypes type, Playfield playfield) => type switch
	{
		TetroTypes.I => new I(1 + playfield.W / 2, 0, playfield),
		TetroTypes.O => new O(0 + playfield.W / 2, 0, playfield),
		TetroTypes.T => new T(1 + playfield.W / 2, 0, playfield),
		TetroTypes.S => new S(1 + playfield.W / 2, 0, playfield),
		TetroTypes.Z => new Z(1 + playfield.W / 2, 0, playfield),
		TetroTypes.J => new J(1 + playfield.W / 2, 0, playfield),
		TetroTypes.L => new L(1 + playfield.W / 2, 0, playfield),
		_ => throw new ArgumentOutOfRangeException(nameof(type)),
	};
}
