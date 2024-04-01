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
		Playfield = new Playfield(20, 10, new Random().Next);
		PlayfieldRenderer = new PlayfieldRenderer(0, 0, renderer, Playfield);
		Scoreboard = new Scoreboard();
	}

	public void Render()
	{
		PlayfieldRenderer.Render();
	}

	protected void RowRemovedHandler(object sender, int i)
	{
		var score = i;
		if (score == 4)
			score = 8;
		Scoreboard.IncrementScore(score);
	}

	// Generates a random tetro
	public void Start()
	{
		_renderer.Clear();
		Playfield.RowRemoved += RowRemovedHandler;
		PlayfieldRenderer.Render();
		var keepRuuning = true;
		using var timer = new Timer((_) => keepRuuning = PlayfieldRenderer.Progress(), null, 0, 1100 - (_settings.Speed * 100));
		while (keepRuuning)
		{
			ConsoleKey input = _inputQueue.GetNextInput();
			switch (input)
			{
				case ConsoleKey.Q:
				case ConsoleKey.X: keepRuuning = false; break;
				case ConsoleKey.UpArrow: PlayfieldRenderer.Rotate(); break;
				case ConsoleKey.LeftArrow: PlayfieldRenderer.MoveLeft(); break;
				case ConsoleKey.RightArrow: PlayfieldRenderer.MoveRight(); break;
				case ConsoleKey.DownArrow: PlayfieldRenderer.MoveDown(); break;
				case ConsoleKey.F5: Render(); break;
				case ConsoleKey.Escape:
					timer.Change(0, Timeout.Infinite);
					_renderer.WriteText(17, 13, "Game Paused");
					var next = new InGameMenu(_renderer, _inputQueue).WhatsNext();
					switch (next)
					{
						// case MenuOptions.SaveGame:
						// 	new JsonRepository().Save(new SavableData()
						// 	{
						// 		NextTetro = _stage.Scoreboard.NextTetro.Type,
						// 		CurrentTetro = _curTetro.Type,
						// 		Blocks = GetBlocks(),
						// 		Score = _stage.Scoreboard.GetScore(),
						// 	});
						// 	// Show game saved message for 2 seconds and quit
						// 	_renderer.WriteText(17, 15, "Game saved!");
						// 	Thread.Sleep(1000);
						// 	_renderer.WriteText(17, 15, "           ");
						// 	keepRuuning = false;
						// 	break;
						case MenuOptions.ResumeGame:
							_renderer.Clear();
							Render();
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
		// _renderer.Clear();
		// Playfield.RowRemoved += RowRemovedHandler;
		// var currTetro = Tetro.CreateTetro(savableData.CurrentTetro, Playfield);
		// var nextTetro = Tetro.CreateTetro(savableData.NextTetro, Playfield);

		// _nextTetro = nextTetro;
		// // Load scoreboard
		// Scoreboard.IncrementScore(savableData.Score);

		// // Load blocks
		// Playfield.SetBlocks(savableData.Blocks.Concat(currTetro.Blocks));
		// Playfield.SetCurrentTetro(currTetro);
		// Thread.Sleep(300);
	}
}
