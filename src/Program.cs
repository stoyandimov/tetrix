using System;
using System.Threading;
using System.Threading.Tasks;
namespace Tetrix
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try 
            {
                Run();
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("E: Something went wrong");
                Console.WriteLine();
                Console.WriteLine(ex);
                Console.WriteLine();
                Console.WriteLine("Press [enter] to exit;");
                Console.ReadLine();                
            }
        }

        public static void Run()
        {
            Console.WriteLine("Welcome to TETRIX - the cross plat tetris");
            // Present welcome message
            // Prompt for options
            // follow http://tetris.wikia.com/wiki/Tetris_Guideline

            var game = new Game();
            game.Play();
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
                    case ConsoleKey.UpArrow:
                        game.Playfield._curTetro.Rotate();
                        break;
                    case ConsoleKey.LeftArrow:
                        game.Playfield._curTetro.MoveLeft();
                        break;
                    case ConsoleKey.RightArrow:
                        game.Playfield._curTetro.MoveRight();
                        break;
                    case ConsoleKey.DownArrow:
                        game.Playfield._curTetro.MoveDown();
                        break;
                    case ConsoleKey.D:
                        game.ToggleDebug();
                        break;
                }

                //game.Playfield.Render(null);
            }
        }
    }
}
