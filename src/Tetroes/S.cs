namespace Tetrix.Tetroes
{
    public class S : Tetro
    {
        public S(int x, int y, Playfield playfield)
            : base(x, y, playfield)
        {
            Color = 10;
            Type = TetroTypes.S;
            CreateBlocks();
        }

        private void CreateBlocks()
        {
            Blocks = new Block[4] 
            {
                new Block(X + 2, Y + 0, Color, '#', '0'),
                new Block(X + 3, Y + 0, Color, '#', '1'),
                new Block(X + 1, Y + 1, Color, '#', '2'),
                new Block(X + 2, Y + 1, Color, '#', '3'),
            };
        }

        public override void Rotate()
        {
            // is horizontal
            if (Blocks[2].Y == Blocks[3].Y)
            {
                if (!_playfield.AreLocationAvailale(
                    (Blocks[2].X + 2, Blocks[2].Y),
                    (Blocks[3].X, Blocks[3].Y - 2)))
                        return;
                        
                Blocks[2].X += 2;
                Blocks[3].Y -= 2;
            } 
            // is vertical
            else
            {
                if (!_playfield.AreLocationAvailale(
                    (Blocks[2].X - 2, Blocks[2].Y),
                    (Blocks[3].X, Blocks[3].Y + 2)))
                        return;

                Blocks[2].X -= 2;
                Blocks[3].Y += 2;
            }
        }
    }
}