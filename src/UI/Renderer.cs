using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Tetrix.UI
{
    public class Renderer
    {
        readonly CancellationTokenSource _cts;
        readonly BlockingCollection<GridMutation> _mutations = new BlockingCollection<GridMutation>();

        public Renderer()
            =>_cts = new CancellationTokenSource();

        public void Render(GridMutation mutation)
            => _mutations.Add(mutation);

        public void BeginRendering()
        {
            Clear();
            Task.Run(() => ProcessUpdates(_cts.Token), _cts.Token);
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
            while (true)
            {
                GridMutation m = _mutations.Take();
                // Clears blocks after move
                foreach (var p in m.SourcePositions)
                {
                    if (p.X < 0 || p.Y < 0)
                        continue;

                    Console.SetCursorPosition(p.X, p.Y);
                    Console.Write(' ');
                }
                // Renders the block on the new position
                foreach (var p in m.TargetPositions)
                {
                    if (p.X < 0 || p.Y < 0)
                        continue;

                    if (Console.ForegroundColor != (ConsoleColor) p.ForeColor)
                        Console.ForegroundColor = (ConsoleColor) p.ForeColor;
                    Console.SetCursorPosition(p.X, p.Y);
                    Console.Write(p.Symbol);
                }
                if (cancellationToken.IsCancellationRequested)
                    break;
            }
        }
    }
}