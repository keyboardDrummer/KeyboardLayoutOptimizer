using System;
using System.Drawing;
using Generic.Common;
using Generic.InputOutput.Printing.Sizable;
using System.Windows;
using DrawingPoint = System.Drawing.Point;
using DrawingSize = System.Drawing.Size;
namespace Generic.Maths.Vectors
{
    [Serializable]
    public class IntVector2 : DefaultObject, IPrintable
    {
        public int X { get; private set;}
        public int Y { get; private set; }

        public IntVector2(IntVector2 clone) : this(clone.X,clone.Y)
        {
        }

        public IntVector2(int x, int y) { X = x; Y = y; }

        public static readonly IntVector2 Zero = new IntVector2(0, 0);
        public static readonly IntVector2 One = new IntVector2(1, 1);

        public override bool Equals(object obj)
        {
            if (!(obj is IntVector2)) return true;
            return Equals((IntVector2) obj);
        }

        public static IntVector2 operator -(IntVector2 a, IntVector2 b)
        {
            return new IntVector2(a.X - b.X, a.Y - b.Y);            
        }

        public static IntVector2 operator *(IntVector2 a, IntVector2 b)
        {
            return new IntVector2(a.X * b.X, a.Y * b.Y);
        }

        public static IntVector2 operator +(IntVector2 a, IntVector2 b)
        {
            return new IntVector2(a.X + b.X, a.Y + b.Y);
        }

        public static IntVector2 operator *(IntVector2 a, int b)
        {
            return new IntVector2(a.X * b, a.Y * b);
        }

        public static IntVector2 operator /(IntVector2 a, int b)
        {
            return new IntVector2(a.X / b, a.Y / b);
        }

        public double Length() { return Math.Sqrt(X * X + Y * Y); }

        public static implicit operator IntVector2(DrawingPoint point)
        {
            return new IntVector2(point.X, point.Y);
        }

        public static implicit operator System.Windows.Point(IntVector2 vec)
        {
            return new System.Windows.Point(vec.X, vec.Y);
        }
        public static implicit operator DrawingPoint(IntVector2 vec)
        {
            return new DrawingPoint(vec.X, vec.Y);
        }

        public static implicit operator IntVector2(DrawingSize size)
        {
            return new IntVector2(size.Width, size.Height);
        }

        public static implicit operator DrawingSize(IntVector2 vec)
        {
            return new DrawingSize(vec.X, vec.Y);
        }

        public static implicit operator FloatVector2(IntVector2 vec)
        {
            return new FloatVector2(vec.X, vec.Y);
        }

        public static implicit operator DoubleVector2(IntVector2 vec)
        {
            return new DoubleVector2(vec.X, vec.Y);
        }

        public static implicit operator PointF(IntVector2 vec)
        {
            return new PointF(vec.X, vec.Y);
        }

        public bool Equals(IntVector2 other)
        {
            return other.X == X && other.Y == Y;
        }

        public static bool operator ==(IntVector2 left, IntVector2 right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(IntVector2 left, IntVector2 right)
        {
            return !Equals(left, right);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X*397) ^ Y;
            }
        }

        public Document Print()
        {
            return "(" + X + "," + Y + ")";
        }

        public static IntVector2 New(int a, int b)
        {
            return new IntVector2(a,b);
        }

        public int LengthSquared()
        {
            return X * X + Y * Y;
        }
    }
}
