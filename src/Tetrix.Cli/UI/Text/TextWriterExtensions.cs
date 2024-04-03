namespace Tetrix.Cli.UI.Text;

public static class TextWriterExtensions
{
	public static void WriteText(this IRenderer renderer, int x, int y, string text)
		=> renderer.Render(new TextWriter().WriteText(x, y, text));

}
