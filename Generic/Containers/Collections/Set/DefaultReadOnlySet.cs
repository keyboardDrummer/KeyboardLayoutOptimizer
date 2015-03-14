using System;
using System.Collections.Generic;

namespace Generic.Containers.Collections.Set
{
    public abstract class DefaultReadOnlySet<T> : DefaultSet<T>
    {
        public override void Clear()
        {
            throw new NotSupportedException();
        }

        public override bool Add(T item)
        {
            throw new NotSupportedException();
        }

        public override void UnionWith(IEnumerable<T> other)
        {
            throw new NotSupportedException();
        }

        public override void IntersectWith(IEnumerable<T> other)
        {
            throw new NotSupportedException();
        }

        public override void ExceptWith(IEnumerable<T> other)
        {
            throw new NotSupportedException();
        }

        public override void SymmetricExceptWith(IEnumerable<T> other)
        {
            throw new NotSupportedException();
        }

        public override bool Remove(T item)
        {
            throw new NotSupportedException();
        }
    }
}