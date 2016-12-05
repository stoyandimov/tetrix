using System;
using System.Linq;

namespace Tetrix
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try 
            {
                // Show number instead of blocks for tetroes
                bool debug = args.Contains("--debug");

                Run(debug);
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

        public static void Run(bool debug)
        {
            Console.WriteLine("Welcome to TETRIX - the console ASCII tetris");
            // Present welcome message
            // Prompt for options
            // follow http://tetris.wikia.com/wiki/Tetris_Guideline

            var game = new Game(new GameContext(debug));
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
                        game.Playfield.Rotate();
                        break;
                    case ConsoleKey.LeftArrow:
                        game.Playfield.MoveLeft();
                        break;
                    case ConsoleKey.RightArrow:
                        game.Playfield.MoveRight();
                        break;
                    case ConsoleKey.DownArrow:
                        game.Playfield.MoveDown();
                        break;
                    case ConsoleKey.D:
                        game.ToggleDebug();
                        break;
                    case ConsoleKey.F5:
                        game.Playfield.Render(null);
                        break;
                    case ConsoleKey.Spacebar:
                        if (game.IsPaused)
                            game.Play();
                        else
                            game.Pause();
                        break;
                }
            }
        }
    }
}
