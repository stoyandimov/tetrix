using System;

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