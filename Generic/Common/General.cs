using System;
using System.Collections.Generic;
using System.Linq;
using Generic.Containers.Maybes;

namespace Generic.Common
{
    public static class General
    {
        public enum WorkerState { Stopped, Working }

        public static Memory<T> Memorize<T>(this Func<T> func) where T : class
        {
            return new Memory<T>(func);
        }

        public static Maybe<long> ParseLong(this string input)
        {
            long result;
            var success = long.TryParse(input, out result);
            return success ? MaybeUtil.Just(result) : new Nothing<long>();
        }

        public static Maybe<int> ParseInt(this string input)
        {
            int result;
            var success = Int32.TryParse(input, out result);
            return success ? MaybeUtil.Just(result) : new Nothing<int>();
        }

        public static Element[] ArrayCopy<Element>(Element[] o, Func<Element, Element> cloneElement)
        {
            var na = new Element[o.Length];
            for (int i = 0; i < o.Length; i++)
            {
                na[i] = cloneElement(o[i]);
            }
            return na;
        }

        public static Element[] Fill<Element>(Element e, int size, Func<Element, Element> clone)
        {
            var result = new Element[size];
            for (int i = 0; i < size; i++) { result[i] = clone(e); }
            return result;
        }
        public static T[,] ToTwoDimensionalArray<T>(IList<T> values, int firstDimensionSize)
        {
            var secondSize = values.Count / firstDimensionSize;
            var result = new T[firstDimensionSize,secondSize];
            for(int x =0;x<firstDimensionSize;x++)
            {
                for(int y=0;y<secondSize;y++)
                {
                    result[x, y] = values[x * secondSize + y];
                }
            }
            return result;
        }

        public static T Clone<T>(this T from) where T : new()
        {
            var result = new T();
            result.CopyFrom(from);
            return result;
        }

        public static void CopyFrom<T>(this T to, T from)
        {
            CopyTo(from, to);
        }

        public static void CopyTo<T>(this T from, T to)
        {
            foreach (var pi in typeof(T).GetProperties().Where(pi => pi.CanWrite && pi.CanRead))
            {
                pi.SetValue(to, pi.GetValue(from, null), null);
            }
        }

        public static void Swap<T>(ref T x, ref T y)
        {
            T temp = x;
            x = y;
            y = temp;
        }

        public static void AbcFormulae(double kwadratic
            , double linear, double constant, out double plus, out double min)
        {
            double root = Math.Sqrt(linear * linear - 4 * kwadratic * constant);
            plus = (-linear + root) / (2*kwadratic);
            min = (-linear - root) / (2*kwadratic);
        }

        public delegate double SingleFunc(double f);
        public static double MonteCarlo(SingleFunc f, double start, double end, int n)
        {
            Random r = new Random();
            double sum = 0;
            for (int i = 0; i < n; i++)
            {
                double input = start + r.NextDouble() / end;
                sum += f(input);
            }
            return sum / n * (end - start);
        }

        public static double NewtonRootFinder(SingleFunc f, SingleFunc f2, double aprox, int n)
        {
            for (int i = 0; i < n; i++)
            {
                aprox = aprox - f(aprox) / f2(aprox);
            }
            return aprox;
        }

        public delegate double SumTerm(int n);
        public static double Sum(SumTerm term, int bottom, int top)
        {
            double result = 0;
            for (int n = bottom; n <= top; n++)
            {
                result += term(n);
            }
            return result;
        }

        public static int Faculty(int n)
        {
            int result = 1;
            while(n>1) { result *= n; n--;  }
            return result;
        }

        public static double Ei(double x, int n)
        {
            SumTerm term = n2 => Faculty(n2) / Math.Pow(-x, n2);
            return Math.Exp(x)/x*Sum(term, 0, n);
        }

        public static bool Greater<T>(T a, T b) where T : IComparable<T>
        {
            return a.CompareTo(b) == 1;
        }

        public static bool RangeOverlap<T>(Tuple<T, T> range1, Tuple<T, T> range2) where T : IComparable<T>
        {
            if (Greater(range1.Item1, range2.Item2))
                return false;

            return !Greater(range2.Item1, range1.Item2);
        }

        public static void Repeat(int amount, Action action)
        {
            for(int i=0;i<amount;i++,action())
            {
            }
        }
    }
}
