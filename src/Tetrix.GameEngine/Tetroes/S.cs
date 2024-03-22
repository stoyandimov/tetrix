namespace Tetrix.GameEngine.Tetroes;

public class S : Tetro
{
	public S(int x, int y, Playfield playfield)
		: base(x, y, playfield)
	{
		Color = 10;
		Type = TetroTypes.S;
		CreateBlocks();
	}

	private void CreateBlocks() => Blocks =
	[
		new(X + 2, Y + 0, Color, SYMBOL, '0'),
		new(X + 3, Y + 0, Color, SYMBOL, '1'),
		new(X + 1, Y + 1, Color, SYMBOL, '2'),
		new(X + 2, Y + 1, Color, SYMBOL, '3'),
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
