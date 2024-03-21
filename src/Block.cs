using Tetrix.UI;

namespace Tetrix;

// Represents a location within the Console.
public class Block(int x, int y, int color, char symbol, char debug) : DrawablePoint(x, y, color, symbol, debug)
{
}
