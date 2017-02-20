using System;
using System.Threading;
using System.Threading.Tasks;
using Tetrix.Tetroes;
using Tetrix.UI;

namespace Tetrix
{
    public class Game
    {

        // Used to generate random INTs when creating tetrominoes
        public Random Randomizer { get; private set;} = new Random();

        // The next tetromino
        public Tetro NextTetro { get; private set; }

        // When set to true, displays the block's index instead of #
        public bool Debug { get; private set; }
        
        // Playfield - the UI box container for all tetroes
        public Playfield Playfield { get; private set; }

        // Scoreboard - the UI box container for next tetro and score
        public Scoreboard Scoreboard { get; private set; }

        // Renders tetro mutations (that is movements and rotations)
        public Renderer Renderer { get; private set; }

        // Indicate if the game is running or paused
        public bool IsPaused { get; set; }

        // Kees track of the removed rows
        int _score = 0;

        // Timer keeping the game speed
        Timer _timer;

        public Game(GameContext ctx)
        {
            Debug = ctx.Debug;
            Renderer = new Renderer(Debug);
            Scoreboard = new Scoreboard();
            Playfield = new Playfield(0, 0, Renderer, this);
            Playfield.GameOver += GameOverHandler;
            Playfield.RowRemoved += RowRemovedHandler;
        }
        
        protected void GameOverHandler(object sender, EventArgs e)
        {
            _timer.Dispose();
            _timer = null;
            cts.Cancel();
            Console.SetCursorPosition(15, 11);
            Console.WriteLine("Game Over");
            Console.SetCursorPosition(0, 27);
        }

        protected void RowRemovedHandler(object sender, EventArgs e)
        {
            Scoreboard.IncrementScore();
        }

        // Starts the movement of tetroes
        public void Play()
        {
            _timer = new Timer(state => { Playfield.Progress(state); }, null, 0, 1000);
            Playfield.Render(null);


            cts = new CancellationTokenSource();
            Task.Run(() => Renderer.ProcessUpdates(cts.Token));

            IsPaused = false;
        }
        public CancellationTokenSource cts;

        // Pauses the movement of tetroes
        public void Pause()
        {
            IsPaused = true;
            _timer.Dispose();
            _timer = null;
        }

        // Switch between # and n (block index) when visualizing blocks
        public void ToggleDebug()
        {
            Debug = !Debug;
            Renderer.Debug = Debug;
        }

        public Tetro GetNextTetro()
        {
            // If new game generate 'next' before setting 'current' tetromino
            if (NextTetro == null)
                NextTetro = GenerateRandomTetro();

            var curTetro = NextTetro;
            NextTetro = GenerateRandomTetro(); 

            // Update Scoreboard
            Scoreboard.UpdateNextTetro(NextTetro);
            //Renderer.Mutations.Add(m);

            return curTetro;
        }

        // Generates a random tetro
        public Tetro GenerateRandomTetro()
        {
            TetroTypes type = (TetroTypes) Randomizer.Next(7);
            return Tetro.CreateTetro(type, Playfield);
        }
    }
}