using System;
using System.Threading;
using System.Threading.Tasks;
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

            RunAsync().Wait();
        }

        public static async Task RunAsync()
        {
            try
            {
                await RunInternalAsyn();
            } 
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex);
            }
        }

        public static async Task RunInternalAsyn()
        {
            var playField = new Playfield();

            var animationLoopThread = new Thread(() =>{
                while(true)
                {
                    Console.Clear();
                    playField.Render();
                    Thread.Sleep(750);
                }
            });

            var gameLoopThread = new Thread(() => {
                while(true)
                {
                    playField.Progress();
                    Thread.Sleep(750);   
                }
            });

            gameLoopThread.Start();
            animationLoopThread.Start();

            bool run = true;
            while(run)
            {
                ConsoleKeyInfo input = Console.ReadKey();
                switch(input.Key)
                {
                    case ConsoleKey.Q:
                    case ConsoleKey.X:
                        run = false;
                        break;
                    case ConsoleKey.LeftArrow:
                        playField.CurTetromino.MoveLeft();
                        break;
                    case ConsoleKey.RightArrow:
                        playField.CurTetromino.MoveRight();
                        break;
                    case ConsoleKey.DownArrow:
                        playField.CurTetromino.MoveDown();
                        break;
                }
                Console.Clear();
                playField.Render();
            }
            
            Console.Clear();
        }
    }
}
