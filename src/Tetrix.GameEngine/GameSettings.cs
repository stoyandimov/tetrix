namespace Tetrix.GameEngine;

public class GameSettings(int speed = 10, bool debug = false)
{
	public bool Debug { get; set; } = debug;

	public int Speed { get; set; } = speed;
}
