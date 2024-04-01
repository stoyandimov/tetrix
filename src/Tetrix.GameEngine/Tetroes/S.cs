namespace Tetrix.GameEngine.Tetroes;

public class S : Tetro
{
	public S(int x, int y, Playfield playfield)
		: base(x, y, playfield)
	{
		Type = TetroTypes.S;
		CreateBlocks();
	}

	private void CreateBlocks() => Blocks =
	[
		new(X + 2, Y + 0, Type),
		new(X + 3, Y + 0, Type),
		new(X + 1, Y + 1, Type),
		new(X + 2, Y + 1, Type),
	];

	public override void Rotate()
	{
		// is horizontal
		if (Blocks[2].Y == Blocks[3].Y)
		{
			if (!_playfield.AreLocationAvailale(
				(Blocks[2].X + 2, Blocks[2].Y),
				(Blocks[3].X, Blocks[3].Y - 2)))
				return;

			Blocks[2].X += 2;
			Blocks[3].Y -= 2;
		}
		// is vertical
		else
		{
			if (!_playfield.AreLocationAvailale(
				(Blocks[2].X - 2, Blocks[2].Y),
				(Blocks[3].X, Blocks[3].Y + 2)))
				return;

			Blocks[2].X -= 2;
			Blocks[3].Y += 2;
		}
	}
}
