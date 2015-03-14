using System;
using System.Collections.Generic;

namespace Generic.Common
{
    public class FuncEqualityComparer<T> : IEqualityComparer<T>
    {
        readonly Func<T, T, bool> areEqual;
        readonly Func<T, int> getHashCode;

        public FuncEqualityComparer(Func<T, T, bool> areEqual, Func<T, int> getHashCode)
        {
            this.areEqual = areEqual;
            this.getHashCode = getHashCode;
        }

        public bool Equals(T x, T y)
        {
            return areEqual(x, y);
        }

        public int GetHashCode(T obj)
        {
            return getHashCode(obj);
        }
    }
}
