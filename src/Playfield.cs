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

        public int X;

        public int Y;

        // List of all block from all tetrominoes
        IList<Block> _blocks = new List<Block>();

        Game _game;

        public Renderer Renderer { get; private set; }

        Tetro _curTetro;

        public event EventHandler RowRemoved;

        protected void OnRowRemoved(EventArgs e)
        {
            if (RowRemoved != null)
                RowRemoved(this, e);
        }

        public event EventHandler GameOver;

        protected void OnGameOver(EventArgs e)
        {
            if (GameOver != null)
                GameOver(this, e);
        }

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
                    OnGameOver(EventArgs.Empty);
                else
                    // Call progress to skip waiting for the next tetro
                    Progress(state);

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
            for (int y = 1; y <= _h; y++)
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
                    removeMutation.SourcePosition.Add((b.Point, b.X, b.Y));
                }

                // Shift upper blocks down
                var shiftDownMutation = new GridMutation();
                foreach(Block b in _blocks.Where(_b => _b.Y < row)) 
                {
                    shiftDownMutation.SourcePosition.Add((b.Point, b.X, b.Y));
                    shiftDownMutation.TargetPosition.Add((b.Point, b.X, ++b.Y));
                }
                Renderer.Mutations.Add(removeMutation);
                Renderer.Mutations.Add(shiftDownMutation);
                OnRowRemoved(EventArgs.Empty);
            }
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
            Console.Clear();
            _game.Scoreboard.RenderScore();

            var points = new List<Point>();
            // Top border line
            points.Add(new Point(X, Y) { Symbol = '+' });
            for(int x = 1; x <= _w; x++)
                points.Add(new Point(X + x, Y) { Symbol = '-' });
            points.Add(new Point(X + _w + 1, Y) { Symbol = '+' });
            
            // For each row
            for(int y = 1; y <= _h; y++)
            {
                // Left border line
                points.Add(new Point(X, Y + y) { Symbol = '|' });

                // Right border line
                points.Add(new Point(X + _w + 1, y) { Symbol = '|' });
            }

            // Bottom border line
            points.Add(new Point(X, Y + _h + 1) { Symbol = '+' });
            for(int x = 0; x < _w; x++)
                points.Add(new Point(X + x + 1, Y + _h + 1) { Symbol = '-' });
            points.Add(new Point(X + _w + 1 , Y + _h + 1) { Symbol = '+' });

            // Generate single mutation for blocks and playfield borders
            var mutation = new GridMutation();
            foreach (Block b in _blocks)
                mutation.TargetPosition.Add((b.Point, b.Point.X, b.Point.Y));

            // Add playfield border points
            foreach (Point p in points)
                mutation.TargetPosition.Add((p, p.X, p.Y));

            Renderer.Mutations.Add(mutation);
        }

    }
}