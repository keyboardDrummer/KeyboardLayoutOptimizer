using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Generic.InputOutput.Printing.Sizable
{
    public class PointerEqualityComparer : IEqualityComparer<object>
    {
        bool IEqualityComparer<object>.Equals(object x, object y)
        {
            return RuntimeHelpers.Equals(x, y);
        }

        public int GetHashCode(object obj)
        {
            return RuntimeHelpers.GetHashCode(obj);
        }
    }
}