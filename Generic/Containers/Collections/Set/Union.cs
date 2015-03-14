using System.Collections.Generic;
using System.Linq;

namespace Generic.Containers.Collections.Set
{
    class Union<T> : DefaultReadOnlySet<T>
    {
        private ISet<T> a;
        private ISet<T> b;

        public Union(ISet<T> a, ISet<T> b)
        {
            this.a = a;
            this.b = b;
        }

        public override IEnumerator<T> GetEnumerator()
        {
            return a.Concat(b.Difference(a)).GetEnumerator();
        }

        public override bool Contains(T item)
        {
            return a.Contains(item) || b.Contains(item);
        }
    }
}
