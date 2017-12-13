using System;
using System.Threading;
using Tetrix.UI;

namespace Tetrix
{
    public class Game
    {
        private readonly GameSettings _settings;

        public Game(GameSettings settings) 
            => _settings = settings;

        public void Bootstrap()
        {
            try 
            {
                var renderer = new Renderer();
                renderer.BeginRendering();
                var mainMenu = new MainMenu(renderer);
                var run = true;
                while(run)
                {
                    MenuOptions next = mainMenu.WhatsNext();
                    switch(next)
                    {
                        case MenuOptions.Exit:
                            run = false;
                            break;
                        case MenuOptions.StartGame:
                            var stage = new TetrisStage(renderer, _settings);
                            stage.Start();
                            // game finished
                            break;
                        }
                    renderer.Clear();
                }
                renderer.EndRendering();
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

        public void Shutdown()
            => Thread.Sleep(900);
    }
}