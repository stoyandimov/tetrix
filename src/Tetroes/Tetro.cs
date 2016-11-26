namespace Tetrix.Tetroes
{
    public abstract class Tetro
    {

        public TetroBlock[] Blocks { get; protected set; }        
        public int X { get; set; }
        public int Y { get; set; }
        public TetroTypes Type { get; protected set; }
        
        // Use ConsoleColors
        public int Color { get; set; }

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
            if (CanMoveLeft())
                foreach(Block b in Blocks)
                    b.X--;
        }
        public void MoveRight()
        {
            if (CanMoveRight())
                foreach(Block b in Blocks)
                    b.X++;   
        }

        public void MoveDown()
        {
            if(CanMoveDown())
                foreach(Block b in Blocks)
                    b.Y++;
        }

        protected bool CanMoveLeft()
        {
            foreach(Block b in Blocks)
            {
                // Check right boundry
                if (b.X <= 0)
                    return false;
                // Check other element
                if (!_playfield.IsLocationAvailable(b.X - 1, b.Y))
                    return false;
            }

            return true;
        }

        protected bool CanMoveRight()
        {
            foreach(Block b in Blocks)
            {
                // Check right boundry
                if (b.X >= 9) 
                    return false;
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
                // Check right boundry
                if (b.Y >= 21) 
                    return false;
                // Check other element
                if (!_playfield.IsLocationAvailable(b.X, b.Y + 1))
                    return false;
            }

            return true;
        }
    }
}