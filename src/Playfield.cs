using System;
using System.Linq;
using System.Collections.Generic;
using Tetrix.Tetrominoes;

namespace Tetrix
{
    public class Playfield
    {
        // Width of the playfield
        int _w = 10;

        // Height of the playfield
        int _h = 22;

        // Used to generate random INTs when creating tetrominoes
        Random _randomizer = new Random();

        // List of all block from all tetrominoes
        IList<Block> _blocks = new List<Block>();

        // When set to true, displays the block's index instead of #
        bool _debug = false;

        // Kees track of the removed rows
        int _score = 0;

        // The current tetromino moving within the playfield
        public Tetromino CurTetromino { get; private set; }

        // The next tetromino
        public Tetromino NextTetromino { get; private set; }

        // Constructor
        public Playfield()
        {
            // Sets current and generate next tetromino
            ResetCurrentTetromino();
        }

        // Switch between # and n (block index) when visualizing blocks
        public void ToggleDebug()
        {
            _debug = !_debug;
        }

        // Progresses the game - 1 move.
        public void Progress()
        {
            if (!CurTetromino.CanMoveDown())
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
                    foreach(Block b in _blocks.Where(b => b.Y == row))
                        blocksToRemove.Add(b);
                        
                    // Remove blocks
                    foreach(Block b in blocksToRemove)
                        _blocks.Remove(b);

                    // Shift upper blocks down
                    foreach(Block b in _blocks.Where(_b => _b.Y < row))
                        b.Y++;
                    
                    _score++;
                }

                // Reset the current tetromino
                ResetCurrentTetromino();
            }

            CurTetromino.MoveDown();
        }

        // Set current tetromino and generate the next one
        protected void ResetCurrentTetromino()
        {
            // If new game generate 'next' before setting 'current' tetromino
            if (NextTetromino == null)
                NextTetromino = GenerateRandomTetromino(); 

            CurTetromino = NextTetromino;
            NextTetromino = GenerateRandomTetromino();

            // Add all blocks from all tetrominoes to single List
            // for faster rendering and colision detection
            foreach (Block b in CurTetromino.Blocks)
                _blocks.Add(b);
        }

        // Generate random tetromino
        protected Tetromino GenerateRandomTetromino()
        {
            switch(_randomizer.Next(7))
            {
                case 0: return new I(3, 0, this);
                case 1: return new O(3, 0, this);
                case 2: return new T(3, 0, this);
                case 3: return new S(3, 0, this);
                case 4: return new Z(3, 0, this);
                case 5: return new J(3, 0, this);
                case 6: return new L(3, 0, this);
                // this is what I tested with :)
                default: return new T(3, 0, this);
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
                if (!CurTetromino.Blocks.Any(_b => _b == b))
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
        public void Render()
        {
            var defColor = Console.ForegroundColor;
            
            // Top border line
            Console.Write('+');
            for(int y = 0; y < _w; y++)
                Console.Write('-');
            Console.WriteLine(NextTetromino.Type);
            
            // For each row
            for(int y = 0; y < _h; y++)
            {
                Console.Write('|'); // Left border line
                
                // For each column
                for(int x = 0; x < _w; x++)
                {
                    bool write = false;
                    foreach(Block b in _blocks)
                    {
                        if (b.X == x && b.Y == y)
                        {
                            Console.ForegroundColor = (ConsoleColor)b.Color;
                            Console.Write(_debug ? b.I.ToString() : "#");
                            Console.ForegroundColor = defColor;
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
            Console.WriteLine(_score);
        }
    }
}