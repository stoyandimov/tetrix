using System;
using System.Collections.Generic;
using System.Linq;

namespace Tetrix
{
    public class TetroMutation
    {
        public List<Tuple<Block, int, int>> SourcePosition { get; private set; } = new List<Tuple<Block, int, int>>();
        public List<Tuple<Block, int, int>> TargetPosition { get; private set; } = new List<Tuple<Block, int, int>>();

        // Remove all blocks mutations that won't affect the GUI
        public void RemoveRedundentBlockMutations()
        {
            
            // Make a collection of blocks that won't move in this mutation
            var redundentBlockMutations = new List<Block>();
            foreach(Tuple<Block, int, int> targetPos in TargetPosition)
            {
                redundentBlockMutations.AddRange(SourcePosition.Where(
                    s => s.Item1 == targetPos.Item1
                    && s.Item2 == targetPos.Item2
                    && s.Item3 == targetPos.Item3).Select(s => s.Item1));
            }

            // Removes blocks that won't move in this mutation
            SourcePosition.RemoveAll(s => redundentBlockMutations.Contains(s.Item1));
            TargetPosition.RemoveAll(s => redundentBlockMutations.Contains(s.Item1));
            
            // Removes source position blocks to which a target block will be written.
            // This will prevent redundent search and remove blocks            
            SourcePosition.RemoveAll(
                s => TargetPosition.Any(t => t.Item2 == s.Item2 && t.Item3 == s.Item3));
        }
    }
}