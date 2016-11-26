using System;

namespace Tetrix.Tetroes
{
    public class L : Tetro
    {
        public L(int x, int y, Playfield playfield)
            : base(x, y, playfield)
        {
            Color = 15;
            Type = TetroTypes.L;
            CreateBlocks();
        }

        private void CreateBlocks()
        {
            Blocks = new TetroBlock[4] 
            {
                new TetroBlock(X + 1, Y + 1, Color, 0),
                new TetroBlock(X + 1, Y + 2, Color, 1),
                new TetroBlock(X + 1, Y + 3, Color, 2),
                new TetroBlock(X + 2, Y + 3, Color, 3),
            };
        }

        public override void Rotate()
        {
            // Rotate clockwise around 1

            // up to right
            if (Blocks[0].Y < Blocks[1].Y)
            {
                if (!_playfield.AreLocationAvailale(
                    new Tuple<int, int>(Blocks[0].X + 1, Blocks[0].Y + 1),
                    new Tuple<int, int>(Blocks[2].X - 1, Blocks[2].Y - 1),
                    new Tuple<int, int>(Blocks[3].X - 2, Blocks[3].Y)))
                        return;

                Blocks[0].X += 1;
                Blocks[0].Y += 1;
                Blocks[2].X -= 1;
                Blocks[2].Y -= 1;
                Blocks[3].X -= 2;
            }
            // right to bottom
            else if (Blocks[0].X > Blocks[1].X)
            {
                if (!_playfield.AreLocationAvailale(
                    new Tuple<int, int>(Blocks[0].X - 1, Blocks[0].Y + 1),
                    new Tuple<int, int>(Blocks[2].X + 1, Blocks[2].Y - 1),
                    new Tuple<int, int>(Blocks[3].X, Blocks[3].Y - 2)))
                        return;

                Blocks[0].X -= 1;
                Blocks[0].Y += 1;
                Blocks[2].X += 1;
                Blocks[2].Y -= 1;
                Blocks[3].Y -= 2;
            }
             // bottom to left
            else if (Blocks[0].Y > Blocks[1].Y)
            {
                 if (!_playfield.AreLocationAvailale(
                    new Tuple<int, int>(Blocks[0].X - 1, Blocks[0].Y + 1),
                    new Tuple<int, int>(Blocks[2].X + 1, Blocks[2].Y + 1),
                    new Tuple<int, int>(Blocks[3].X + 2, Blocks[3].Y)))
                        return;

                Blocks[0].X -= 1;
                Blocks[0].Y -= 1;
                Blocks[2].X += 1;
                Blocks[2].Y += 1;
                Blocks[3].X += 2;
            }
            // left to top
            else
            {
                 if (!_playfield.AreLocationAvailale(
                    new Tuple<int, int>(Blocks[0].X + 1, Blocks[0].Y - 1),
                    new Tuple<int, int>(Blocks[2].X - 1, Blocks[2].Y + 1),
                    new Tuple<int, int>(Blocks[3].X, Blocks[3].Y + 2)))
                        return;

                Blocks[0].X += 1;
                Blocks[0].Y -= 1;
                Blocks[2].X -= 1;
                Blocks[2].Y += 1;
                Blocks[3].Y += 2;
            }
        }
    }
}