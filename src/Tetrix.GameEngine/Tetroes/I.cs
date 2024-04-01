namespace Tetrix.GameEngine.Tetroes;

public class I : Tetro
{
	public I(int x, int y, Playfield playfield)
		: base(x, y, playfield)
	{
		Type = TetroTypes.I;
		CreateBlocks();
	}

	private void CreateBlocks() => Blocks =
	[
		new(X + 0, Y + 0, Type),
		new(X + 1, Y + 0, Type),
		new(X + 2, Y + 0, Type),
		new(X + 3, Y + 0, Type),
	];

	public override void Rotate()
	{
		// is vertical
		if (Blocks[0].X == Blocks[1].X)
		{
			if (!_playfield.AreLocationAvailale(
				(Blocks[1].X + 1, Blocks[1].Y - 1),
				(Blocks[2].X + 2, Blocks[2].Y - 2),
				(Blocks[3].X + 3, Blocks[3].Y - 3)))
				return;

			Blocks[1].X += 1;
			Blocks[2].X += 2;
			Blocks[3].X += 3;

			Blocks[1].Y -= 1;
			Blocks[2].Y -= 2;
			Blocks[3].Y -= 3;
		}
		// is horizontal
		else
		{
			if (!_playfield.AreLocationAvailale(
				(Blocks[1].X - 1, Blocks[1].Y + 1),
				(Blocks[2].X - 2, Blocks[2].Y + 2),
				(Blocks[3].X - 3, Blocks[3].Y + 3)))
				return;

			Blocks[1].X -= 1;
			Blocks[2].X -= 2;
			Blocks[3].X -= 3;

			Blocks[1].Y += 1;
			Blocks[2].Y += 2;
			Blocks[3].Y += 3;
		}
	}
}
