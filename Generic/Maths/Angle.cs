using System;
using Generic.Maths.Vectors;
using JetBrains.Annotations;

namespace Generic.Maths
{
    [Serializable]
    [PublicAPI]
    public struct Angle //: IComparable<Angle>
    {
        private readonly double value;

        public Angle(double angle)
        {
            if (Math.Abs(angle - TwoPIdouble) > 0.01 && Math.Abs(angle - -TwoPIdouble) > 0.01)
            {
                value = (angle + TwoPIdouble)%(TwoPIdouble);
            }
            else
            {
                value = TwoPIdouble;
            }
        }

        public static Angle Zero
        {
            get { return new Angle(0); }
        }

        public static Angle TwoPI
        {
            get { return new Angle(TwoPIdouble); }
        }

        public static Angle PI
        {
            get { return new Angle(Math.PI); }
        }

        private static double TwoPIdouble
        {
            get { return 2*Math.PI; }
        }

        public static Angle HalfPI
        {
            get { return new Angle(Math.PI/2.0); }
        }

        public static Angle Asin(double value)
        {
            return new Angle(Math.Asin(value));
        }

        public double ToDouble()
        {
            return value;
        }

        public float ToFloat()
        {
            return (float) value;
        }

        public double Cos()
        {
            return Math.Cos(value);
        }

        public double Sin()
        {
            return Math.Sin(value);
        }

        public static Angle operator +(Angle angle1, Angle angle2)
        {
            return new Angle(angle1.value + angle2.value);
        }

        public static Angle operator -(Angle angle1, Angle angle2)
        {
            return new Angle(angle1.value - angle2.value);
        }

        public static Angle operator *(double factor, Angle angle1)
        {
            return new Angle(angle1.value*factor);
        }

        public static Angle operator *(Angle angle1, double factor)
        {
            return new Angle(angle1.value*factor);
        }

        public static Angle operator -(Angle angle)
        {
            return new Angle(-angle.value);
        }

        public static double operator /(Angle angle1, Angle angle2)
        {
            return angle1.value/angle2.value;
        }

        public static Angle operator /(Angle angle1, double factor)
        {
            return new Angle(angle1.value/factor);
        }

        public Boolean ClockWiseBetween(Angle angle0, Angle angle2)
        {
            return ((angle0.value < value && value < angle2.value) // 012
                    || (angle2.value < angle0.value && angle0.value < value) //201
                    || (value < angle2.value && angle2.value < angle0.value) //120
                   );
        }

        public static Boolean operator ==(Angle angle1, Angle angle2)
        {
            return 0 == angle1.CompareTo(angle2);
        }

        public static Boolean operator !=(Angle angle1, Angle angle2)
        {
            return 0 != angle1.CompareTo(angle2);
        }

        public static Boolean operator >(Angle angle1, Angle angle2)
        {
            return 1 == angle1.CompareTo(angle2);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }

        public static Boolean operator <(Angle angle1, Angle angle2)
        {
            return -1 == angle1.CompareTo(angle2);
        }

        public static Boolean operator >=(Angle angle1, Angle angle2)
        {
            int compareResult = angle1.CompareTo(angle2);
            return 1 == compareResult || 0 == compareResult;
        }

        public static Boolean operator <=(Angle angle1, Angle angle2)
        {
            int compareResult = angle1.CompareTo(angle2);
            return -1 == compareResult || 0 == compareResult;
        }

        public int CompareTo(Angle other)
        {
            if (value > other.value)
            {
                return 1;
            }
            if (Math.Abs(value - other.value) < 0.001)
            {
                return 0;
            }
            return -1;
        }

        public static explicit operator double(Angle a)
        {
            return a.ToDouble();
        }

        public double Tan()
        {
            return Math.Tan(value);
        }

        public DoubleVector2 OnUnitDisc()
        {
            return new DoubleVector2(Cos(), Sin());
        }

        public static Angle FromPoint(DoubleVector2 doubleVector2)
        {
            return new Angle(Math.Atan2(doubleVector2.Y, doubleVector2.X));
        }
    }
}