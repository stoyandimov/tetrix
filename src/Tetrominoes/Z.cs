namespace Tetrix.Tetrominoes
{
    public class Z : Tetromino
    {
        public Z(int x, int y, Playfield playfield)
            : base(x, y, playfield)
        {
            Color = 12;
            Type = TetrominoType.Z;
            CreateBlocks();
        }

        protected void CreateBlocks()
        {
            Blocks = new Block[4] 
            {
                new Block(X + 1, Y + 0, Color),
                new Block(X + 2, Y + 0, Color),
                new Block(X + 2, Y + 1, Color),
                new Block(X + 3, Y + 1, Color),
            };
        }

        public override void Rotate()
        {

        }
    }
}