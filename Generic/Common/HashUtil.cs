using System.Collections.Generic;
using Generic.InputOutput.Printing.Sizable;

namespace Generic.Common
{
    public static class HashUtil
    {
        public static int GetOrderIndependentHashCode<T>(IEnumerable<T> source)
        {
            int hash = 0;
            foreach (T element in source)
            {
                hash = hash ^ EqualityComparer<T>.Default.GetHashCode(element);
            }
            return hash;
        }

        public static int GetOrderDependentHashCode(IList<Document> documents)
        {
            return GetOrderIndependentHashCode(documents); //XXX fix.
        }
    }
}
