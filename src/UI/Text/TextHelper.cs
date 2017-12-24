namespace Tetrix.UI.Text
{
    public class TextWriter : ITextWriter
    {
        public GridMutation WriteText(int x, int y, string text)
        {
            var m = new GridMutation();
            for (int i = 0; i < text.Length; i++)
                m.AddTarget(new DrawablePoint(x + i, y, text[i]));
            return m;
        }
    }
}