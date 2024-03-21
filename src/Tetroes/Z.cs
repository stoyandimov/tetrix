namespace Tetrix.Tetroes;

public class Z : Tetro
{
	public Z(int x, int y, Playfield playfield)
		: base(x, y, playfield)
	{
		Color = 12;
		Type = TetroTypes.Z;
		CreateBlocks();
	}

	private void CreateBlocks() => Blocks =
	[
		new(X + 1, Y + 0, Color, SYMBOL, '0'),
		new(X + 2, Y + 0, Color, SYMBOL, '1'),
		new(X + 2, Y + 1, Color, SYMBOL, '2'),
		new(X + 3, Y + 1, Color, SYMBOL, '3'),
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
