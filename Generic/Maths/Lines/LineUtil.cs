using System;
using Generic.Containers.Maybes;
using Generic.Maths.Vectors;

namespace Generic.Maths.Lines
{
    public static class LineUtil
    {
        public static DoubleVector2 Parameterize(this IBoundedLine line, double distance)
        {
            return line.Start + line.Angle.OnUnitDisc() * distance;
        }

        public static IBoundedLine GetBoundedLine(DoubleVector2 firstPoint, DoubleVector2 secondPoint)
        {
            var dx = secondPoint.X - firstPoint.X;
            var dy = secondPoint.Y - firstPoint.Y;
            var yPerX = dy/dx;
            if (double.IsInfinity(yPerX))
            {
                return new BoundedVertical(firstPoint.X, firstPoint.Y, secondPoint.Y);
            }
            return new TwoPointBoundedLine(firstPoint, secondPoint);
        }

        public static Maybe<DoubleVector2> IntersectBounded(IBoundedLine first, IBoundedLine second)
        {
            var unbounded = Intersect(first, second);
            if (unbounded.IsNothing)
                return unbounded;

            var intersection = unbounded.FromJust;

            if (!IsIntersectionWithinBounds(first, intersection) || !IsIntersectionWithinBounds(second, intersection))
                return MaybeUtil.Nothing<DoubleVector2>();

            return unbounded;
        }

        private static bool IsIntersectionWithinBounds(IBoundedLine second, DoubleVector2 intersection)
        {
            return (second.LeftX < intersection.X && intersection.X < second.RightX)
                   || (second.BottomY < intersection.Y && intersection.Y < second.TopY);
        }

        public static Maybe<DoubleVector2> Intersect(ILine first, ILine second)
        {
            if (first is IVertical && second is IVertical)
                return IntersectVertical((IVertical) first, (IVertical) second);

            var secondVertical = second as IVertical;
            if (secondVertical != null)
                return IntersectVerticalAndNonVertical((INonVertical) first, secondVertical);

            var firstVertical = first as IVertical;
            if (firstVertical != null)
                return IntersectVerticalAndNonVertical((INonVertical) second, firstVertical);

            return IntersectNonVerticals((INonVertical) first, (INonVertical) second);
        }

        private static Maybe<DoubleVector2> IntersectNonVerticals(INonVertical first, INonVertical second)
        {
            var perDiff = first.DyDx - second.DyDx;

            if (perDiff == 0)
                return new Nothing<DoubleVector2>();

            var x = (second.Y0 - first.Y0)/perDiff;
            return MaybeUtil.Just(new DoubleVector2(x, first.Y(x)));
        }

        private static Maybe<DoubleVector2> IntersectVerticalAndNonVertical(INonVertical first, IVertical second)
        {
            return MaybeUtil.Just(new DoubleVector2(second.X, first.Y(second.X)));
        }

        private static Maybe<DoubleVector2> IntersectVertical(IVertical first, IVertical second)
        {
            return new Nothing<DoubleVector2>();
        }

//        public static DoubleVector2 LeftPoint(this IBoundedLine line)
//        {
//            return new DoubleVector2(line.LeftX, line.line.Y(line.LeftX));
//        }
//
//        public static DoubleVector2 RightPoint(this IBoundedLine line)
//        {
//            return new DoubleVector2(line.RightX, line.Y(line.RightX));
//        }

        public static bool IsHorizontal(this IBoundedLine line)
        {
            return line.BottomY == line.TopY;
        }

        public static ILine New(double dydx, int y0)
        {
            var x0 = y0 / dydx;
            if (double.IsInfinity(dydx))
                return new Vertical(x0);
            return new Line(dydx, y0);
        }
    }
}