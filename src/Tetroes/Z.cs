using System;

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
            Blocks = new TetroBlock[4] 
            {
                new TetroBlock(X + 1, Y + 0, Color, 0),
                new TetroBlock(X + 2, Y + 0, Color, 1),
                new TetroBlock(X + 2, Y + 1, Color, 2),
                new TetroBlock(X + 3, Y + 1, Color, 3),
            };
        }

        public override void Rotate()
        {
            // is vertical
            if (Blocks[2].Y == Blocks[3].Y)
            {
                if (!_playfield.AreLocationAvailale(
                    new Tuple<int, int>(Blocks[0].X + 2, Blocks[0].Y),
                    new Tuple<int, int>(Blocks[3].X, Blocks[3].Y - 2)))
                        return;
                        
                Blocks[0].X += 2;
                Blocks[3].Y -= 2;
            } 
            // is horizontal
            else
            {
                if (!_playfield.AreLocationAvailale(
                    new Tuple<int, int>(Blocks[0].X - 2, Blocks[0].Y),
                    new Tuple<int, int>(Blocks[3].X, Blocks[3].Y + 2)))
                        return;

                Blocks[0].X -= 2;
                Blocks[3].Y += 2;
            }
        }
    }
}