namespace Tetrix.UI
{
    // Represents the entire writeable console.
    public class Canvas
    {

        // Grid [Y] [X]
        public Point[,] Grid { get; set; }
        public int Height { get; private set; }
        public int Width { get; private set; }

        public Canvas(int h, int w)
        {
            Height = h;
            Width = w;
            InitializeCanvasGrid(Height, Width);
        }

        // Create a Point for each position within the console buffer
        private void InitializeCanvasGrid(int h, int w)
        {
            Grid = new Point[h, w];
            // Rows
            for(int y = 0; y < h; y++)
            {
                // Columns
                for(int x = 0; x < w; x++)
                    // Add to Grid
                    Grid[y, x] = new Point(x, y);
            }
        }

        // Outputs a rectangle
        public void Rect(int x, int y, int h, int w, int foreColor = 15, int backColor = 0)
        {
            // Horizontal borders including corners
            Write(x, y, "+", foreColor, backColor);
            Write(x + w + 1, y, "+", foreColor, backColor);
            for (int _x = 1; _x < w + 1; _x++)
            {
                Write(_x + x, y, "-", foreColor, backColor);
                Write(_x + x, y + h + 1, "-", foreColor, backColor);
            }
            Write(x, y + h + 1, "+", foreColor, backColor);
            Write(x + w + 1, y + h + 1, "+", foreColor, backColor);            

            // Vericle border EXcluding corners
            for (int _y = 1; _y < h + 1; _y++)
            {
                Write(x, _y + y, "|", foreColor, backColor);
                Write(x + w + 1, _y + y, "|", foreColor, backColor);                
            }
        }

        // Outputs strings
        public void Write(int x, int y, string text, int foreColor = 15, int backColor = 0)
        {
            for (int i = 0; i < text.Length; i++)
            {
                var point = Grid[y, x + i];
                point.Symbol = text[i];
                point.ForeColor = foreColor;
                point.BackColor = backColor;
            }
        }

        // Output verticle strings
        public void WriteVerticle(int x, int y, string text, int foreColor = 15, int backColor = 0)
        {
            for (int i = 0; i < text.Length; i++)
            {
                var point = Grid[x + i, y];
                point.Symbol = text[i];
                point.ForeColor = foreColor;
                point.BackColor = backColor;
            }
        }
    }
}