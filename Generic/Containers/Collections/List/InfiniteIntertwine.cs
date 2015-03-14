using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Generic.Containers.Collections.List
{
    [Obsolete]
    internal class InfiniteIntertwine<A> : IList<A>
    {
        private readonly IList<IList<A>> data;

        public InfiniteIntertwine(IList<IList<A>> data)
        {
            this.data = data;
        }

        // ReSharper disable FunctionNeverReturns

        #region IList<A> Members

        public IEnumerator<A> GetEnumerator()
        {
            for (int total = 0;; total++)
            {
                for (int i = 0; i < total; i++)
                {
                    yield return data[i][total - i];
                }
            }
        }

        // ReSharper restore FunctionNeverReturns

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(A item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotSupportedException();
        }

        public bool Contains(A item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(A[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(A item)
        {
            throw new NotSupportedException();
        }

        public int Count
        {
            get { return data.Sum(x => x.Count); }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public int IndexOf(A item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, A item)
        {
            throw new NotSupportedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        public A this[int index]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotSupportedException(); }
        }

        #endregion
    }
}