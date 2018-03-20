using System;
using Tetrix.UI;
using Tetrix.UI.Text;

namespace Tetrix
{
    public class MainMenu
    {
        Renderer _renderer;
        public MainMenu(Renderer renderer)
            => _renderer = renderer;

        public MenuOptions WhatsNext()
        {
            _renderer.Clear();
            _renderer.WriteFiglet(1, 0, "TetriS");
            _renderer.WriteText(1, 7, " - Start game");
            _renderer.WriteText(1, 8, "   Load");
            _renderer.WriteText(1, 9, "   Exit");

            MenuOptions currentOption = MenuOptions.StartGame;

            bool run = true;
            while(run)
            {
                ConsoleKeyInfo input = Console.ReadKey(true);
                switch(input.Key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.DownArrow:
                        if (currentOption == MenuOptions.StartGame)
                        {
                            currentOption = MenuOptions.Load;
                            _renderer.Render(
                                GridMutation.Create(2, 7, new DrawablePoint(2, 8, '-')));
                        }
                        else if (currentOption == MenuOptions.Load)
                        {
                            currentOption = MenuOptions.Exit;
                            _renderer.Render(
                                GridMutation.Create(2, 8, new DrawablePoint(2, 9, '-')));
                        }
                        else
                        {
                            currentOption = MenuOptions.StartGame;
                            _renderer.Render(
                                GridMutation.Create(2, 9, new DrawablePoint(2, 7, '-')));
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
        // Main Menu
        StartGame = 100,
        Load = 101,
        Exit = 102,

        // In Game Menu
        ResumeGame = 200,
        SaveGame = 201,
        QuitGame = 202
    }
}