using System.Collections.Generic;

namespace Tetrix
{
    // Represents the entire writeable console.
    public class Canvas
    {
        // Contains all blocks
        public IList<Block> Blocks {get; private set; } = new List<Block>();
        public int Height { get; private set; }
        public int Width { get; private set; }

        public Canvas(int h, int w)
        {
            Height = h;
            Width = w;
        }

        public void Render()
        {
            // render all blocks on the correct positions
        }
    }
}