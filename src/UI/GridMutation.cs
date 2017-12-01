using System.Collections.Generic;
using System.Linq;

namespace Tetrix.UI
{
    public class GridMutation
    {
        public IList<Point> SourcePosition { get; private set; } = new List<Point>();
        public IList<DrawablePoint> TargetPosition { get; private set; } = new List<DrawablePoint>();

        public void AddSource(int x, int y)
            => AddSource(new Point(x, y));

        public void AddSource(Point p)
            => SourcePosition.Add(p);

        public void AddTarget(DrawablePoint p)
            => TargetPosition.Add(p);

        // Remove all blocks mutations that won't affect the GUI
        /*
        public void RemoveRedundentBlockMutations()
        {
            // Make a collection of blocks that won't move in this mutation
            var redundentBlockMutations = new List<Point>();
            foreach(Point targetPos in TargetPosition)
                redundentBlockMutations.AddRange(SourcePosition.Where(
                    s => s== targetPos
                    && s.X == targetPos.X
                    && s.Y == targetPos.Y)
                );

            // Removes blocks that won't move in this mutation
            SourcePosition.RemoveAll(s => redundentBlockMutations.Contains(s));
            TargetPosition.RemoveAll(t => redundentBlockMutations.Contains(t));
            
            // Removes source position blocks to which a target block will be written.
            // This will prevent redundent search and remove blocks
            SourcePosition.RemoveAll(
                s => TargetPosition.Any(t => t.X == s.X && t.Y == s.Y));
        }
        */
    }
}