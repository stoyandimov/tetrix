using System.Collections.Generic;
using System.Linq;

namespace Tetrix.UI
{
    public class GridMutation
    {
        public List<(Point P, int X, int Y)> SourcePosition { get; private set; } = new List<(Point, int, int)>();
        public List<(Point P, int X, int Y)> TargetPosition { get; private set; } = new List<(Point, int, int)>();

        // Remove all blocks mutations that won't affect the GUI
        public void RemoveRedundentBlockMutations()
        {
            // Make a collection of blocks that won't move in this mutation
            var redundentBlockMutations = new List<Point>();
            foreach((Point P, int X, int Y) targetPos in TargetPosition)
            {
                redundentBlockMutations.AddRange(SourcePosition.Where(
                    s => s.P == targetPos.P
                    && s.X == targetPos.X
                    && s.Y == targetPos.Y).Select(s => s.P));
            }

            // Removes blocks that won't move in this mutation
            SourcePosition.RemoveAll(s => redundentBlockMutations.Contains(s.P));
            TargetPosition.RemoveAll(s => redundentBlockMutations.Contains(s.P));
            
            // Removes source position blocks to which a target block will be written.
            // This will prevent redundent search and remove blocks
            SourcePosition.RemoveAll(
                s => TargetPosition.Any(t => t.X == s.X && t.Y == s.Y));
        }
    }
}