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

        // The X (left) position
        public int X { get; set; }
        
        // The Y (top) position
        public int Y { get; set; }

        Renderer _renderer;
        
        // Constructor
        public Block(int x, int y, int color, char symbol, Renderer renderer, char debug)
        {
            _renderer = renderer;
            Symbol = symbol;
            Debug = debug;
            Y = y;
            X = x;
            Color = color;
        }
        public Block(int x, int y, int color, char symbol, Renderer renderer)
            : this(x, y, color, symbol, renderer, symbol)
        {
        }
    }
}