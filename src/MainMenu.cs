using System;
using Tetrix.UI;
using Tetrix.UI.Text;

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
            _renderer.WriteFiglet(1, 0, "TetriS");
            _renderer.WriteText(1, 7, " - Start game");
            _renderer.WriteText(1, 8, "   Exit");

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
                                GridMutation.Create(2, 8, new DrawablePoint(2, 7, '-')));
                        }
                        else
                        {
                            currentOption = MenuOptions.Exit;
                            _renderer.Render(
                                GridMutation.Create(2, 7, new DrawablePoint(2, 8, '-')));
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