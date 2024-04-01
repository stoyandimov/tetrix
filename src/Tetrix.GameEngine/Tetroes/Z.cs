namespace Tetrix.GameEngine.Tetroes;

public class Z : Tetro
{
	public Z(int x, int y, Playfield playfield)
		: base(x, y, playfield)
	{
		Type = TetroTypes.Z;
		CreateBlocks();
	}

	private void CreateBlocks() => Blocks =
	[
		new(X + 1, Y + 0, Type),
		new(X + 2, Y + 0, Type),
		new(X + 2, Y + 1, Type),
		new(X + 3, Y + 1, Type),
	];

	public override void Rotate()
	{
		// is vertical
		if (Blocks[2].Y == Blocks[3].Y)
		{
			if (!_playfield.AreLocationAvailale(
				(Blocks[0].X + 2, Blocks[0].Y),
				(Blocks[3].X, Blocks[3].Y - 2)))
				return;

			Blocks[0].X += 2;
			Blocks[3].Y -= 2;
		}
		// is horizontal
		else
		{
			if (!_playfield.AreLocationAvailale(
				(Blocks[0].X - 2, Blocks[0].Y),
				(Blocks[3].X, Blocks[3].Y + 2)))
				return;

			Blocks[0].X -= 2;
			Blocks[3].Y += 2;
		}
	}
}
