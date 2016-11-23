namespace Tetrix.Tetrominoes
{
     public abstract class Tetromino
    {
        public Block[] Blocks { get; protected set; }        
        public int X { get; set; }
        public int Y { get; set; }
        public TetrominoType Type { get; protected set; }
        
        // Use ConsoleColors
        public int Color { get; set; }

        public abstract void Rotate();
        public void MoveLeft()
        {
            foreach(Block b in Blocks)
                b.X++;
        }
        public void MoveRight()
        {
            foreach(Block b in Blocks)
                b.X--;   
        }
        public void MoveDown()
        {
            foreach(Block b in Blocks)
                b.Y++;
        }
    }
}