using System.Collections.Generic;
using System.Linq;

namespace Generic.Containers.Collections.Set
{
    class Difference<T> : DefaultReadOnlySet<T>
    {
        private readonly ISet<T> from;
        private readonly ISet<T> without;

        public Difference(ISet<T> from, ISet<T> without)
        {
            this.from = from;
            this.without = without;
        }

        public override bool Contains(T item)
        {
            return from.Contains(item) && !without.Contains(item);
        }

        public override IEnumerator<T> GetEnumerator()
        {
            return from.Where(x => !without.Contains(x)).GetEnumerator();
        }
    }
}
