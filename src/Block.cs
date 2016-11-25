namespace Tetrix
{
    public class Block 
    {
        // The X (left) position
        public int X { get; set; }
        
        // The X (top) position
        public int Y { get; set; }

        // Color number according to System.ConsoleColor
        public int Color { get; set; } 

        // The index of the element within Tetrominoes (used for debugging)
        public int I { get; set; }
        
        // Constructor
        public Block(int x, int y, int color, int i)
        {
            X = x;
            Y = y;
            I = i;
            Color = color;
        }
    }
}