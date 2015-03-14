using System;
using Generic.Common;
using Generic.InputOutput.Printing.Sizable;

namespace Generic.Maths.Vectors
{
    [Serializable]
    public class FloatVector3 : DefaultObject, IPrintable
    {
        public float X { get; private set;}
        public float Y { get; private set; }
        public float Z { get; private set; }

        public FloatVector3(FloatVector3 clone) : this(clone.X,clone.Y,clone.Z)
        {
        }

        public FloatVector3(float x, float y, float z) { X = x; Y = y; Z = z; }

        public static readonly FloatVector3 Zero = new FloatVector3(0, 0, 0);
        public static readonly FloatVector3 One = new FloatVector3(0, 0, 0);

        public override bool Equals(object obj)
        {
            if (!(obj is FloatVector3)) return true;
            return Equals((FloatVector3)obj);
        }

        public float Inner(FloatVector3 b)
        {
            return X * b.X +  Y * b.Y + Z * b.Z;
        }

        public static FloatVector3 operator -(FloatVector3 a, FloatVector3 b)
        {
            return new FloatVector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);            
        }

        public static FloatVector3 operator *(FloatVector3 a, FloatVector3 b)
        {
            return new FloatVector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }

        public static FloatVector3 operator +(FloatVector3 a, FloatVector3 b)
        {
            return new FloatVector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static FloatVector3 operator *(FloatVector3 a, float b)
        {
            return new FloatVector3(a.X * b, a.Y * b, a.Z * b);
        }

        public static FloatVector3 operator /(FloatVector3 a, float b)
        {
            return new FloatVector3(a.X / b, a.Y / b, a.Z / b);
        }

        public double Length() { return Math.Sqrt(X * X + Y * Y + Z*Z); }


        public static bool operator ==(FloatVector3 left, FloatVector3 right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(FloatVector3 left, FloatVector3 right)
        {
            return !Equals(left, right);
        }

        public bool Equals(FloatVector3 other)
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
                int result = X.GetHashCode();
                result = (result * 397) ^ Y.GetHashCode();
                result = (result * 397) ^ Z.GetHashCode();
                return result;
            }
        }

        public Document Print()
        {
            return "(" + X + "," + Y + ","+ Z + ")";
        }

        public float LengthSquared()
        {
            return X * X + Y * Y + Z*Z;
        }
    }
}
