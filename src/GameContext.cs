namespace Tetrix
{
    public class GameSettings
    {
        public bool Debug { get; private set; }

        public GameSettings(bool debug)
        {
            Debug = debug;
        }
    }
}