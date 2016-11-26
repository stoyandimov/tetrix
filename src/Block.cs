namespace Tetrix
{
    // Represents a location within the Console.
    public class Block 
    {
        // The X (left) position
        public int X { get; set; }
        
        // The X (top) position
        public int Y { get; set; }

        // Constructor
        public Block(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}