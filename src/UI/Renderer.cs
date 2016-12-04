using System;

namespace Tetrix.UI
{
    public class Renderer
    {
        public Canvas Canvas { get; private set; }
        public Renderer(Canvas canvas)
        {
            Canvas = canvas;
        }

        // Renders the entire console buffer
        public void Render()
        {
            foreach(Point p in Canvas.Grid)
            {
                Console.SetCursorPosition(p.X, p.Y);
                Console.ForegroundColor = (ConsoleColor) p.ForeColor;
                Console.BackgroundColor = (ConsoleColor) p.BackColor;
                Console.Write(p.Symbol);
            }
        }
    }
}