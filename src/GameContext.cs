namespace Tetrix
{
    public class GameContext
    {
        public bool Debug { get; private set; }

        public GameContext(bool debug)
        {
            Debug = debug;
        }
    }
}