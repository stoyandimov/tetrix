using System;
using System.Threading;
namespace Tetrix
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to TETRIX - the cross plat tetris");
            // Present welcome message
            // Prompt for options
            // follow http://tetris.wikia.com/wiki/Tetris_Guideline

            bool run = true;
            var playField = new Playfield();
            while(run)
            {
                Console.Clear();
                playField.Render();
                playField.CurTetromino.MoveDown();
                Thread.Sleep(1000);
            }
        }
    }
}
