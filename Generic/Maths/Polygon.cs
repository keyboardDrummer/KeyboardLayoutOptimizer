using System;
using System.Collections.Generic;
using System.Linq;
using Generic.Cloneables;
using Generic.Containers.Collections.List;
using Generic.Maths.Lines;
using Generic.Maths.Vectors;

namespace Generic.Maths
{
    [Serializable]
    public class Polygon : IGenCloneable<Polygon>, IEquatable<Polygon>
    {
        public Polygon()
        {
            Points = new List<IntVector2>();
        }

        public Polygon(params IntVector2[] points) : this(ListUtil.New(points)) { }

        public Polygon(IList<IntVector2> points)
        {
            Points = points;
        }

        public Polygon(Polygon clone)
        {
            Points = new List<IntVector2>(clone.Points.Count);
            foreach (var point in clone.Points)
                Points.Add(point);
        }

        public IList<IntVector2> Points { get; set; }

        public Boolean Equals(Polygon poly)
        {
            for (var i = 0; i < Points.Count; i++)
            {
                if (!Points[i].Equals(poly.Points[i]))
                    return false;
            }
            return true;
        }

        public Polygon Clone()
        {
            return new Polygon(this);
        }

        public IList<IBoundedLine> GetLines()
        {
            return Points.HoldHandsCycle().SelectList(x => LineUtil.GetBoundedLine(x.Item1, x.Item2));
        }

        public Boolean IsSelfIntersecting()
        {
            return GetLines().ShakeHands().Any(edges => LineUtil.IntersectBounded(edges.Item1, edges.Item2).IsJust);
        }
    }
}