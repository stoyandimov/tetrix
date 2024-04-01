using Tetrix.Cli.UI;
using Tetrix.Cli.UI.Text;

namespace Tetrix.Cli;

public class InGameMenu(IRenderer renderer, InputQueue inputQueue)
{
	private readonly IRenderer _renderer = renderer;
	private readonly InputQueue _inputQueue = inputQueue;

	public MenuOptions WhatsNext()
	{
		// Wait 100 ms before rendering menu
		// to prevent current tetro from being rendered
		var timeout = new Timer(state =>
		{
			_renderer.Clear();
			_renderer.WriteFiglet(1, 0, "TetriS");
			_renderer.WriteText(1, 7, " - Resume");
			_renderer.WriteText(1, 8, "   Save");
			_renderer.WriteText(1, 9, "   Quit");
		}, null, 100, Timeout.Infinite);

		MenuOptions currentOption = MenuOptions.ResumeGame;

		bool run = true;
		while (run)
		{
			ConsoleKey input = _inputQueue.GetNextInput();
			switch (input)
			{
				case ConsoleKey.Escape:
					return MenuOptions.ResumeGame;
				case ConsoleKey.UpArrow:
				case ConsoleKey.DownArrow:
					if (input == ConsoleKey.UpArrow && currentOption != MenuOptions.ResumeGame)
						currentOption--;
					else if (input == ConsoleKey.DownArrow && currentOption != MenuOptions.QuitGame)
						currentOption++;

					_renderer.Clear(2, 7);
					_renderer.Clear(2, 8);
					_renderer.Clear(2, 9);

					if (currentOption == MenuOptions.ResumeGame)
						_renderer.Render(2, 7, '-');
					else if (currentOption == MenuOptions.SaveGame)
						_renderer.Render(2, 8, '-');
					else
						_renderer.Render(2, 9, '-');
					break;
				case ConsoleKey.Enter:
					return currentOption;
			}
		}

		return MenuOptions.QuitGame;
	}
}
