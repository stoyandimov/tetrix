namespace Tetrix.Cli.UI;

public interface IRenderer
{
	void Render(PlayfieldGridMutation mutation);
	void Render(GridMutation mutation);

	void Render(int x, int y, char symbol, int color = (int) ConsoleColor.White, char d = ' ');

	void Render(int x, int y, char symbol, int oldX, int oldY, int color, char d = ' ');

	void BeginRendering();

	void Clear(int x, int y);

	void Clear();

	void EndRendering();

	void ProcessUpdates(CancellationToken cancellationToken);
}
