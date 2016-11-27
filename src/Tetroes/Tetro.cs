using System;

namespace Tetrix.Tetroes
{
    public abstract class Tetro
    {
        public Block[] Blocks { get; protected set; }        
        public int X { get; set; }
        public int Y { get; set; }
        public TetroTypes Type { get; protected set; }
        
        // Use ConsoleColors
        public int Color { get; set; }

        // Container for the mutation state (used by BeginMutation(), EndMupation();)
        private TetroMutation _mutationState;
        

        protected Playfield _playfield;

        public Tetro(int x, int y, Playfield playfield)
        {
            X = x;
            Y = y;
            _playfield = playfield;
        }

        public abstract void Rotate();
        public void MoveLeft()
        {
            foreach(Block b in Blocks)
                b.X--;
        }
        public void MoveRight()
        {
            foreach(Block b in Blocks)
                b.X++;   
        }

        public void MoveDown()
        {
            foreach(Block b in Blocks)
                b.Y++;
        }

        public bool CanMoveLeft()
        {
            foreach(Block b in Blocks)
            {
                // Check other element
                if (!_playfield.IsLocationAvailable(b.X - 1, b.Y))
                    return false;
            }

            return true;
        }

        public bool CanMoveRight()
        {
            foreach(Block b in Blocks)
            {
                // Check other element
                if (!_playfield.IsLocationAvailable(b.X + 1, b.Y))
                    return false;
            }

            return true;
        }

        public bool CanMoveDown()
        {
            foreach(Block b in Blocks)
            {
                // Check other element
                if (!_playfield.IsLocationAvailable(b.X, b.Y + 1))
                    return false;
            }

            return true;
        }

        // Registers the current location of the tetro blocks
        public void BeginMutation()
        {
            _mutationState = new TetroMutation();
            foreach(Block b in Blocks)
                _mutationState.SourcePosition.Add(new Tuple<Block, int, int>(b, b.X, b.Y));
        }

        // Registers the current location of the tetro blocks and returns 
        // Tetro mutation with the old block locations and the new block locations 
        public TetroMutation EndMupation()
        {
            foreach(Block b in Blocks)
                _mutationState.TargetPosition.Add(new Tuple<Block, int, int>(b, b.X, b.Y));

            return _mutationState;
        }
    }
  
}