namespace Tetrix.UI
{
    public static class TextHelper
    {
        public static GridMutation Write(int x, int y, string text)
        {
            var m = new GridMutation();
            for (int i = 0; i < text.Length; i++)
                m.AddTarget(new DrawablePoint(x + i, y, text[i]));

            return m;
        }
        public static GridMutation WriteLines(int x, int y, string[] lines)
        {
            var m = new GridMutation();
            for (int _y = 0; _y < lines.Length; _y++)
            {
                string text = lines[_y];
                for (int _x = 0; _x < text.Length; _x++)
                    m.AddTarget(new DrawablePoint(x + _x, y + _y, text[_x]));
            }

            return m;
        }
    }
}