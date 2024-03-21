namespace Tetrix.UI;

public class SquareRenderer(GameSettings settings) : Renderer(settings)
{
	public override void Render(int x, int y, char symbol, int oldX, int oldY, int color = DrawablePoint.DefaultForeColor, char d = ' ')
	{
		var m = new GridMutation();
		m.AddSources([
			new(oldX, oldY),
			new(oldX + 1, oldY),
		]);
		m.AddTargets([
			new(x, y, color, symbol, d),
			new(x + 1, y, color, symbol, d),
		]);
		Render(m);
	}

	public override void Clear(int x, int y)
	{
		var m = new GridMutation();
		m.AddSource(x, y);
		m.AddSource(x + 1, y);
		Render(m);
	}

	public override void ProcessUpdates(CancellationToken ct)
	{
		while (true)
		{
			GridMutation m = _mutations.Take(ct);
			// Clears blocks after move
			foreach (var p in m.SourcePositions)
			{
				if (p.X < 0 || p.Y < 0)
					continue;

				Console.SetCursorPosition(p.X * 2, p.Y);
				Console.Write("  ");
			}
			// Renders the block on the new position
			foreach (var p in m.TargetPositions)
			{
				if (p.X < 0 || p.Y < 0)
					continue;

				if (Console.ForegroundColor != (ConsoleColor)p.ForeColor)
					Console.ForegroundColor = (ConsoleColor)p.ForeColor;
				var s = _settings.Debug ? p.Debug : p.Symbol;
				Console.SetCursorPosition(p.X * 2, p.Y);
				Console.Write($"{s}{s}");
			}
			if (ct.IsCancellationRequested)
				break;
		}
	}
}
