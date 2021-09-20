using System;
using System.Text;
using Tetrix.UI;

namespace Tetrix
{
    public class Program
    {
        public static void Main(string[] args)
        {
            PrepareConsole();

            var inputQueue = new InputQueue();
            System.Threading.Tasks.Task.Run(() => { while (true) inputQueue.AddInput(Console.ReadKey(true).Key); });
            var settings = new GameSettings(args);
            var renderer = new Renderer(settings);
            var game = new Game(settings, renderer, inputQueue);
            game.Bootstrap(); // blocks
            game.Shutdown();

            ResetConsole();
        }

        private static void PrepareConsole()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
            Console.CancelKeyPress += (s, e) => ResetConsole();
        }

        private static void ResetConsole()
        {
            Console.Clear();
            Console.CursorVisible = true;
            Console.ResetColor();
        }
    }
}
