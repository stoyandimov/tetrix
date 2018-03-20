using Tetrix.UI;

namespace Tetrix
{
    // Represents a location within the Console.
    public class Block : DrawablePoint
    {
        public Block(int x, int y, int color, char symbol, char debug)
            : base(x, y, color, symbol, debug)
        {
        }
    }
}