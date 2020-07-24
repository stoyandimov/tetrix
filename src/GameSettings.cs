namespace Tetrix
{
    public class GameSettings
    {
        public bool Debug { get; set; }

        public int Speed { get; set; }

        public GameSettings(string[] s)
        {
            // Default settings
            Debug = false;
            Speed = 10;

            for (int i = 0; i < s.Length; i++)
            {
                switch (s[i])
                { 
                    // Show number instead of blocks for tetroes
                    case "-d": case "--debug":
                        Debug = true;
                        break;
                    // Game speed
                    case "-s": case "--speed":
                        int speed;
                        if (int.TryParse(s[i + 1], out speed))
                        {
                            if (speed < 1 || speed > 10)
                                speed = 10;

                            Speed = speed;
                        }

                        break;
                    default:
                        break;
                }
            }
        }
    }
}