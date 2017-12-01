namespace Tetrix.UI
{
    public class DrawablePoint : Point
    {
        public char Debug { get; set; }
        public char Symbol { get; set; } = ' ';
        public int ForeColor { get; set; } = 15;
        public int BackColor { get; set; } = 0;

        public DrawablePoint(int x, int y, char symbol) 
            : base(x, y)
            => Symbol = symbol;

        public DrawablePoint(int x, int y, int color, char symbol, char debug)
            : base(x, y)
        {
            Symbol = '#';
            ForeColor = color;
            Symbol = symbol;
            Debug = debug;
        }
    }
}
