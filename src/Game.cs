using System;
using System.Threading;
using Tetrix.Tetroes;
using Tetrix.UI;

namespace Tetrix
{
    public class Game
    {

        public GameSettings Settings { get; set; }

        // Indicates if the game/playfield is still playable
        public bool IsGameOver { get; private set; } = false;

        // Used to generate random INTs when creating tetrominoes
        public Random Randomizer { get; private set;} = new Random();

        // The next tetromino
        public Tetro NextTetro { get; private set; }

        // Playfield - the UI box container for all tetroes
        public Playfield Playfield { get; private set; }

        // Scoreboard - the UI box container for next tetro and score
        public Scoreboard Scoreboard { get; private set; }

        // Renders tetro mutations (that is movements and rotations)
        public Renderer Renderer { get; private set; }

        // Indicate if the game is running or paused
        public bool IsPaused { get; set; }

        private Timer Timer { get; set; }

        public Game(GameSettings settings)
        {
            Settings = settings;
            Renderer = new Renderer();
            Scoreboard = new Scoreboard(Renderer);
        }

        public void Start()
        {
            try 
            {
                Renderer.BeginRendering();
                var mainMenu = new MainMenu(Renderer);

                while(!IsGameOver)
                {
                    MenuOptions next = mainMenu.WhatsNext();
                    switch(next)
                    {
                        case MenuOptions.Exit:
                            IsGameOver = true;
                            break;
                        case MenuOptions.StartGame:
                            Playfield = new Playfield(0, 0, Renderer, this);
                            Playfield.GameOver += GameOverHandler;
                            Playfield.RowRemoved += RowRemovedHandler;
                            Timer = new Timer(Playfield.Progress, null, 0, 1100 - (Settings.Speed * 100));
                            Renderer.Clear();
                            Playfield.Start(); // blocks
                            // game finished
                            Renderer.EndRendering();
                            break;
                    }
                }
                Renderer.Clear();
                Renderer.Write(0, 0, "Exiting...");
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("E: Something went wrong");
                Console.WriteLine();
                Console.WriteLine(ex);
                Console.WriteLine();
                Console.WriteLine("Press [enter] to exit;");
                Console.ReadLine();
            }
        }

        public void Shutdown()
        {
            Thread.Sleep(1500);
        }

        protected void GameOverHandler(object sender, EventArgs e)
        {
            Renderer.Mutations.Add(
                TextHelper.Write(19, 11, "Game Over"));
            Renderer.Mutations.Add(
                TextHelper.Write(13, 13, "[Press 'Enter' to exit]"));

            Timer.Dispose();
            IsGameOver = true;

            Console.SetCursorPosition(0, 27);
        }

        protected void RowRemovedHandler(object sender, EventArgs e)
        {
            Scoreboard.IncrementScore();
        }

        // Pauses the movement of tetroes
        public void Pause()
        {
            IsPaused = true;
        }

        // Switch between # and n (block index) when visualizing blocks
        public void ToggleDebug()
        {
            Settings.Debug = !Settings.Debug;
            Renderer.Debug = Settings.Debug;
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