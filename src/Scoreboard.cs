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

        public Scoreboard(Renderer renderer)
            => _renderer = renderer;

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
            => _renderer.WriteText(15, 9, $"score: {_score}");

        private GridMutation RenderNextTetro(Tetro tetro)
        {
            var m = (new TextWriter()).WriteText(15, 2, "next:");
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