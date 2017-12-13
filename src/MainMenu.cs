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
            _renderer.Clear();
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
                            _renderer.Render(
                                GridMutation.Create(2, 4, new DrawablePoint(2, 3, '-')));
                        }
                        else
                        {
                            currentOption = MenuOptions.Exit;
                            _renderer.Render(
                                GridMutation.Create(2, 3, new DrawablePoint(2, 4, '-')));
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