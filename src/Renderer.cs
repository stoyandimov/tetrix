using System;
using System.Collections.Concurrent;

namespace Tetrix
{
    public class Renderer
    {
        public BlockingCollection<BlockMovement> Moves { get; private set; }  = new BlockingCollection<BlockMovement>();  

        public void ProcessUpdates()
        {
            while(true) 
            {
                BlockMovement m = Moves.Take();
                Console.SetCursorPosition(m.OldX, m.OldY);
                Console.Write(' ');
                Console.SetCursorPosition(m.NewX, m.NewY);
                Console.ForegroundColor = (ConsoleColor) m.Block.Color;
                Console.Write(m.Block.Debug);
                Console.ResetColor();
                Console.SetCursorPosition(0, 27);
            }
        }
    }

    public class BlockMovement
    {
        public int OldX { get; set; }
        public int OldY { get; set; }
        public int NewX { get; set; }
        public int NewY { get; set; }
        public Block Block { get; set; }
    }
}