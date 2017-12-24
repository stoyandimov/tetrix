namespace Tetrix.UI.Text
{
    public static class TextWriterExtensions
    {
        public static void WriteText(this Renderer renderer, int x, int y, string text)
            => renderer.Render(new TextWriter().WriteText(x, y, text));

        public static void WriteFiglet(this Renderer renderer, int x, int y, string text)
            => renderer.Render(new FigletWriter().WriteText(x, y, text));
    }
}
