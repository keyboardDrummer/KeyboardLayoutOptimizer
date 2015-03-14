using System;
using System.Collections.Generic;
using System.Linq;

namespace Generic.Containers.Collections.Set
{

    public class SelectDisjunct<T, T1> : DefaultReadOnlySet<T1>
    {
        readonly ISet<T> xs;
        readonly Func<T, T1> to;
        private readonly Func<T1, T> from;

        public SelectDisjunct(ISet<T> xs, Func<T, T1> to, Func<T1,T> from)
        {
            this.xs = xs;
            this.to = to;
            this.from = from;
        }

        public override int Count
        {
            get { return xs.Count; }
        }

        public override bool Contains(T1 item)
        {
            return xs.Contains(from(item));
        }

        public override IEnumerator<T1> GetEnumerator()
        {
            return xs.Select(to).GetEnumerator();
        }
    }
}
