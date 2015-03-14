using System;
using System.Drawing;
using Generic.Common;
using Generic.InputOutput.Printing;
using Generic.InputOutput.Printing.Sizable;

namespace Generic.Maths.Vectors
{
    public class DoubleVector2 : DefaultObject, IPrintable
    {
        readonly double x;
        readonly double y;

        public double X
        {
            get { return x; }
        }

        public double Y
        {
            get { return y; }
        }

        public DoubleVector2(double x, double y) { this.x = x; this.y = y; }

        public static DoubleVector2 Zero { get { return new DoubleVector2(0, 0); } }
        public static DoubleVector2 operator -(DoubleVector2 a, DoubleVector2 b)
        {
            return new DoubleVector2(a.X - b.X, a.Y - b.Y);            
        }

        public static DoubleVector2 operator +(DoubleVector2 a, DoubleVector2 b)
        {
            return new DoubleVector2(a.X + b.X, a.Y + b.Y);
        }

        public static DoubleVector2 operator *(DoubleVector2 a, double b)
        {
            return new DoubleVector2(a.X * b, a.Y * b);
        }

        public static implicit operator PointF(DoubleVector2 vec)
        {
            return new PointF((float)vec.X, (float)vec.Y);
        }

        public double Length() { return Math.Sqrt(X * X + Y * Y); }

        public static implicit operator DoubleVector2(PointF point)
        {
            return new DoubleVector2(point.X, point.Y);
        }

        public IntVector2 Floor()
        {
            return new IntVector2((int)X, (int)Y);
        }

        public static implicit operator DoubleVector2(SizeF size)
        {
            return new DoubleVector2(size.Width, size.Height);
        }

        public Document Print()
        {
            return "(" + X.Print() + "," + Y.Print() + ")";
        }
    }
}
