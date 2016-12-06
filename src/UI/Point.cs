namespace Tetrix.UI
{

    // Make points move left, right, up, down.
    // Upon each move create one add and one remove with a Renderer.
    // Remove redundent removes (that is don't remove where add will take place
    // within the frame)
    // Each frame process a stack of adds and removes to the Canvas

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