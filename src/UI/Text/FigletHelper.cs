using Colorful;

namespace Tetrix.UI.Text;

public class FigletWriter : ITextWriter
{
	public GridMutation WriteText(int x, int y, string text)
	{
		Figlet figlet = new Figlet();
		StyledString f = figlet.ToAscii(text);
		var mutation = new GridMutation();
		for (int _y = 0; _y < f.CharacterGeometry.GetLength(0); _y++)
			for (int _x = 0; _x < f.CharacterGeometry.GetLength(1); _x++)
				mutation.AddTarget(new DrawablePoint(_x + x, _y + y, f.CharacterGeometry[_y, _x]));

		return mutation;
	}
}
