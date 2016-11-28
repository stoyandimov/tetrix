using System;
using System.Linq;
using System.Collections.Generic;
using Tetrix.Tetroes;

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

        public event EventHandler TetroChanged;

        protected void OnTetroChanged(EventArgs e)
        {
            if (TetroChanged != null)
                TetroChanged(this, e);
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
                Console.SetCursorPosition(0, 10 + y);
                if (row.Count() > 0)
                    Console.Write("Row: " + y + " boxes: " + row.Count()); 
                int count = row.Count();
                if (count == _w)
                    rowsToRemove.Add(y);
            }

            // If full rows remove the blocks
            foreach (int row in rowsToRemove)
            {
                // Select blocks to remoev
                var blocksToRemove = new List<Block>();
                foreach(Block b in _blocks.Where(b => b.Y == row))
                    blocksToRemove.Add(b);
                    
                // Remove blocks
                foreach(Block b in blocksToRemove)
                    _blocks.Remove(b);

                // Shift upper blocks down
                foreach(Block b in _blocks.Where(_b => _b.Y < row))
                    b.Y++;

                OnRowRemoved(new EventArgs());
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
        public bool AreLocationAvailale(params Tuple<int, int>[] locations)
        {
            foreach (var pos in locations)
                if (!IsLocationAvailable(pos.Item1, pos.Item2))
                    return false;

            return true;
        }

        // Renders the entire screen
        public void Render(object state)
        {
            Console.Clear();
            // Move to playfield height
            for(int y = 0; y < Y; y++)
                Console.WriteLine();

            // Top border line
            
            Indent(X);
            Console.Write('+');
            for(int x = 0; x < _w; x++)
                Console.Write('-');
            Console.WriteLine('+');
            
            // For each row
            for(int y = 0; y < _h; y++)
            {
                Indent(X);
                Console.Write('|'); // Left border line
                
                // For each column
                for(int x = 0; x < _w; x++)
                {
                    bool write = false;
                    foreach(Block b in _blocks)
                    {
                        // +X/Y for the playfield position (+1 accounting for borders)
                        if (b.X == x + X + 1 && b.Y == y + Y + 1)
                        {
                            Console.ForegroundColor = (ConsoleColor)b.Color;
                            Console.Write(_game.Debug ? b.Debug : b.Symbol);
                            Console.ResetColor();
                            write = true;
                            break;
                        }
                    }
                    // If no block output 'empty' or if debug output y 
                    if (!write)
                    {
                        if (_game.Debug)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Write(y.ToString().Last());
                            Console.ResetColor();
                        }
                        else 
                        {
                            Console.Write(' ');
                        }

                    }
                }

                Console.WriteLine('|'); // Right border line                
            }

            // Bottom border line
            Indent(X);
            Console.Write('+');
            for(int x = 0; x < _w; x++)
                Console.Write('-');
            Console.WriteLine('+');
        }
        
        private void Indent(int i)
        {
            for(int x = 0; x < i; x++)
                Console.Write(' ');
        }

    }
}