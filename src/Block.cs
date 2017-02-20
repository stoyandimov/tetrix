using Tetrix.UI;

namespace Tetrix
{
    // Represents a location within the Console.
    public class Block 
    {
       // Color number according to System.ConsoleColor
        public int Color { get; set; } 

        // The symbol that will be outputed to the console
        public char Symbol { get; set; }
        public char Debug { get; set; }

        public Point Point { get; private set;}

        // The X (left) position
        private int _x;
        public int X
        {   get
            {
                return _x;
            }
            set
            {
                _x = value;
                Point.X = value;
            }
         }

        // The Y (top) position
        private int _y;
        public int Y
        {   get
            {
                return _y;
            }
            set
            {
                _y = value;
                Point.Y = value;
            }
         }

        // Constructor
        public Block(int x, int y, int color, char symbol, char debug)
        {
            Point = new Point(x, y)
            {
                Symbol = '#',
                ForeColor = color,
            };
            Symbol = symbol;
            Debug = debug;
            Y = y;
            X = x;
        }
    }
}