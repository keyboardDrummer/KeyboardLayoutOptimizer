using System.Collections.Generic;
using System.Linq;
using Generic.Containers.Collections.Enumerables;

namespace Generic.Containers.Collections.Set
{
    public abstract class DefaultSet<T> : DefaultEnumerable<T>, ISet<T>
    {
        public abstract void Clear();

        public abstract bool Add(T item);
        public abstract void UnionWith(IEnumerable<T> other);

        public abstract void IntersectWith(IEnumerable<T> other);

        public abstract void ExceptWith(IEnumerable<T> other);

        public abstract void SymmetricExceptWith(IEnumerable<T> other);
        public abstract bool Remove(T item);

        public virtual bool Contains(T item)
        {
            return Enumerable.Contains(this, item);
        }

        public virtual void CopyTo(T[] array, int arrayIndex)
        {
            var rator = GetEnumerator();
            while(rator.MoveNext())
            {
                array[arrayIndex] = rator.Current;
                arrayIndex++;
            }
        }

        public virtual int Count
        {
            get { return this.CustomCount(); }
        }

        public virtual bool IsReadOnly
        {
            get { return false; }
        }

        public virtual bool IsSubsetOf(IEnumerable<T> other)
        {
            return this.All(other.Contains);
        }

        public virtual bool IsSupersetOf(IEnumerable<T> other)
        {
            return other.All(Contains);
        }

        public virtual bool IsProperSupersetOf(IEnumerable<T> other)
        {
            return IsSupersetOf(other) && this.Any(x => !other.Contains(x));
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            return IsSubsetOf(other) && other.Any(x => !Contains(x));
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            return !other.Intersect(this).IsEmpty();
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            return IsSupersetOf(other) && Count == other.Count();
        }

        void ICollection<T>.Add(T item)
        {
            Add(item);
        }

        public T Min()
        {
            return Enumerable.Min(this);
        }

        public T Max()
        {
            return Enumerable.Max(this);
        }
    }
}