using System.Collections.Generic;
using System.Linq;

namespace Tetrix.UI
{
    public class GridMutation
    {
        public List<Point> SourcePositions { get; private set; } = new List<Point>();
        public List<DrawablePoint> TargetPositions { get; private set; } = new List<DrawablePoint>();

        public void AddSource(int x, int y)
            => AddSource(new Point(x, y));

        public void AddSource(Point p)
            => SourcePositions.Add(p);

        public void AddSources(IEnumerable<Point> points)
            => SourcePositions.AddRange(points);

        public void AddTarget(DrawablePoint p)
            => TargetPositions.Add(p);

        public void AddTargets(IEnumerable<DrawablePoint> points)
            => TargetPositions.AddRange(points);


        public static GridMutation Create(int x, int y, DrawablePoint target)
        {
            var m = new GridMutation();
            m.AddSource(x, y);
            m.AddTarget(target);

            return m;
        }

        public static GridMutation Create(Point source, DrawablePoint target)
        {
            var m = new GridMutation();
            m.AddSource(source);
            m.AddTarget(target);

            return m;
        }

        public static GridMutation Create(IEnumerable<Point> sources, IEnumerable<DrawablePoint> targets)
        {
            var m = new GridMutation();
            m.AddSources(sources);
            m.AddTargets(targets);

            return m;
        }

    }
}