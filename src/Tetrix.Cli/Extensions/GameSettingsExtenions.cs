namespace Tetrix.Cli.Extensions;

public static class GameSettingsExtensions
{
	public static GameSettings SetFromArgs(this GameSettings settings, string[] args)
	{
		for (int i = 0; i < args.Length; i++)
		{
			switch (args[i])
			{
				// Show number instead of blocks for tetroes
				case "-d":
				case "--debug":
					settings.Debug = true;
					break;
				// Game speed
				case "-s":
				case "--speed":
					int speed;
					if (int.TryParse(args[i + 1], out speed))
					{
						if (speed < 1 || speed > 10)
							speed = 10;

						settings.Speed = speed;
					}

					break;
				default:
					break;
			}
		}
		return settings;
	}
}
