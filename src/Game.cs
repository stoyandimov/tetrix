using System;
using System.Threading;
using System.Threading.Tasks;
using Tetrix.Tetroes;

namespace Tetrix
{
    public class Game
    {

        // Used to generate random INTs when creating tetrominoes
        public Random Randomizer { get; private set;} = new Random();

        // The next tetromino
        public Tetro NextTetro { get; private set; }


          // When set to true, displays the block's index instead of #
        public bool Debug { get; set; } = false;

        // Kees track of the removed rows
        int _score = 0;

        public Playfield Playfield { get; private set;}
        public Renderer Renderer { get; private set; }
        Timer _timer;
        Timer _renderTimer;
        public Game()
        {
            Renderer = new Renderer();
            Playfield = new Playfield(3, 3, Renderer, this);
        }

        public void Run()
        {
            // public Timer(TimerCallback callback, object state, int dueTime, int period);
        }

        public void Play()
        {
            // Timer(TimerCallback callback, object state, int dueTime, int period);
            Playfield.Progress(null);
            Playfield.Render(null);
            _timer = new Timer(state => { Playfield.Progress(state); }, null, 0, 1000);
            Task.Run(() => Renderer.ProcessUpdates());
            //_renderTimer = new Timer(Renderer.ProcessUpdates, null, 0, 1000);            
        }

        public void Stop()
        {
            _timer.Dispose();
            _timer = null;
        }

        // Switch between # and n (block index) when visualizing blocks
        public void ToggleDebug()
        {
            Debug = !Debug;
        }

        public Tetro GetNextTetro()
        {
            // If new game generate 'next' before setting 'current' tetromino
            if (NextTetro == null)
                NextTetro = GenerateRandomTetro();

            var curTetro = NextTetro;
            NextTetro = GenerateRandomTetro(); 

            return curTetro;
        }

        // Generate random tetromino
        protected Tetro GenerateRandomTetro()
        {
            switch(Randomizer.Next(7))
            {
                case 0: return new I(Playfield.X + 4, Playfield.Y + 1, Playfield);
                case 1: return new O(Playfield.X + 4, Playfield.Y + 1, Playfield);
                case 2: return new T(Playfield.X + 4, Playfield.Y + 1, Playfield);
                case 3: return new S(Playfield.X + 4, Playfield.Y + 1, Playfield);
                case 4: return new Z(Playfield.X + 4, Playfield.Y + 1, Playfield);
                case 5: return new J(Playfield.X + 4, Playfield.Y + 1, Playfield);
                case 6: return new L(Playfield.X + 4, Playfield.Y + 1, Playfield);
                // this is what I tested with :)
                default: return new T(Playfield.X + 4, Playfield.Y + 1, Playfield);
            }
        }

    }
}