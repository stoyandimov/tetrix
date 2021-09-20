using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Tetrix.UI
{
    public class Renderer : IRenderer
    {
        private readonly GameSettings _settings;
        readonly CancellationTokenSource _cts;
        readonly BlockingCollection<GridMutation> _mutations = new BlockingCollection<GridMutation>();

        public Renderer(GameSettings settings)
        {
            _settings = settings;
            _cts = new CancellationTokenSource();
        }

        public void Render(GridMutation mutation)
            => _mutations.Add(mutation);

        public void Render(int x, int y, char symbol, int color = DrawablePoint.DefaultForeColor, char d = ' ')
        {
            var dp = new DrawablePoint(x, y, color, symbol, d);
            var m = new GridMutation();
            m.AddTarget(dp);
            Render(m);
        }

        public void Render(int x, int y, char symbol, int oldX, int oldY, int color = DrawablePoint.DefaultForeColor, char d = ' ')
        {
            var @new = new DrawablePoint(x, y, color, symbol, d);
            var old = new Point(oldX, oldY);
            var mut = new GridMutation();
            mut.AddSource(old);
            mut.AddTarget(@new);
            Render(mut);
        }


        public void BeginRendering()
        {
            Clear();
            Task.Run(() => ProcessUpdates(_cts.Token), _cts.Token);
        }

        public void Clear(int x, int y)
        {
            var mut = new GridMutation();
            mut.AddSource(x, y);
            Render(mut);
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
                    Console.Write(_settings.Debug ? p.Debug : p.Symbol);
                }
                if (cancellationToken.IsCancellationRequested)
                    break;
            }
        }
    }
}