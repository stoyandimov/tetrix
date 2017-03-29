namespace Tetrix.Tetroes
{
    public class Z : Tetro
    {
        public Z(int x, int y, Playfield playfield)
            : base(x, y, playfield)
        {
            Color = 12;
            Type = TetroTypes.Z;
            CreateBlocks();
        }

        private void CreateBlocks()
        {
            Blocks = new Block[4] 
            {
                new Block(X + 1, Y + 0, Color, '#', '0'),
                new Block(X + 2, Y + 0, Color, '#', '1'),
                new Block(X + 2, Y + 1, Color, '#', '2'),
                new Block(X + 3, Y + 1, Color, '#', '3'),
            };
        }

        public override void Rotate()
        {
            // is vertical
            if (Blocks[2].Y == Blocks[3].Y)
            {
                if (!_playfield.AreLocationAvailale(
                    (Blocks[0].X + 2, Blocks[0].Y),
                    (Blocks[3].X, Blocks[3].Y - 2)))
                        return;
                        
                Blocks[0].X += 2;
                Blocks[3].Y -= 2;
            } 
            // is horizontal
            else
            {
                if (!_playfield.AreLocationAvailale(
                    (Blocks[0].X - 2, Blocks[0].Y),
                    (Blocks[3].X, Blocks[3].Y + 2)))
                        return;

                Blocks[0].X -= 2;
                Blocks[3].Y += 2;
            }
        }
    }
}