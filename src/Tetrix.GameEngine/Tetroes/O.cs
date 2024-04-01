namespace Tetrix.GameEngine.Tetroes;

public class O : Tetro
{
	public O(int x, int y, Playfield playfield)
		: base(x, y, playfield)
	{
		Type = TetroTypes.O;
		CreateBlocks();
	}

	private void CreateBlocks() => Blocks =
	[
		new(X + 0, Y + 0, Type),
		new(X + 0, Y + 1, Type),
		new(X + 1, Y + 0, Type),
		new(X + 1, Y + 1, Type),
	];

	public override void Rotate()
	{
		// rotation has no impact on view
	}
}
