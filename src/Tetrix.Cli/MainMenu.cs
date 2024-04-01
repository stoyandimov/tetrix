using Tetrix.Cli.UI;
using Tetrix.Cli.UI.Text;

namespace Tetrix.Cli;

public class MainMenu(IRenderer renderer, InputQueue inputQueue)
{
	private readonly IRenderer _renderer = renderer;
	private readonly InputQueue _inputQueue = inputQueue;

	public MenuOptions WhatsNext()
	{
		Console.WriteLine("What's next...");
		_renderer.Clear();
		_renderer.WriteFiglet(1, 0, "TetriS");
		_renderer.WriteText(1, 7, " - Start game");
		_renderer.WriteText(1, 8, "   Load");
		_renderer.WriteText(1, 9, "   Exit");

		MenuOptions currentOption = MenuOptions.StartGame;

		bool run = true;
		while (run)
		{
			ConsoleKey input = _inputQueue.GetNextInput();
			switch (input)
			{
				case ConsoleKey.UpArrow:
				case ConsoleKey.DownArrow:
					if (input == ConsoleKey.UpArrow && currentOption != MenuOptions.StartGame)
						currentOption--;
					else if (input == ConsoleKey.DownArrow && currentOption != MenuOptions.Exit)
						currentOption++;

					_renderer.Clear(2, 7);
					_renderer.Clear(2, 8);
					_renderer.Clear(2, 9);

					if (currentOption == MenuOptions.StartGame)
						_renderer.Render(2, 7, '-');
					else if (currentOption == MenuOptions.Load)
						_renderer.Render(2, 8, '-');
					else
						_renderer.Render(2, 9, '-');
					break;
				case ConsoleKey.Enter:
					return currentOption;
			}
		}

		return MenuOptions.Exit;
	}
}

public enum MenuOptions
{
	// Main Menu
	StartGame = 100,
	Load = 101,
	Exit = 102,

	// In Game Menu
	ResumeGame = 200,
	SaveGame = 201,
	QuitGame = 202
}