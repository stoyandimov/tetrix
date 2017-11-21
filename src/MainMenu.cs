using System;
using Tetrix.UI;


namespace Tetrix
{
    public class MainMenu
    {
        Renderer _renderer;
        public MainMenu(Renderer renderer)
        {
            _renderer = renderer;
        }

        public MenuOptions WhatsNext()
        {
            _renderer.Write(1, 1, "Welcome to TETRIX - the console ASCII tetris");

            _renderer.Write(1, 3, " - Start game");
            _renderer.Write(1, 4, "   Exit");

            MenuOptions currentOption = MenuOptions.StartGame;

            bool run = true;
            while(run)
            {
                ConsoleKeyInfo input = Console.ReadKey(true);
                switch(input.Key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.DownArrow:
                        if (currentOption != MenuOptions.StartGame)
                        {
                            currentOption = MenuOptions.StartGame;
                            var m = new GridMutation();
                            m.SourcePosition.Add((new Point(2, 4), 2, 4));
                            m.TargetPosition.Add((new Point(2, 3) { Symbol = '-' }, 2, 3));
                            _renderer.Mutations.Add(m);
                        }
                        else
                        {
                            currentOption = MenuOptions.Exit;
                            var m = new GridMutation();
                            m.SourcePosition.Add((new Point(2, 3), 2, 3));
                            m.TargetPosition.Add((new Point(2, 4) { Symbol = '-' }, 2, 4));
                            _renderer.Mutations.Add(m);
                        }
                        break;
                    case ConsoleKey.Enter:
                        return currentOption;
                }
            }

            return MenuOptions.Exit;
        }
    }
    public enum MenuOptions
    {
        StartGame = 1,
        Exit = 2,
    }
}