using System;
using System.Collections.Generic;
using Tetrix.Tetrominoes;

namespace Tetrix
{
    public class Playfield
    {
        int _w = 10;
        int _h = 22;

        Tetromino[] _tetrominoes;

        IList<Block> _blocks = new List<Block>();

        public Tetromino CurTetromino { get; private set; }

        public Playfield()
        {
            CurTetromino = new T(0, 0);
            _tetrominoes = new Tetromino[] 
            {
                CurTetromino,
                new O(5, 20),
                new T(7, 20),
            };

            foreach (Tetromino t in _tetrominoes)
                foreach (Block b in t.Blocks)
                    _blocks.Add(b);
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