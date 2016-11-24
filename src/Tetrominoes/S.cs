namespace Tetrix.Tetrominoes
{
    public class S : Tetromino
    {
        public S(int x, int y, Playfield playfield)
            : base(x, y, playfield)
        {
            Color = 10;
            Type = TetrominoType.S;
            CreateBlocks();
        }

        protected void CreateBlocks()
        {
            Blocks = new Block[4] 
            {
                new Block(X + 1, Y + 1, Color),
                new Block(X + 2, Y + 1, Color),
                new Block(X + 2, Y + 0, Color),
                new Block(X + 3, Y + 0, Color),
            };
        }

        public override void Rotate()
        {

        }
    }
}