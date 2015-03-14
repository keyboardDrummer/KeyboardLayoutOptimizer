using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Generic.Common;
using Generic.Containers.Collections.Enumerables;
using Generic.Containers.Collections.Enumerators;
using Generic.Functions;

namespace Generic.Containers.Collections.List
{
    [Serializable]
    public abstract class DefaultList<A> : DefaultObject, IList<A>, IList
    {
        #region IList Members

        public int Add(object value)
        {
            Add((A)value);
            return Count - 1;
        }

        public bool Contains(object value)
        {
            return Contains((A)value);
        }

        public int IndexOf(object value)
        {
            return IndexOf((A)value);
        }

        public void Insert(int index, object value)
        {
            Insert(index,(A)value);
        }

        public void Remove(object value)
        {
            Remove((A)value);
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public object SyncRoot
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsSynchronized
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsFixedSize
        {
            get { throw new NotImplementedException(); }
        }

        object IList.this[int index]
        {
            get { return this[index]; }
            set { this[index] = (A)value; }
        }

        #endregion

        #region IList<A> Members

        public virtual IEnumerator<A> GetEnumerator()
        {
            return new EnumeratorFromList<A>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public virtual void Add(A item)
        {
            Insert(Count, item);
        }

        public abstract void Clear();

        public abstract void Insert(int index, A item);

        public abstract void RemoveAt(int index);

        public virtual bool Contains(A item)
        {
            return Enumerable.Contains(this, item);
        }

        public virtual void CopyTo(A[] array, int arrayIndex)
        {
            for (int i = 0; i < Count; i++)
                array[arrayIndex + i] = this[i];
        }

        public virtual bool Remove(A item)
        {
            int index = IndexOf(item);
            if (index == -1)
                return false;
            RemoveAt(index);
            return true;
        }

        public abstract int Count { get; }

        public virtual bool IsReadOnly
        {
            get { return false; }
        }

        public virtual int IndexOf(A item)
        {
            return this.IndexOf(item, FuncUtil.ObjectEquality<A>());
        }

        public abstract A this[int index] { get; set; }

        #endregion
    }
}
