using System;
using Tetrix.Tetroes;
using Tetrix.UI;

namespace Tetrix
{
    public class Scoreboard
    {
        Renderer _renderer;
        Tetro _nextTetro;
        int _score;

        public Scoreboard(Renderer renderer)
        {
            _renderer = renderer;
        }

        public void IncrementScore(int count)
        {
            _score += count;
            RenderScore();
        }

        public void UpdateNextTetro(Tetro next)
        {
            if (_nextTetro != null)
                _renderer.Mutations.Add(ClearTetro(_nextTetro));

            _nextTetro = next;
            _renderer.Mutations.Add(RenderNextTetro(_nextTetro));
        }

        public void RenderScore()
        {
            var mutation = TextHelper.Write(15, 9, $"score: {_score}");
            _renderer.Mutations.Add(mutation);
        }

        private GridMutation RenderNextTetro(Tetro tetro)
        {
            var m = TextHelper.Write(15, 2, "next:");
            foreach(Block b in tetro.Blocks)
                m.AddTarget(new DrawablePoint(b.X + 11, b.Y + 4, b.ForeColor, b.Symbol, b.Debug));

            return m;
        }

        private GridMutation ClearTetro(Tetro tetro)
        {
            var m = new GridMutation();
            foreach(DrawablePoint b in tetro.Blocks)
                m.AddSource(b.X + 11, b.Y + 4);

            return m;
        }
    }
}