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

        public void RenderDebug(object state)
        {
            if (Debug)
            {
                this.Mutations.Add(TextHelper.WriteLines(15, 10 , new string[] {
                    "Rendering:",
                    "----------",
                    "Additions: " + AdditionsCounter,
                    "Deletions: " + DeletionsCounter,
                    "Mutations: " + MutationsCounter,
                    "Mutation mean time: " + (
                    _mutationProcessingTime.TotalMilliseconds / MutationsCounter).ToString("#.##") + " ms/m"
                }));
            }
        }

        public void ProcessUpdates(CancellationToken cancellationToken)
        {
            var dTimer = new Timer(RenderDebug, null, 0, 500);
            while(true)
            {
                GridMutation m = Mutations.Take();
                var Start = DateTime.Now;
                m.RemoveRedundentBlockMutations(); // Remove all blocks mutations that won't affect the GUI
                foreach ((Point p, int x, int y) in m.SourcePosition)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(' ');

                    DeletionsCounter++;
                }

                foreach ((Point p, int x, int y) in m.TargetPosition)
                {
                    if (Console.ForegroundColor != (ConsoleColor) p.ForeColor)
                        Console.ForegroundColor = (ConsoleColor) p.ForeColor;
                    Console.SetCursorPosition(x, y);
                    Console.Write(p.Symbol);

                    AdditionsCounter++;
                }
                var End = DateTime.Now;
               _mutationProcessingTime =  _mutationProcessingTime.Add(End - Start);
                MutationsCounter++;

                if (cancellationToken.IsCancellationRequested)
                    break;
            }
            dTimer.Dispose();
        }
    }
}