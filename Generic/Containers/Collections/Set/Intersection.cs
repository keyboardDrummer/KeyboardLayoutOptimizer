using System.Collections.Generic;
using System.Linq;

namespace Generic.Containers.Collections.Set
{
    public class Intersection<T> : DefaultReadOnlySet<T>
    {
        private readonly ISet<T> a;
        private readonly ISet<T> b;

        public Intersection(ISet<T> a, ISet<T> b)
        {
            this.a = a;
            this.b = b;
        }

        public override IEnumerator<T> GetEnumerator()
        {
            return a.Where(x => b.Contains(x)).Distinct().GetEnumerator();
        }

        public override bool Contains(T item)
        {
            return a.Contains(item) && b.Contains(item);
        }
    }
}
