using System;
using System.Threading;
using Tetrix.UI;
using Tetrix.UI.Text;

namespace Tetrix
{
    public class InGameMenu
    {
        private readonly Renderer _renderer;
        public InGameMenu(Renderer renderer)
            => _renderer = renderer;

        public MenuOptions WhatsNext()
        {
            // Wait 100 ms before rendering menu
            // to prevent current tetro from being rendered
            var timeout = new Timer(state => {
                _renderer.Clear();
                _renderer.WriteFiglet(1, 0, "TetriS");
                _renderer.WriteText(1, 7, " - Resume");
                _renderer.WriteText(1, 8, "   Save");
                _renderer.WriteText(1, 9, "   Quit");
            }, null, 100, Timeout.Infinite);

            MenuOptions currentOption = MenuOptions.ResumeGame;

            bool run = true;
            while(run)
            {
                ConsoleKeyInfo input = Console.ReadKey(true);
                switch(input.Key)
                {
                    case ConsoleKey.Escape:
                        return MenuOptions.ResumeGame;
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.DownArrow:
                        if (input.Key == ConsoleKey.UpArrow && currentOption != MenuOptions.ResumeGame)
                            currentOption--;
                        else if (input.Key == ConsoleKey.DownArrow && currentOption != MenuOptions.QuitGame)
                            currentOption++;

                        _renderer.Clear(2, 7);
                        _renderer.Clear(2, 8);
                        _renderer.Clear(2, 9);

                        if (currentOption == MenuOptions.ResumeGame)
                            _renderer.Render(2, 7, '-');
                        else if (currentOption == MenuOptions.SaveGame)
                            _renderer.Render(2, 8, '-');
                        else
                            _renderer.Render(2, 9, '-');
                        break;
                    case ConsoleKey.Enter:
                        return currentOption;
                }
            }

            return MenuOptions.QuitGame;
        }
    }
}