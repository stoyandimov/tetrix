namespace Tetrix.Tetroes
{
    public class TetroBlock : Block
    {
        // Color number according to System.ConsoleColor
        public int Color { get; set; } 


        // The index of the element within Tetrominoes (used for debugging)
        public int I { get; set; }
        
        // Constructor
        public TetroBlock(int x, int y, int color, int i)
            : base (x, y)
        {
            I = i;
            Color = color;
        }
    }
}