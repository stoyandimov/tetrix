namespace Tetrix
{
    public class Block 
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Color { get; set; } 

        public Block(int x, int y, int color)
        {
            X = x;
            Y = y;
            Color = color;
        }
    }
}