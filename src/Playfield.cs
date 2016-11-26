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

        public int X = 0;

        public int Y = 0;

        // List of all block from all tetrominoes
        IList<TetroBlock> _blocks = new List<TetroBlock>();

        Game _game;

        public Tetro _curTetro;

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
        public Playfield(int x, int y, Game game)
        {
            X = x;
            Y = y;
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
            }

            _curTetro.MoveDown();
        }

        // Set current tetromino and generate the next one
        protected void ResetCurrentTetro()
        {
            _curTetro = _game.GetNextTetro();

            // Add all blocks from all tetrominoes to single List
            // for faster rendering and colision detection
            foreach (TetroBlock b in _curTetro.Blocks)
                _blocks.Add(b);
        }

        public void RemoveFullRowsIfAny()
        {
            // Check for full rows
            var rowsToRemove= new List<int>();
            for (int y = 0; y < _h; y++)
            {
                var row = _blocks.Where(b => b.Y == y);
                int count = row.Count();
                if (count == _w)
                    rowsToRemove.Add(y);
            }

            // If full rows remove the blocks
            foreach (int row in rowsToRemove)
            {
                // Select blocks to remoev
                var blocksToRemove = new List<Block>();
                foreach(TetroBlock b in _blocks.Where(b => b.Y == row))
                    blocksToRemove.Add(b);
                    
                // Remove blocks
                foreach(TetroBlock b in blocksToRemove)
                    _blocks.Remove(b);

                // Shift upper blocks down
                foreach(TetroBlock b in _blocks.Where(_b => _b.Y < row))
                    b.Y++;

                OnRowRemoved(new EventArgs());
            }
        }
        
        

        // Check if a single block location (x, y) is available/empty 
        public bool IsLocationAvailable(int x, int y)
        {   
            // check out of boundries
            if (x >= _w || x < 0 || y >= _h /*|| y < 0*/)
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
            // Top border line
            Console.Write('+');
            for(int y = 0; y < _w; y++)
                Console.Write('-');
            Console.WriteLine('+');
            
            // For each row
            for(int y = 0; y < _h; y++)
            {
                Console.Write('|'); // Left border line
                
                // For each column
                for(int x = 0; x < _w; x++)
                {
                    bool write = false;
                    foreach(TetroBlock b in _blocks)
                    {
                        if (b.X == x && b.Y == y)
                        {
                            Console.ForegroundColor = (ConsoleColor)b.Color;
                            Console.Write(_game.Debug ? b.I.ToString() : "#");
                            Console.ResetColor();
                            write = true;
                            break;
                        }
                    }
                    if (!write)
                        Console.Write(' ');
                }

                Console.WriteLine('|'); // Right border line                
            }

            // Bottom border line
            Console.Write('+');
            for(int y = 0; y < _w; y++)
                Console.Write('-');
            Console.WriteLine('+');
        }
        
    }
}