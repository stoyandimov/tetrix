using Tetrix.Tetroes;
using Tetrix.UI;
using Tetrix.UI.Text;

namespace Tetrix
{
    public class Scoreboard
    {
        Renderer _renderer;
        Tetro _nextTetro;
        int _score;
        int _x;
        int _y;

        public Scoreboard(Renderer renderer, int x, int y)
        {
            _renderer = renderer;
            _x = x;
            _y = y;
        }

        public void IncrementScore(int count)
        {
            _score += count;
            RenderScore();
        }

        public void UpdateNextTetro(Tetro next)
        {
            if (_nextTetro != null)
                _renderer.Render(ClearTetro(_nextTetro));

            _nextTetro = next;
            _renderer.Render(RenderNextTetro(_nextTetro));
        }

        public void RenderScore()
            => _renderer.WriteText(_x, _y + 9, $"score: {_score}");

        private GridMutation RenderNextTetro(Tetro tetro)
        {
            var m = (new TextWriter()).WriteText(_x, _y + 2, "next:");
            foreach(Block b in tetro.Blocks)
                m.AddTarget(new DrawablePoint(b.X + _x, b.Y + 4, b.ForeColor, b.Symbol, b.Debug));

            return m;
        }

        private GridMutation ClearTetro(Tetro tetro)
        {
            var m = new GridMutation();
            foreach(DrawablePoint b in tetro.Blocks)
                m.AddSource(b.X + _x, b.Y + 4);

            return m;
        }
    }
}