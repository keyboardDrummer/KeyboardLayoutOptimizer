using System;
using Generic.Common;
using Generic.InputOutput.Printing.Sizable;

namespace Generic.Maths.Vectors
{
    [Serializable]
    public class IntVector3 : DefaultObject, IPrintable
    {
        public int X { get; private set;}
        public int Y { get; private set; }
        public int Z { get; private set; }

        public IntVector3(IntVector3 clone) : this(clone.X,clone.Y,clone.Z)
        {
        }

        public IntVector3(int x, int y, int z) { X = x; Y = y; Z = z; }

        public static readonly IntVector3 Zero = new IntVector3(0, 0, 0);
        public static readonly IntVector3 One = new IntVector3(0, 0, 0);

        public override bool Equals(object obj)
        {
            if (!(obj is IntVector3)) return true;
            return Equals((IntVector3)obj);
        }

        public long LongInner(IntVector3 b)
        {
            return (long)X * b.X + (long)Y * b.Y + (long)Z * b.Z;
        }

        public int Inner(IntVector3 b)
        {
            return X * b.X + Y * b.Y + Z * b.Z;
        }

        public static IntVector3 operator -(IntVector3 a, IntVector3 b)
        {
            return new IntVector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);            
        }

        public static IntVector3 operator *(IntVector3 a, IntVector3 b)
        {
            return new IntVector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }

        public static IntVector3 operator +(IntVector3 a, IntVector3 b)
        {
            return new IntVector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static IntVector3 operator *(IntVector3 a, int b)
        {
            return new IntVector3(a.X * b, a.Y * b, a.Z * b);
        }

        public static IntVector3 operator /(IntVector3 a, int b)
        {
            return new IntVector3(a.X / b, a.Y / b, a.Z / b);
        }

        public double Length() { return Math.Sqrt(X * X + Y * Y+Z*Z); }

        public static implicit operator FloatVector3(IntVector3 vec)
        {
            return new FloatVector3(vec.X, vec.Y,vec.Z);
        }

        public static bool operator ==(IntVector3 left, IntVector3 right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(IntVector3 left, IntVector3 right)
        {
            return !Equals(left, right);
        }

        public bool Equals(IntVector3 other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return other.X == X && other.Y == Y && other.Z == Z;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = X;
                result = (result * 397) ^ Y;
                result = (result * 397) ^ Z;
                return result;
            }
        }

        public Document Print()
        {
            return "(" + X + "," + Y + ","+ Z + ")";
        }

        public long LongLengthSquared()
        {
            return (long)X * X + (long)Y * Y + (long)Z * Z;
        }

        public int LengthSquared()
        {
            return X * X + Y * Y + Z*Z;
        }
    }
}
