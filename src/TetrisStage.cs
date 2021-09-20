using System;
using System.Linq;
using System.Threading;
using tetrix.Storage;
using Tetrix.Tetroes;
using Tetrix.UI;

namespace Tetrix
{
    public class TetrisStage
    {
        private readonly Playfield _playfield;
        public readonly Scoreboard Scoreboard;
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
            _playfield = new Playfield(0, 0, renderer, this);
            Scoreboard = new Scoreboard(17, 2, renderer);
        }

        public void Render()
        {
            Scoreboard.RenderScore();
            Scoreboard.SetNextTetro(_nextTetro);
            _playfield.Render();
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
            return Tetro.CreateTetro(type, _playfield);
        }

        public void Start()
        {
            _renderer.Clear();
            _playfield.RowRemoved += RowRemovedHandler;
            Scoreboard.RenderScore();
            _playfield.Start(_settings.Speed, _inputQueue);
            Thread.Sleep(300);
        }

        public void Load(SavableData savableData)
        {
            _renderer.Clear();
            _playfield.RowRemoved += RowRemovedHandler;
            var currTetro = Tetro.CreateTetro(savableData.CurrentTetro, _playfield);
            var nextTetro = Tetro.CreateTetro(savableData.NextTetro, _playfield);

            _nextTetro = nextTetro;
            // Load scoreboard
            Scoreboard.IncrementScore(savableData.Score);
            Scoreboard.SetNextTetro(nextTetro);
            Scoreboard.RenderScore();

            // Load blocks
            _playfield.SetBlocks(savableData.Blocks.Concat(currTetro.Blocks));
            _playfield.SetCurrentTetro(currTetro);
            _playfield.Start(_settings.Speed, _inputQueue);
            Thread.Sleep(300);
        }
    }
}
