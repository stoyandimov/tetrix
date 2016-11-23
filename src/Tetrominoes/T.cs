namespace Tetrix.Tetrominoes
{
    public class T : Tetromino
    {
        public T(int x, int y)
        {
            X = x;
            Y = y;
            Color = 13;
            Type = TetrominoType.T;
            CreateBlocks();
        }

        protected void CreateBlocks()
        {
            Blocks = new Block[4] 
            {
                new Block(X + 1, Y + 0, Color),
                new Block(X + 0, Y + 1, Color),
                new Block(X + 1, Y + 1, Color),
                new Block(X + 2, Y + 1, Color),
            };
        }

        public override void Rotate()
        {

        }
    }
}