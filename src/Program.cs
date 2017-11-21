using System.Linq;

namespace Tetrix
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Show number instead of blocks for tetroes
            bool debug = args.Contains("--debug");
            var settings = new GameSettings(debug);
            var game = new Game(settings);
            game.Start();
            game.Shutdown();
            System.Console.Clear();
        }
    }
}
