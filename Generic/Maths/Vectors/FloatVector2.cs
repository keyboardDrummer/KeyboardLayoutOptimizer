using System;
using System.Drawing;

namespace Generic.Maths.Vectors
{
    public struct FloatVector2
    {
        public float X;
        public float Y;

        public FloatVector2(float x, float y) { X = x; Y = y; }

        public static FloatVector2 Zero { get { return new FloatVector2(0, 0); } }
        public static FloatVector2 One { get { return new FloatVector2(1, 1); } }

        public static FloatVector2 operator -(FloatVector2 a, FloatVector2 b)
        {
            return new FloatVector2(a.X - b.X, a.Y - b.Y);            
        }

        public static FloatVector2 operator +(FloatVector2 a, FloatVector2 b)
        {
            return new FloatVector2(a.X + b.X, a.Y + b.Y);
        }

        public static FloatVector2 operator *(FloatVector2 a, FloatVector2 b)
        {
            return new FloatVector2(a.X * b.X, a.Y * b.Y);
        }

        public static FloatVector2 operator /(FloatVector2 a, float b)
        {
            return new FloatVector2(a.X / b, a.Y / b);
        }

        public static FloatVector2 operator *(FloatVector2 a, int b)
        {
            return new FloatVector2(a.X * b, a.Y * b);
        }

        public double Length() { return Math.Sqrt(X * X + Y * Y); }

        public static implicit operator FloatVector2(PointF point)
        {
            return new FloatVector2(point.X, point.Y);
        }

        public static implicit operator PointF(FloatVector2 vec)
        {
            return new PointF(vec.X, vec.Y);
        }

        /*
        public static implicit operator Point(FloatVector2 vec)
        {
            return new Point((int)vec.X, (int)vec.Y);
        }
        */

        public static implicit operator FloatVector2(SizeF size)
        {
            return new FloatVector2(size.Width, size.Height);
        }

        public static implicit operator SizeF(FloatVector2 vec)
        {
            return new SizeF(vec.X, vec.Y);
        }

        public override string ToString()
        {
            return "(" + X + "," + Y +")";
        }
    }
}
