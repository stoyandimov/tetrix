using Tetrix.GameEngine.Storage;
using Tetrix.GameEngine.Tetroes;
using Tetrix.GameEngine.UI;
using Tetrix.GameEngine.UI.Text;

namespace Tetrix.GameEngine;

public class TetrisStage
{
	public Playfield Playfield { get; }
	public Scoreboard Scoreboard { get; }
	private readonly Random _randomizer;
	private readonly IRenderer _renderer;
	private readonly GameSettings _settings;
	private readonly InputQueue _inputQueue;
	private Tetro _nextTetro;

	public TetrisStage(IRenderer renderer, GameSettings settings, InputQueue inputQueue)
	{
		_renderer = renderer;
		_settings = settings;
		_inputQueue = inputQueue;
		_randomizer = new Random();
		Playfield = new Playfield(0, 0, renderer, this);
		Scoreboard = new Scoreboard(17, 2, renderer);
	}

	public void Render()
	{
		Scoreboard.RenderScore();
		Scoreboard.SetNextTetro(_nextTetro);
		Playfield.Render();
	}

	protected void RowRemovedHandler(object sender, RowRemovedEventArgs e)
	{
		var score = e.RowCount;
		if (score == 4)
			score = 8;
		Scoreboard.IncrementScore(score);
	}

	public Tetro GetNextTetro()
	{
		// If new game generate 'next' before setting 'current' tetromino
		if (_nextTetro == null)
			_nextTetro = GenerateRandomTetro();

		var curTetro = _nextTetro;
		_nextTetro = GenerateRandomTetro();

		// Update Scoreboard
		Scoreboard.SetNextTetro(_nextTetro);

		return curTetro;
	}

	// Generates a random tetro
	public Tetro GenerateRandomTetro()
	{
		TetroTypes type = (TetroTypes)_randomizer.Next(7);
		return Tetro.CreateTetro(type, Playfield);
	}

	public void Start()
	{
		_renderer.Clear();
		Playfield.RowRemoved += RowRemovedHandler;
		Playfield.Render();
		Scoreboard.RenderScore();
		var keepRuuning = true;
		using var timer = new Timer((_) => keepRuuning = Playfield.Progress(), null, 0, 1100 - (_settings.Speed * 100));
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
				case ConsoleKey.F5: Render(); break;
				case ConsoleKey.Escape:
					timer.Change(0, Timeout.Infinite);
					_renderer.WriteText(17, 13, "Game Paused");
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
		_renderer.Clear();
		Playfield.RowRemoved += RowRemovedHandler;
		var currTetro = Tetro.CreateTetro(savableData.CurrentTetro, Playfield);
		var nextTetro = Tetro.CreateTetro(savableData.NextTetro, Playfield);

		_nextTetro = nextTetro;
		// Load scoreboard
		Scoreboard.IncrementScore(savableData.Score);
		Scoreboard.SetNextTetro(nextTetro);
		Scoreboard.RenderScore();

		// Load blocks
		Playfield.SetBlocks(savableData.Blocks.Concat(currTetro.Blocks));
		Playfield.SetCurrentTetro(currTetro);
		Thread.Sleep(300);
	}
}
