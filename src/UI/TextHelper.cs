namespace Tetrix.UI
{
    public static class TextHelper
    {
        public static GridMutation Write(int x, int y, string text)
        {
            var m = new GridMutation();
            for (int i = 0; i < text.Length; i++)
                m.TargetPosition.Add((
                    new Point(x + i, y) { Symbol = text[i] }, x + i, y
                ));

            return m;
        }
        public static GridMutation WriteLines(int x, int y, string[] lines)
        {
            var m = new GridMutation();
            for (int _y = 0; _y < lines.Length; _y++)
            {
                string text = lines[_y];
                for (int _x = 0; _x < text.Length; _x++)
                    m.TargetPosition.Add((
                        new Point(x + _x, y + _y) { Symbol = text[_x] }, x + _x, y + _y
                    ));
            }

            return m;
        }
    }
}