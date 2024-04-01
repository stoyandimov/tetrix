namespace Tetrix.Cli.UI.Text;

interface ITextWriter
{
	GridMutation WriteText(int x, int y, string text);
}
