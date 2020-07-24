namespace Tetrix.UI
{
    public class DrawablePoint : Point
    {
        public const int DefaultForeColor = 15;
        public const int DefaultBackColor = 0;
        public char Debug { get; set; }
        public char Symbol { get; set; } = ' ';
        public int ForeColor { get; set; } = DefaultForeColor;
        public int BackColor { get; set; } = DefaultBackColor;

        public DrawablePoint(int x, int y, char symbol)
            : base(x, y)
        {
            Symbol = symbol;
            Debug = symbol;
        }

        public DrawablePoint(int x, int y, int color, char symbol, char debug = ' ')
            : this(x, y, symbol)
        {
            ForeColor = color;
            Debug = debug == ' ' ? symbol : debug;
        }
    }
}
