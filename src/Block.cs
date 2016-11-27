using System;

namespace Tetrix
{
    // Represents a location within the Console.
    public class Block 
    {
       // Color number according to System.ConsoleColor
        public int Color { get; set; } 


        // The symbol that will be outputed to the console
        public char Symbol { get; set; }
        public char Debug { get; set; }
         int _x;
        // The X (left) position
        public int X
        {
            get 
            {
                return _x;
            }
            set
            {
                if (value != _x)
                {
                    _renderer.Moves.Add(new BlockMovement()
                    {
                        OldX = _x,
                        OldY = _y,
                        NewX = value,
                        NewY = _y,
                        Block = this,
                    });
                    _x = value;
                }
            }
        }
        
        int _y;        
        // The Y (top) position
        public int Y 
        {
            get 
            {
                return _y;
            }
            set
            {
                if (value != _y)
                {
                    _renderer.Moves.Add(new BlockMovement()
                    {
                        OldX = _x,
                        OldY = _y,
                        NewX = _x,
                        NewY = value,
                        Block = this,
                    });
                    _y = value;
                }
            }
        }

        Renderer _renderer;
        
        // Constructor
        public Block(int x, int y, int color, char symbol, Renderer renderer, char debug)
        {
            _renderer = renderer;
            Symbol = symbol;
            Debug = debug;
            Y = y;
            X = x;
            Color = color;
        }
        public Block(int x, int y, int color, char symbol, Renderer renderer)
            : this(x, y, color, symbol, renderer, symbol)
        {
        }
    }
}