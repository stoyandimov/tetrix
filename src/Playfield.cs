using System;
using System.Linq;
using System.Collections.Generic;
using Tetrix.Tetrominoes;

namespace Tetrix
{
    public class Playfield
    {
        int _w = 10;
        int _h = 22;

        Random _randomizer = new Random();

        IList<Block> _blocks = new List<Block>();

        public Tetromino CurTetromino { get; private set; }

        public Playfield()
        {
            ResetCurrentTetromino();
        }

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
                }

                // Reset the current tetromino
                ResetCurrentTetromino();
            }

            CurTetromino.MoveDown();
        }

        protected void ResetCurrentTetromino()
        {
            CurTetromino = GenerateTetromino();
            foreach (Block b in CurTetromino.Blocks)
                _blocks.Add(b);
        }

        protected Tetromino GenerateTetromino()
        {
            switch(_randomizer.Next(7))
            {
                case 0: return new I(0, 0, this);
                case 1: return new O(0, 0, this);
                case 2: return new T(0, 0, this);
                case 3: return new S(0, 0, this);
                case 4: return new Z(0, 0, this);
                case 5: return new J(0, 0, this);
                case 6: return new L(0, 0, this);
                // this is what I tested with :)
                default: return new T(0, 0, this);
            }
        }

        public bool IsLocationAvailable(int x, int y)
        {
            foreach (Block b in _blocks)
                if (!CurTetromino.Blocks.Any(_b => _b == b))
                   if (b.X == x && b.Y == y)
                    return false;

            return true;
        }

        public void Render()
        {
            var defColor = Console.ForegroundColor;
            
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
                    foreach(Block b in _blocks)
                    {
                        if (b.X == x && b.Y == y)
                        {
                            Console.ForegroundColor = (ConsoleColor)b.Color;
                            Console.Write('#');
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
            Console.WriteLine('+');
        }
    }
}