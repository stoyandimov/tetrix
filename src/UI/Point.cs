namespace Tetrix.UI
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Symbol { get; set; } = ' ';
        public int ForeColor { get; set; } = 15;
        public int BackColor { get; set; } = 0;
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}