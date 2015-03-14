using System;
using System.Collections.Generic;

namespace Generic.Containers.Collections.Enumerables
{
    class FromEnumeratorFunc<T> : DefaultEnumerable<T>
    {
        private readonly Func<IEnumerator<T>> f;

        public FromEnumeratorFunc(Func<IEnumerator<T>> f)
        {
            this.f = f;
        }

        public override IEnumerator<T> GetEnumerator()
        {
            return f();
        }
    }
}
