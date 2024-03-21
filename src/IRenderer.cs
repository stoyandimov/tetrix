namespace Tetrix.UI;

public interface IRenderer
{
	void Render(GridMutation mutation);

	void Render(int x, int y, char symbol, int color = DrawablePoint.DefaultForeColor, char d = ' ');

	void Render(int x, int y, char symbol, int oldX, int oldY, int color = DrawablePoint.DefaultForeColor, char d = ' ');

	void BeginRendering();

	void Clear(int x, int y);

	void Clear();

	void EndRendering();

	void ProcessUpdates(CancellationToken cancellationToken);
}
