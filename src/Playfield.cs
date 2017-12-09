using System;
using System.Linq;
using System.Collections.Generic;
using Tetrix.Tetroes;
using Tetrix.UI;

namespace Tetrix
{
    public class Playfield
    {
        // Height of the playfield
        int _h = 22;

        // Width of the playfield
        int _w = 10;

        public int X { get; set; }

        public int Y { get; set; }

        Game _game;

        public Renderer Renderer { get; private set; }

        Tetro _curTetro;

        // List of all block from all tetrominoes
        IList<Block> _blocks = new List<Block>();

        public event EventHandler<RowRemovedEventArgs> RowRemoved;

        protected void OnRowRemoved(RowRemovedEventArgs e)
            => RowRemoved?.Invoke(this, e);

        public event EventHandler GameOver;

        protected void OnGameOver(EventArgs e)
             => GameOver?.Invoke(this, e);

        // Constructor
        public Playfield(int x, int y, Renderer renderer, Game game)
        {
            X = x;
            Y = y;
            Renderer = renderer;
            _game = game;
        }

        // Progresses the game - 1 move.
        public void Progress(object state)
        {
            if (_curTetro == null)
                ResetCurrentTetro(); 

            if (!_curTetro.CanMoveDown())
            {
                // Checks for full rows and removes if Any
                RemoveFullRowsIfAny();
                // Reset the current tetromino
                ResetCurrentTetro();

                // If the new tetro cannot move done - game over
                if (!_curTetro.CanMoveDown())
                {
                    OnGameOver(EventArgs.Empty);
                    GameOver = null;
                }

                return;
            }

            MoveDown();
        }

        public void Rotate()
        {
            _curTetro.BeginMutation();
            _curTetro.Rotate();
            Renderer.Mutations.Add(_curTetro.EndMupation());
        }

        internal void Start()
        {
            Renderer.Debug = _game.Settings.Debug;
            this.Render();

            bool run = true;
            while(run)
            {
                ConsoleKeyInfo input = Console.ReadKey(true);

                // Exits at game over when pressing 'Enter'
                if (_game.IsGameOver && input.Key != ConsoleKey.Enter)
                    continue;

                switch(input.Key)
                {
                    case ConsoleKey.Q:
                    case ConsoleKey.X:
                        run = false;
                        GameOver?.Invoke(this, EventArgs.Empty);
                        break;
                    case ConsoleKey.UpArrow:
                        this.Rotate();
                        break;
                    case ConsoleKey.LeftArrow:
                        this.MoveLeft();
                        break;
                    case ConsoleKey.RightArrow:
                        this.MoveRight();
                        break;
                    case ConsoleKey.DownArrow:
                        this.MoveDown();
                        break;
                    case ConsoleKey.F5:
                        this.Render();
                        break;
                    case ConsoleKey.D:
                        Renderer.Debug = !Renderer.Debug;
                        this.Render();
                        break;
                    case ConsoleKey.Enter:
                        if (_game.IsGameOver)
                            run = false;
                        break;
                }
            }
        }

        public void MoveLeft()
        {
            if (!_curTetro.CanMoveLeft())
                return;

            _curTetro.BeginMutation();
            _curTetro.MoveLeft();
            Renderer.Mutations.Add(_curTetro.EndMupation());
        }

        public void MoveRight()
        {
            if (!_curTetro.CanMoveRight())
                return;

            _curTetro.BeginMutation();
            _curTetro.MoveRight();
            Renderer.Mutations.Add(_curTetro.EndMupation());
        }

        public void MoveDown()
        {
            if (!_curTetro.CanMoveDown())
                return;

            _curTetro.BeginMutation();
            _curTetro.MoveDown();
            Renderer.Mutations.Add(_curTetro.EndMupation());
        }

        // Set current tetromino and generate the next one
        protected void ResetCurrentTetro()
        {
            _curTetro = _game.GetNextTetro();

            // Add all blocks from all tetrominoes to single List
            // for faster rendering and colision detection
            foreach (Block b in _curTetro.Blocks)
                _blocks.Add(b);
        }

        public void RemoveFullRowsIfAny()
        {
            // Check for full rows
            var rowsToRemove= new List<int>();
            for (int y = Y; y <= _h + Y; y++)
            {
                var row = _blocks.Where(b => b.Y == y);
                int count = row.Count();
                if (count == _w)
                    rowsToRemove.Add(y);
            }

            var mutation = new GridMutation();
            // If full rows remove the blocks
            foreach (int row in rowsToRemove)
            {
                // Select blocks to remove
                var blocksToRemove = new List<Block>();
                foreach(Block b in _blocks.Where(b => b.Y == row))
                    blocksToRemove.Add(b);

                // Remove blocks
                var removeMutation = new GridMutation();
                foreach(Block b in blocksToRemove)
                {
                    _blocks.Remove(b);
                    removeMutation.AddSource(b);
                }

                // Shift upper blocks down
                var shiftDownMutation = new GridMutation();
                foreach(Block b in _blocks.Where(_b => _b.Y < row))
                {
                    shiftDownMutation.AddSource(new Point(b.X, b.Y));
                    ++b.Y;
                    shiftDownMutation.AddTarget(b);
                }
                Renderer.Mutations.Add(removeMutation);
                Renderer.Mutations.Add(shiftDownMutation);
            }
            OnRowRemoved(RowRemovedEventArgs.Create(rowsToRemove.Count));
        }

        // Check if a single block location (x, y) is available/empty 
        public bool IsLocationAvailable(int x, int y)
        {   
            // check out of boundries
            if (x >= _w + X + 1 || x < X + 1 || y >= _h + Y + 1 /* || y < 1 */)
                return false;

            // check block colisions
            foreach (Block b in _blocks)
                if (!_curTetro.Blocks.Any(_b => _b == b))
                   if (b.X == x && b.Y == y)
                    return false;

            return true;
        }

        // Returns true if ALL locations are available
        public bool AreLocationAvailale(params (int, int)[] locations)
        {
            foreach ((int x, int y) in locations)
                if (!IsLocationAvailable(x, y))
                    return false;

            return true;
        }

        // Renders the entire screen
        public void Render()
        {
            Renderer.Clear();

            _game.Scoreboard.RenderScore();
            _game.Scoreboard.UpdateNextTetro(_game.NextTetro);

            var points = new List<DrawablePoint>
            {
                // Top border line
                new DrawablePoint(X, Y, '\u2554'),
                new DrawablePoint(X + 1, Y, '\u2550'),
                new DrawablePoint(X + 2, Y, '\u2550'),
                new DrawablePoint(X + 3, Y, '\u255D'),
                new DrawablePoint(X + 4, Y, ' '),
                new DrawablePoint(X + 5, Y, ' '),
                new DrawablePoint(X + 6, Y, ' '),
                new DrawablePoint(X + 7, Y, ' '),
                new DrawablePoint(X + 8, Y, '\u255A'),
                new DrawablePoint(X + 9, Y, '\u2550'),
                new DrawablePoint(X + 10, Y, '\u2550'),
                new DrawablePoint(X + 11, Y, '\u2557')
            };

            // For each row
            for (int y = Y + 1; y <= _h + Y; y++)
            {
                // Left border line
                points.Add(new DrawablePoint(X, y, '\u2551'));

                // Right border line
                points.Add(new DrawablePoint(X + _w + 1, y, '\u2551'));
            }

            // Bottom border line
            points.Add(new DrawablePoint(X, Y + _h + 1, '\u255A'));
            for(int x = X + 1; x <= _w + X + 1; x++)
                points.Add(new DrawablePoint(x, Y + _h + 1, '\u2550'));
            points.Add(new DrawablePoint(X + _w + 1, Y + _h + 1, '\u255D'));


            // Generate single mutation for blocks and playfield borders
            var mutation = new GridMutation();
            foreach (DrawablePoint p in _blocks)
                mutation.AddTarget(p);

            // Add playfield border points
            foreach (DrawablePoint p in points)
                mutation.AddTarget(p);

            Renderer.Mutations.Add(mutation);
        }

    }
}