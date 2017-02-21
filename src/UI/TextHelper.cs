using System;

namespace Tetrix.UI
{
    public static class TextHelper
    {
        public static GridMutation Write(int x, int y, string text)
        {
            var m = new GridMutation();
            for (int i = 0; i < text.Length; i++)
                m.TargetPosition.Add(new Tuple<Point, int, int>(
                    new Point(x + i, y) { Symbol = text[i] }, x + i, y
                ));

            return m;
        }
    }
}