using System;

namespace Tetrix.Tetroes
{
    public class I : Tetro
    {
        public I(int x, int y, Playfield playfield)
            : base(x, y, playfield)
        {
            Color = 11;
            Type = TetroTypes.I;
            CreateBlocks();
        }

        private void CreateBlocks()
        {
            Blocks = new Block[4] 
            {
                new Block(X + 0, Y + 0, Color, '#', '0'),
                new Block(X + 1, Y + 0, Color, '#', '1'),
                new Block(X + 2, Y + 0, Color, '#', '2'),
                new Block(X + 3, Y + 0, Color, '#', '3'),
            };
        }

        public override void Rotate()
        {
            // is vertical
            if (Blocks[0].X == Blocks[1].X)
            {
                if (!_playfield.AreLocationAvailale(
                    new Tuple<int, int>(Blocks[1].X + 1, Blocks[1].Y - 1),
                    new Tuple<int, int>(Blocks[2].X + 2, Blocks[2].Y - 2),
                    new Tuple<int, int>(Blocks[3].X + 3, Blocks[3].Y - 3)))
                        return;
                        
                Blocks[1].X += 1;
                Blocks[2].X += 2;
                Blocks[3].X += 3;

                Blocks[1].Y -= 1;
                Blocks[2].Y -= 2;
                Blocks[3].Y -= 3;
            } 
            // is horizontal
            else
            {
                if (!_playfield.AreLocationAvailale(
                    new Tuple<int, int>(Blocks[1].X - 1, Blocks[1].Y + 1),
                    new Tuple<int, int>(Blocks[2].X - 2, Blocks[2].Y + 2),
                    new Tuple<int, int>(Blocks[3].X - 3, Blocks[3].Y + 3)))
                        return;

                Blocks[1].X -= 1;
                Blocks[2].X -= 2;
                Blocks[3].X -= 3;

                Blocks[1].Y += 1;
                Blocks[2].Y += 2;
                Blocks[3].Y += 3;
            }
        }
    }
}