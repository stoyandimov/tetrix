using System;
using System.Collections.Generic;
using System.Linq;

namespace Tetrix
{
    public class TetroMutation
    {
        public List<Tuple<Block, int, int>> SourcePosition { get; private set; } = new List<Tuple<Block, int, int>>();
        public List<Tuple<Block, int, int>> TargetPosition { get; private set; } = new List<Tuple<Block, int, int>>();
    
        public void RemoveRedundentBlockMutations()
        {
            var redundentBlockMutations = new List<Block>();
            foreach(Tuple<Block, int, int> targetPos in TargetPosition)
            {
                redundentBlockMutations.AddRange(SourcePosition.Where(
                    s => s.Item1 == targetPos.Item1 
                    && s.Item2 == targetPos.Item2 
                    && s.Item3 == targetPos.Item3).Select(s => s.Item1));
            }

            SourcePosition.RemoveAll(s => redundentBlockMutations.Contains(s.Item1));
            TargetPosition.RemoveAll(s => redundentBlockMutations.Contains(s.Item1));
        }
    }
}    