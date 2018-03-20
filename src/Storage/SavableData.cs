using System.Collections.Generic;
using Tetrix;

namespace tetrix.Storage
{
    public class SavableData
    {
        public int GameSpeed { get; set; }
        public int Score { get; set; }
        public IEnumerable<Block> Blocks { get; set; }
    }
}
