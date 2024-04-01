using Tetrix.GameEngine.Tetroes;

namespace Tetrix.GameEngine.UI;

public static class BlockExtensions
{
	public static int GetColor(this Block block)
		=> block.Type switch
		{
			TetroTypes.I => 11,
			TetroTypes.J => 3,
			TetroTypes.L => 15,
			TetroTypes.O => 12,
			TetroTypes.S => 10,
			TetroTypes.T => 13,
			TetroTypes.Z => 12,
			_ => 0
		};
}