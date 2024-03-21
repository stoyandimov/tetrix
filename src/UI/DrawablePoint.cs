namespace Tetrix.UI;

public class DrawablePoint(int x, int y, char symbol) : Point(x, y)
{
	public const int DefaultForeColor = 15;
	public const int DefaultBackColor = 0;
	public char Debug { get; set; } = symbol;
	public char Symbol { get; set; } = symbol;
	public int ForeColor { get; set; } = DefaultForeColor;
	public int BackColor { get; set; } = DefaultBackColor;

	public DrawablePoint(int x, int y, int color, char symbol, char debug = ' ')
		: this(x, y, symbol)
	{
		ForeColor = color;
		Debug = debug == ' ' ? symbol : debug;
	}
}
