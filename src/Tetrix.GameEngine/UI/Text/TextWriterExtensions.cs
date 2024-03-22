namespace Tetrix.GameEngine.UI.Text;

public static class TextWriterExtensions
{
	public static void WriteText(this IRenderer renderer, int x, int y, string text)
		=> renderer.Render(new TextWriter().WriteText(x, y, text));

	public static void WriteFiglet(this IRenderer renderer, int x, int y, string text)
		=> renderer.Render(new FigletWriter().WriteText(x, y, text));
}
