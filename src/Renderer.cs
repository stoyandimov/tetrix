using System;
using System.Collections.Concurrent;

namespace Tetrix
{
    public class Renderer
    {
        public bool Debug;

        public int AdditionsCounter = 0;
        public int DeletionsCounter = 0;
        public int MutationsCounter = 0;

        public Renderer(bool debug)
        {
            Debug = debug;
        }

        public BlockingCollection<TetroMutation> Mutations { get; private set; }  = new BlockingCollection<TetroMutation>();  

        public void ProcessUpdates()
        {
            while(true) 
            {
                TetroMutation m = Mutations.Take();
                m.RemoveRedundentBlockMutations();
                foreach (Tuple<Block, int, int> pos in m.SourcePosition)
                {
                    Console.SetCursorPosition(pos.Item2, pos.Item3);
                    Console.Write(' ');
                    
                    DeletionsCounter++;
                }

                foreach (Tuple<Block, int, int> pos in m.TargetPosition)
                {
                    Console.SetCursorPosition(pos.Item2, pos.Item3);
                    Console.ForegroundColor = (ConsoleColor) pos.Item1.Color;
                    Console.Write(Debug ? pos.Item1.Debug : pos.Item1.Symbol);
                    Console.ResetColor();

                    AdditionsCounter++;
                }

                MutationsCounter++;
                if (Debug)
                {
                    Console.SetCursorPosition(20, 15);
                    Console.Write("Additions: " + AdditionsCounter);
                    Console.SetCursorPosition(20, 16);
                    Console.Write("Deletions: " + DeletionsCounter);
                    Console.SetCursorPosition(20, 17);
                    Console.Write("Mutations: " + MutationsCounter);
                }

                Console.SetCursorPosition(0, 27);
            }
        }
    }
}