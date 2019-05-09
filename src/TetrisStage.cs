using System;
using System.Threading;
using Tetrix.Tetroes;
using Tetrix.UI;

namespace Tetrix
{
    public class TetrisStage
    {
        private readonly Playfield _playfield;
        private readonly Scoreboard _scoreboard;
        private readonly Random _randomizer;
        private readonly Renderer _renderer;
        private readonly GameSettings _settings;
        private Tetro _nextTetro;

        public TetrisStage(Renderer renderer, GameSettings settings)
        {
            _renderer = renderer;
            _settings = settings;
            _randomizer = new Random();
            _playfield = new Playfield(0, 0, renderer, this);
            _scoreboard = new Scoreboard(renderer, _playfield.X + _playfield.W + 3, _playfield.Y);
        }

        public void Render()
        {
            _scoreboard.RenderScore();
            _scoreboard.UpdateNextTetro(_nextTetro);
            _playfield.Render();
        }

        protected void RowRemovedHandler(object sender, RowRemovedEventArgs e)
        {
            var score = e.RowCount;
            if (score == 4)
                score = 8;
            _scoreboard.IncrementScore(score);
        }

        public Tetro GetNextTetro()
        {
            // If new game generate 'next' before setting 'current' tetromino
            if (_nextTetro == null)
                _nextTetro = GenerateRandomTetro();

            var curTetro = _nextTetro;
            _nextTetro = GenerateRandomTetro();

            // Update Scoreboard
            _scoreboard.UpdateNextTetro(_nextTetro);

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
            _scoreboard.RenderScore();
            _playfield.Start(_settings.Speed);
            Thread.Sleep(300);
        }
    }
}
