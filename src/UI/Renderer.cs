using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Tetrix.UI
{
    public class Renderer
    {
        public bool Debug;

        public int AdditionsCounter = 0;
        public int DeletionsCounter = 0;
        public int MutationsCounter = 0;

        CancellationTokenSource _cts;

        TimeSpan _mutationProcessingTime;

        public BlockingCollection<GridMutation> Mutations { get; private set; }  = new BlockingCollection<GridMutation>();  

        public Renderer()
        {
            _mutationProcessingTime = default(TimeSpan);
            _cts = new CancellationTokenSource();
        }

        public void RenderDebug(object state)
        {
            if (!Debug) return;

            this.Mutations.Add(TextHelper.WriteLines(15, 15, new string[] {
                "Rendering:",
                "----------",
                "Additions: " + AdditionsCounter,
                "Deletions: " + DeletionsCounter,
                "Mutations: " + MutationsCounter,
                "Mutation mean time: " + (
                _mutationProcessingTime.TotalMilliseconds / MutationsCounter).ToString("#.##") + " ms/m"
            }));
        }

        public void BeginRendering()
        {
            Clear();
            Task.Run(() => this.ProcessUpdates(_cts.Token));
        }

        public void Clear()
            => Console.Clear();

        public void EndRendering()
        {
            if (!_cts.IsCancellationRequested)
                _cts.Cancel();
        }

        public void ProcessUpdates(CancellationToken cancellationToken)
        {
            var dTimer = new Timer(RenderDebug, null, 0, 500);
            while (true)
            {
                GridMutation m = Mutations.Take();
                var Start = DateTime.Now;
                // Remove all blocks mutations that won't affect the GUI
                // m.RemoveRedundentBlockMutations();
                // Clears blocks after move
                foreach (var p in m.SourcePosition)
                {
                    Console.SetCursorPosition(p.X, p.Y);
                    Console.Write(' ');
                    DeletionsCounter++;
                }
                // Renders the block on the new position
                foreach (var p in m.TargetPosition)
                {
                    if (Console.ForegroundColor != (ConsoleColor) p.ForeColor)
                        Console.ForegroundColor = (ConsoleColor) p.ForeColor;
                    Console.SetCursorPosition(p.X, p.Y);
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
            Debug = false;
        }

        public void Write(int x, int y, string text)
            => Mutations.Add(TextHelper.Write(x, y, text));

        public void WriteLines(int x, int y, string[] lines)
            => Mutations.Add(TextHelper.WriteLines(x, y, lines));

    }
}