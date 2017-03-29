namespace Tetrix.Tetroes
{
    public class T : Tetro
    {
        public T(int x, int y, Playfield playfield)
            : base(x, y, playfield)
        {
            Color = 13;
            Type = TetroTypes.T;
            CreateBlocks();
        }

        private void CreateBlocks()
        {
            Blocks = new Block[4] 
            {
                new Block(X + 1, Y + 0, Color, '#', '0'),
                new Block(X + 0, Y + 1, Color, '#', '1'),
                new Block(X + 1, Y + 1, Color, '#', '2'),
                new Block(X + 2, Y + 1, Color, '#', '3'),
            };
        }

        public override void Rotate()
        {
            // Rotate clockwise
            // up to left
            if (Blocks[0].Y < Blocks[2].Y)
            {
                if (!_playfield.AreLocationAvailale(
                    (Blocks[1].X, Blocks[1].Y - 2),
                    (Blocks[2].X - 1, Blocks[2].Y - 1),
                    (Blocks[3].X - 2, Blocks[3].Y)))
                        return;
                        
                Blocks[1].Y -= 2;
                Blocks[2].Y -= 1;
                Blocks[2].X -= 1;
                Blocks[3].X -= 2;
            } 
            // right to bottom
            else if (Blocks[0].X > Blocks[2].X)
            {
                if (!_playfield.AreLocationAvailale(
                    (Blocks[1].X + 2, Blocks[1].Y),
                    (Blocks[2].X + 1, Blocks[2].Y - 1),
                    (Blocks[3].X, Blocks[3].Y - 2)))
                        return;

                Blocks[1].X += 2;
                Blocks[2].Y -= 1;
                Blocks[2].X += 1;
                Blocks[3].Y -= 2;
            } 
            // bottom to left
            else if (Blocks[0].Y > Blocks[2].Y)
            {
                if (!_playfield.AreLocationAvailale(
                    (Blocks[1].X, Blocks[1].Y + 2),
                    (Blocks[2].X + 1, Blocks[2].Y + 1),
                    (Blocks[3].X + 2, Blocks[3].Y)))
                        return;

                Blocks[1].Y += 2;
                Blocks[2].Y += 1;
                Blocks[2].X += 1;
                Blocks[3].X += 2;
            } 
            // left to top
            else 
            {
                if (!_playfield.AreLocationAvailale(
                    (Blocks[1].X - 2, Blocks[1].Y),
                    (Blocks[2].X - 1, Blocks[2].Y + 1),
                    (Blocks[3].X, Blocks[3].Y + 2)))
                        return;

                Blocks[1].X -= 2;
                Blocks[2].Y += 1;
                Blocks[2].X -= 1;
                Blocks[3].Y += 2;
            }
        }
    }
}