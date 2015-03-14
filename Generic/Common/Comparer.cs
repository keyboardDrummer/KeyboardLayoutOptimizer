using System;
using System.Collections.Generic;

namespace Generic.Common
{
    public enum Ordering { Lesser, Equal, Greater }
    class Comparer<T> : IComparer<T>
    {
        readonly Func<T, T, Ordering> compare;

        public Comparer(Func<T, T, Ordering> compare)
        {
            this.compare = compare;
        }

        public int Compare(T x, T y)
        {
            switch (compare(x, y))
            {
                case Ordering.Lesser:
                    return -1;
                case Ordering.Equal:
                    return 0;
                case Ordering.Greater:
                    return 1;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
