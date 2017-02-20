using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Tetrix.UI
{
    public class Renderer
    {
        public bool Debug;

        public int AdditionsCounter = 0;
        public int DeletionsCounter = 0;
        public int MutationsCounter = 0;

        TimeSpan _mutationProcessingTime;

        public BlockingCollection<GridMutation> Mutations { get; private set; }  = new BlockingCollection<GridMutation>();  

        public Renderer(bool debug)
        {
            _mutationProcessingTime = default(TimeSpan);
            Debug = debug;
        }

        public void ProcessUpdates(CancellationToken cancellationToken)
        {
            while(true) 
            {
                GridMutation m = Mutations.Take();
                var Start = DateTime.Now;
                m.RemoveRedundentBlockMutations(); // Remove all blocks mutations that won't affect the GUI
                foreach (Tuple<Point, int, int> pos in m.SourcePosition)
                {
                    Console.SetCursorPosition(pos.Item2, pos.Item3);
                    Console.Write(' ');

                    DeletionsCounter++;
                }

                foreach (Tuple<Point, int, int> pos in m.TargetPosition)
                {
                    Console.SetCursorPosition(pos.Item2, pos.Item3);
                    Console.ForegroundColor = (ConsoleColor) pos.Item1.ForeColor;
                    Console.Write(pos.Item1.Symbol);
                    Console.ResetColor();

                    AdditionsCounter++;
                }
                var End = DateTime.Now;
               _mutationProcessingTime =  _mutationProcessingTime.Add(End - Start);
                MutationsCounter++;
                if (Debug)
                {
                    Console.SetCursorPosition(20, 15);
                    Console.Write("Additions: " + AdditionsCounter);
                    Console.SetCursorPosition(20, 16);
                    Console.Write("Deletions: " + DeletionsCounter);
                    Console.SetCursorPosition(20, 17);
                    Console.Write("Mutations: " + MutationsCounter);
                    Console.SetCursorPosition(20, 18);
                    Console.Write("Mutation mean time: " + (
                        _mutationProcessingTime.TotalMilliseconds / MutationsCounter).ToString("#.##") + " ms/m");
                }

                Console.SetCursorPosition(0, 27);
                
                if (cancellationToken.IsCancellationRequested)
                    break;
            }
        }
    }
}