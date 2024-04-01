using Tetrix.GameEngine.Tetroes;

namespace Tetrix.GameEngine;

// Represents a location within the playfield.
public class Block(int x, int y, TetroTypes type)
{
	public int X { get; set; } = x;
	public int Y { get; set; } = y;
	public TetroTypes Type { get; set; } = type;
}
