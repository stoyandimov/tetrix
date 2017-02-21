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

        public void IncrementScore()
        {
            _score++;
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
            var mutation = TextHelper.Write(15, 7, $"score: {_score}");
            _renderer.Mutations.Add(mutation);
        }

        private GridMutation RenderNextTetro(Tetro tetro)
        {
            var m = TextHelper.Write(15, 3, "next:");
            foreach(Block b in tetro.Blocks)
                m.TargetPosition.Add(new Tuple<Point, int, int>(
                        new Point(b.X + 17, b.Y + 3) { Symbol = b.Symbol, ForeColor = b.Point.ForeColor }, b.X + 17, b.Y + 2
                    ));
            return m;
        }

        private GridMutation ClearTetro(Tetro tetro)
        {
            var m = new GridMutation();
            foreach(Block b in tetro.Blocks)
                m.SourcePosition.Add(new Tuple<Point, int, int>(
                        new Point(b.X + 17, b.Y + 3) { Symbol = b.Symbol }, b.X + 17, b.Y + 2
                    ));
            return m;
        }
    }
}