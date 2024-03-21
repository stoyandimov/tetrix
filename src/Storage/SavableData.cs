using Tetrix.Tetroes;

namespace Tetrix.Storage;

public class SavableData
{
	public int GameSpeed { get; set; }
	public int Score { get; set; }
	public TetroTypes CurrentTetro { get; set; }
	public TetroTypes NextTetro { get; set; }
	public IEnumerable<Block> Blocks { get; set; }
}
