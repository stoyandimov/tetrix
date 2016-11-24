namespace Tetrix.Tetrominoes
{
    public class O : Tetromino
    {
        public O(int x, int y, Playfield playfield)
            : base(x, y, playfield)
        {
            X = x;
            Y = y;
            Color = 14;
            Type = TetrominoType.O;
            CreateBlocks();
        }

        protected void CreateBlocks()
        {
            Blocks = new Block[4] 
            {
                new Block(X + 0, Y + 0, Color, 0),
                new Block(X + 0, Y + 1, Color, 1),
                new Block(X + 1, Y + 0, Color, 2),
                new Block(X + 1, Y + 1, Color, 3),
            };
        }

        public override void Rotate()
        {
            // rotation has no impact on view
        }
    }
}