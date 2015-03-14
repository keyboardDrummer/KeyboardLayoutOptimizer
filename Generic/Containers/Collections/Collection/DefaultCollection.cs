using System;
using System.Collections.Generic;
using System.Linq;
using Generic.Containers.Collections.Enumerables;

namespace Generic.Containers.Collections.Collection
{
    public abstract class DefaultCollection<T> : DefaultEnumerable<T>, ICollection<T>
    {
        public abstract void Add(T item);

        public abstract void Clear();

        public virtual bool Contains(T item)
        {
            return Enumerable.Contains(this, item);
        }

        public virtual void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public abstract bool Remove(T item);

        public virtual int Count
        {
            get { return this.Count(); }
        }

        public virtual bool IsReadOnly
        {
            get { return false; }
        }
    }
}
