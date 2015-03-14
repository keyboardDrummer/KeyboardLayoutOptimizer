using System.Collections.Generic;
using System.Linq;
using Generic.Common;

namespace Generic.Containers.Collections.Enumerables
{
    public class EnumerableEqualityComparer<T> : IEqualityComparer<IEnumerable<T>>
    {
        public bool Equals(IEnumerable<T> x, IEnumerable<T> y)
        {
            return x.SequenceEqual(y);
        }

        public int GetHashCode(IEnumerable<T> obj)
        {
            return HashUtil.GetOrderIndependentHashCode(obj); //XXX fix order.
        }
    }
}
