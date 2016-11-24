namespace Tetrix.Tetrominoes
{
    public class J : Tetromino
    {
        public J(int x, int y, Playfield playfield)
            : base(x, y, playfield)
        {
            Color = 9;
            Type = TetrominoType.J;
            CreateBlocks();
        }

        protected void CreateBlocks()
        {
            Blocks = new Block[4] 
            {
                new Block(X + 2, Y + 1, Color, 0),
                new Block(X + 2, Y + 2, Color, 1),
                new Block(X + 2, Y + 3, Color, 2),
                new Block(X + 1, Y + 3, Color, 3),
            };
        }

        public override void Rotate()
        {
            
        }
    }
}