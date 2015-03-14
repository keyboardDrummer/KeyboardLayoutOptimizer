using System;
using Generic.Maths.Vectors;

namespace Generic.Maths.Lines
{
    public class BoundedVertical : Vertical, IBoundedLine
    {
        private readonly double yBottom;
        private readonly double yTop;
        readonly bool goingUp;

        public BoundedVertical(double x, double firstY, double secondY) : base(x)
        {
            goingUp = firstY < secondY;
            var maxY = Math.Max(firstY, secondY);
            var minY = Math.Min(firstY, secondY);
            this.yTop = maxY;
            this.yBottom = minY;
        }

        #region IBoundedLine Members

        public DoubleVector2 Start
        {
            get { return new DoubleVector2(X, goingUp ? yBottom : yTop); }
        }

        public DoubleVector2 End
        {
            get { return new DoubleVector2(X, goingUp ? yTop : yBottom); }
        }

        public double LeftX
        {
            get { return X; }
        }

        public double RightX
        {
            get { return X; }
        }

        public double TopY
        {
            get { return yTop; }
        }

        public double BottomY
        {
            get { return yBottom; }
        }

        public Angle Angle
        {
            get { return goingUp ? Angle.HalfPI : Angle.HalfPI + Angle.PI; }
        }

        public double Length
        {
            get { return yTop - yBottom; }
        }

        #endregion

        public override string ToString()
        {
            return "Vertical, x = " + X;
        }
    }
}