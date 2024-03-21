using Tetrix.Storage;
using Tetrix.UI;

namespace Tetrix;

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
					case MenuOptions.Exit:
						run = false;
						break;
					case MenuOptions.Load:
						var stage = new TetrisStage(_renderer, _settings, _inputQueue);
						stage.Load(JsonRepository.Load());
						// game finished
						break;
					case MenuOptions.StartGame:
						stage = new TetrisStage(_renderer, _settings, _inputQueue);
						stage.Start();
						// game finished
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

	public void Shutdown()
		=> Thread.Sleep(900);
}