using System;
using Tetrix.UI;
using Tetrix.UI.Text;

namespace Tetrix
{
    public class MainMenu
    {
        private readonly IRenderer _renderer;
        public MainMenu(IRenderer renderer)
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
                        if (input.Key == ConsoleKey.UpArrow && currentOption != MenuOptions.StartGame)
                            currentOption--;
                        else if (input.Key == ConsoleKey.DownArrow && currentOption != MenuOptions.Exit)
                            currentOption++;

                        _renderer.Clear(2, 7);
                        _renderer.Clear(2, 8);
                        _renderer.Clear(2, 9);

                        if (currentOption == MenuOptions.StartGame)
                            _renderer.Render(2, 7, '-');
                        else if (currentOption == MenuOptions.Load)
                            _renderer.Render(2, 8, '-');
                        else
                            _renderer.Render(2, 9, '-');
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