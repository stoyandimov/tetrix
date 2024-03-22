using Tetrix.GameEngine.Tetroes;

namespace Tetrix.GameEngine.Storage;

public record SavableData(int GameSpeed, int Score, TetroTypes CurrentTetro, TetroTypes NextTetro, IEnumerable<Block> Blocks);
