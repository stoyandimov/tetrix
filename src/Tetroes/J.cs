namespace Tetrix.Tetroes;

public class J : Tetro
{
	public J(int x, int y, Playfield playfield)
		: base(x, y, playfield)
	{
		Color = 3;
		Type = TetroTypes.J;
		CreateBlocks();
	}

	private void CreateBlocks() => Blocks =
	[
		new(X + 2, Y + 0, Color, SYMBOL, '0'),
		new(X + 2, Y + 1, Color, SYMBOL, '1'),
		new(X + 2, Y + 2, Color, SYMBOL, '2'),
		new(X + 1, Y + 2, Color, SYMBOL, '3'),
	];

	public override void Rotate()
	{
		// Rotate clockwise around 1

		// up to right
		if (Blocks[0].Y < Blocks[1].Y)
		{
			if (!_playfield.AreLocationAvailale(
				(Blocks[0].X + 1, Blocks[0].Y + 1),
				(Blocks[2].X - 1, Blocks[2].Y - 1),
				(Blocks[3].X, Blocks[3].Y - 2)))
				return;

			Blocks[0].X += 1;
			Blocks[0].Y += 1;
			Blocks[2].X -= 1;
			Blocks[2].Y -= 1;
			Blocks[3].Y -= 2;
		}
		// right to bottom
		else if (Blocks[0].X > Blocks[1].X)
		{
			if (!_playfield.AreLocationAvailale(
				(Blocks[0].X - 1, Blocks[0].Y + 1),
				(Blocks[2].X + 1, Blocks[2].Y - 1),
				(Blocks[3].X + 2, Blocks[3].Y)))
				return;

			Blocks[0].X -= 1;
			Blocks[0].Y += 1;
			Blocks[2].X += 1;
			Blocks[2].Y -= 1;
			Blocks[3].X += 2;
		}
		// bottom to left
		else if (Blocks[0].Y > Blocks[1].Y)
		{
			if (!_playfield.AreLocationAvailale(
				(Blocks[0].X - 1, Blocks[0].Y - 1),
				(Blocks[2].X + 1, Blocks[2].Y + 1),
				(Blocks[3].X, Blocks[3].Y + 2)))
				return;

			Blocks[0].X -= 1;
			Blocks[0].Y -= 1;
			Blocks[2].X += 1;
			Blocks[2].Y += 1;
			Blocks[3].Y += 2;
		}
		// left to top
		else
		{
			if (!_playfield.AreLocationAvailale(
				(Blocks[0].X + 1, Blocks[0].Y - 1),
				(Blocks[2].X - 1, Blocks[2].Y + 1),
				(Blocks[3].X - 2, Blocks[3].Y)))
				return;

			Blocks[0].X += 1;
			Blocks[0].Y -= 1;
			Blocks[2].X -= 1;
			Blocks[2].Y += 1;
			Blocks[3].X -= 2;
		}
	}
}
