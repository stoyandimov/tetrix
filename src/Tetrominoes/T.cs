namespace Tetrix.Tetrominoes
{
    public class T : Tetromino
    {
        public T(int x, int y, Playfield playfield)
            : base(x, y, playfield)
        {
            Color = 13;
            Type = TetrominoType.T;
            CreateBlocks();
        }

        protected void CreateBlocks()
        {
            Blocks = new Block[4] 
            {
                new Block(X + 1, Y + 0, Color, 0),
                new Block(X + 0, Y + 1, Color, 1),
                new Block(X + 1, Y + 1, Color, 2),
                new Block(X + 2, Y + 1, Color, 3),
            };
        }

        public override void Rotate()
        {

        }
    }
}