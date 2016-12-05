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
            RenderScore(++_score);
        }

        public void UpdateNextTetro(Tetro next)
        {
            if (_nextTetro != null)
                ClearTetro(_nextTetro);
            
            _nextTetro = next;
            RenderNextTetro(_nextTetro);
        }

        private void RenderScore(int score)
        {
            Console.SetCursorPosition(15, 7);
            Console.Write($"score: {score}");
        }

        private void RenderNextTetro(Tetro tetro)
        {
            Console.SetCursorPosition(18, 7);
            Console.Write("Next: ");
            foreach(Block b in tetro.Blocks)
            {
                Console.ForegroundColor = (ConsoleColor) b.Color;
                Console.SetCursorPosition(b.X + 20, b.Y + 5);
                Console.Write(b.Symbol);
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