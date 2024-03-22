namespace Tetrix.GameEngine.Tetroes;

public class O : Tetro
{
	public O(int x, int y, Playfield playfield)
		: base(x, y, playfield)
	{
		X = x;
		Y = y;
		Color = 14;
		Type = TetroTypes.O;
		CreateBlocks();
	}

	private void CreateBlocks() => Blocks =
	[
		new(X + 0, Y + 0, Color, SYMBOL, '0'),
			new(X + 0, Y + 1, Color, SYMBOL, '1'),
			new(X + 1, Y + 0, Color, SYMBOL, '2'),
			new(X + 1, Y + 1, Color, SYMBOL, '3'),
		];

	public override void Rotate()
	{
		// rotation has no impact on view
	}
}
