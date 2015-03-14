using System;
using Generic.InputOutput.Printing;
using Generic.InputOutput.Printing.Sizable;
using Generic.Maths.Vectors;

namespace Generic.Maths.Lines
{
    class TwoPointBoundedLine : IBoundedLine, INonVertical, IPrintable
    {
        private readonly DoubleVector2 left;
        private readonly DoubleVector2 right;
        readonly bool leftToRight;

        internal TwoPointBoundedLine(DoubleVector2 first, DoubleVector2 second)
        {
            leftToRight = first.X < second.X;
            if (leftToRight)
            {
                left = first;
                right = second;
            }
            else
            {
                left = second;
                right = first;
            }
        }

        public Angle Angle { get { return Angle.FromPoint(End - Start);  } }

        public double Length
        {
            get { return (End - Start).Length(); }
        }

        public double DyDx { get { return (right.Y - left.Y)/(right.X - left.X); } }

        public double Y0
        {
            get { return Y(0); }
        }

        public double Y(double x)
        {
            return DyDx*(x - left.X) + left.Y;
        }

        public double X(double y)
        {
            return (y - left.Y)/DyDx + left.X;
        }

        public DoubleVector2 Start
        {
            get { return leftToRight ? left : right; }
        }

        public DoubleVector2 End
        {
            get { return leftToRight ? right : left; }
        }

        public double LeftX
        {
            get { return left.X; }
        }

        public double RightX
        {
            get { return right.X; }
        }

        public double TopY
        {
            get { return Math.Max(left.Y,right.Y); }
        }

        public double BottomY
        {
            get { return Math.Min(left.Y,right.Y); }
        }

        public override string ToString()
        {
            return "(" + left + "," + right + ")";
        }

        public Document Print()
        {
            return (left.Print() + DocumentUtil.Comma + right.Print()).InBrackets();
        }
    }
}
