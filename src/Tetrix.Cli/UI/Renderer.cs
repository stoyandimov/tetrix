using System.Collections.Concurrent;

namespace Tetrix.Cli.UI;

public class Renderer(GameSettings settings) : IRenderer
{
	protected readonly CancellationTokenSource _cts = new();
	protected readonly GameSettings _settings = settings;
	protected readonly BlockingCollection<GridMutation> _mutations = [];

	public void Render(GridMutation mutation)
		=> _mutations.Add(mutation);

	public virtual void Render(int x, int y, char symbol, int color = DrawablePoint.DefaultForeColor, char d = ' ')
	{
		var dp = new DrawablePoint(x, y, color, symbol, d);
		var m = new GridMutation();
		m.AddTarget(dp);
		Render(m);
	}

	public virtual void Render(int x, int y, char symbol, int oldX, int oldY, int color = DrawablePoint.DefaultForeColor, char d = ' ')
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

	public virtual void Clear(int x, int y)
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

	public virtual void ProcessUpdates(CancellationToken ct)
	{
		while (true)
		{
			GridMutation m = _mutations.Take(ct);
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

				if (Console.ForegroundColor != (ConsoleColor)p.ForeColor)
					Console.ForegroundColor = (ConsoleColor)p.ForeColor;
				Console.SetCursorPosition(p.X, p.Y);
				Console.Write(_settings.Debug ? p.Debug : p.Symbol);
				// Console.Write(_settings.Debug ? p.Debug : p.Y.ToString().Last());
			}
			if (ct.IsCancellationRequested)
				break;
		}
	}
}
