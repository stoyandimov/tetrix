namespace Tetrix
{
    public class Block 
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Color { get; set; } 
        public int I { get; set; }
        public Block(int x, int y, int color, int i)
        {
            X = x;
            Y = y;
            I = i;
            Color = color;
        }
    }
}