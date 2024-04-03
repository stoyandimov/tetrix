using Tetrix.Cli.UI;
using Tetrix.Cli.UI.Text;
using Tetrix.GameEngine.Storage;
using Tetrix.GameEngine.Tetroes;

namespace Tetrix.Cli;

public class TetrisStage
{
	public Playfield Playfield { get; }
	public PlayfieldRenderer PlayfieldRenderer { get; }
	public Scoreboard Scoreboard { get; }
	private readonly IRenderer _renderer;
	private readonly GameSettings _settings;
	private readonly InputQueue _inputQueue;

	public TetrisStage(IRenderer renderer, GameSettings settings, InputQueue inputQueue)
	{
		_renderer = renderer;
		_settings = settings;
		_inputQueue = inputQueue;
		Scoreboard = new Scoreboard();
		Playfield = new Playfield(20, 10, () => new Random().Next(7));
		PlayfieldRenderer = new PlayfieldRenderer(0, 0, renderer, Playfield, Scoreboard);
	}

	protected void RowRemovedHandler(object sender, int i)
	{
		var score = i;
		if (score == 4)
			score *= 2;
		Scoreboard.IncrementScore(score);
		_renderer.WriteText(17, 4, $"Score: {Scoreboard.GetScore()}");
	}

	// Generates a random tetro
	public void Start()
	{
		var keepRuuning = true;
		PlayfieldRenderer.Render();
		Playfield.RowRemoved += RowRemovedHandler;
		Playfield.GameOver += (_, _) =>
		{
			keepRuuning = false;
			_renderer.WriteText(17, 13, "Game Over");
			_renderer.WriteText(17, 15, "[press a key to go to menu]");
		};
		using var timer = new Timer((_) => Playfield.Progress(), null, 0, 1100 - (_settings.Speed * 100));
		while (keepRuuning)
		{
			ConsoleKey input = _inputQueue.GetNextInput();
			switch (input)
			{
				case ConsoleKey.Q:
				case ConsoleKey.X: keepRuuning = false; break;
				case ConsoleKey.UpArrow: Playfield.Rotate(); break;
				case ConsoleKey.LeftArrow: Playfield.MoveLeft(); break;
				case ConsoleKey.RightArrow: Playfield.MoveRight(); break;
				case ConsoleKey.DownArrow: Playfield.MoveDown(); break;
				case ConsoleKey.F5: PlayfieldRenderer.Render(); break;
				case ConsoleKey.Escape:
					timer.Change(0, Timeout.Infinite);
					_renderer.WriteText(17, 13, "Game Paused");
					var next = new InGameMenu(_renderer, _inputQueue).WhatsNext();
					switch (next)
					{
						case MenuOptions.SaveGame:
							JsonFileRepository.Save(new(_settings.Speed, Scoreboard.GetScore(), Playfield.NextTetro.Type, Playfield.CurrTetro.Type, Playfield.GetBlocks()));
							// Show game saved message for 2 seconds and quit
							_renderer.WriteText(17, 15, "Game saved!");
							Thread.Sleep(1000);
							_renderer.WriteText(17, 15, "           ");
							keepRuuning = false;
							break;
						case MenuOptions.ResumeGame:
							_renderer.Clear();
							PlayfieldRenderer.Render();
							timer.Change(100, 1100 - (_settings.Speed * 100));
							break;
						case MenuOptions.QuitGame:
							keepRuuning = false;
							break;
						default: break;
					}
					break;
				case ConsoleKey.Enter:
					if (!keepRuuning)
						return;
					timer.Change(100, 1100 - (_settings.Speed * 100));
					break;
			}
		}
		timer.Change(0, Timeout.Infinite);

		Thread.Sleep(300);
	}

	public void Load(SavableData savableData)
	{
		var currTetro = Tetro.CreateTetro(savableData.CurrentTetro, Playfield);
		var nextTetro = Tetro.CreateTetro(savableData.NextTetro, Playfield);

		// Load scoreboard
		Scoreboard.IncrementScore(savableData.Score);
		RowRemovedHandler(null, 0);

		// Load blocks
		Playfield.SetBlocks(savableData.Blocks);
		Playfield.SetCurrTetro(currTetro);
		Playfield.SetNextTetro(nextTetro);
		Thread.Sleep(300);
	}
}
