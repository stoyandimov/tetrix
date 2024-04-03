using Tetrix.Cli.UI;
using Tetrix.Cli.UI.Text;
using Tetrix.GameEngine.Storage;

namespace Tetrix.Cli;

public class Game(GameSettings settings, IRenderer renderer, InputQueue inputQueue)
{
	private readonly GameSettings _settings = settings;
	private readonly IRenderer _renderer = renderer;
	private readonly InputQueue _inputQueue = inputQueue;

	public void Bootstrap()
	{
		try
		{
			_renderer.BeginRendering();
			var mainMenu = new MainMenu(_renderer, _inputQueue);
			var run = true;
			while (run)
			{
				MenuOptions next = mainMenu.WhatsNext();
				switch (next)
				{
					case MenuOptions.StartGame:
					case MenuOptions.ResumeGame:
					case MenuOptions.Load:
						_renderer.Clear();
						var stage = new TetrisStage(_renderer, _settings, _inputQueue);
						if (next == MenuOptions.Load) stage.Load(JsonFileRepository.Load());
						stage.Start(); // blocking
						break;
					case MenuOptions.Exit:
					case MenuOptions.QuitGame:
						run = false;
						break;
				}
				_renderer.Clear();
			}
			_renderer.EndRendering();
		}
		catch (Exception ex)
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("E: Something went wrong");
			Console.WriteLine();
			Console.WriteLine(ex);
			Console.WriteLine();
			Console.WriteLine("Press [enter] to exit;");
			Console.ReadLine();
		}
	}

	public static void Shutdown() => Thread.Sleep(900);
}