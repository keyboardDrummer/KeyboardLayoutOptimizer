using System;

namespace Generic.Containers.Collections.Collection
{
    public abstract class DefaultReadOnlyCollection<T> : DefaultCollection<T>
    {
        public override bool IsReadOnly
        {
            get { return true; }
        }

        public override void Add(T item)
        {
            throw new NotSupportedException();
        }

        public override void Clear()
        {
            throw new NotSupportedException();
        }

        public override bool Remove(T item)
        {
            throw new NotSupportedException();
        }
    }
}