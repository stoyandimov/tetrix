namespace Tetrix.Tetrominoes
{
    public class I : Tetromino
    {
        public I(int x, int y, Playfield playfield)
            : base(x, y, playfield)
        {
            Color = 11;
            Type = TetrominoType.I;
            CreateBlocks();
        }

        protected void CreateBlocks()
        {
            Blocks = new Block[4] 
            {
                new Block(X + 0, Y + 0, Color),
                new Block(X + 1, Y + 0, Color),
                new Block(X + 2, Y + 0, Color),
                new Block(X + 3, Y + 0, Color),
            };
        }

        public override void Rotate()
        {

        }
    }
}