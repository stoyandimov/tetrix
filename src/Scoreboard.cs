using System;
using Tetrix.Tetroes;

namespace Tetrix
{
    public class Scoreboard
    {
        Tetro _nextTetro;
        int _score;

        public void IncrementScore()
        {
            _score++;
            RenderScore();
        }

        public void UpdateNextTetro(Tetro next)
        {
            if (_nextTetro != null)
                ClearTetro(_nextTetro);

            _nextTetro = next;
            RenderNextTetro(_nextTetro);
        }

        public void RenderScore()
        {
            Console.SetCursorPosition(15, 7);
            Console.Write($"score: {_score}");
        }

        private void RenderNextTetro(Tetro tetro)
        {
            Console.SetCursorPosition(15, 3);
            Console.Write("Next: ");
            foreach(Block b in tetro.Blocks)
            {
                Console.ForegroundColor = (ConsoleColor) b.Color;
                Console.SetCursorPosition(b.X + 20, b.Y + 5);
                Console.Write(b.Point.Symbol);
                Console.ResetColor();
            }
        }

        private void ClearTetro(Tetro tetro)
        {
            Console.SetCursorPosition(18, 7);
            foreach(Block b in tetro.Blocks)
            {
                Console.SetCursorPosition(b.X + 20, b.Y + 5);
                Console.Write(' ' );
            }
        }
    }
}