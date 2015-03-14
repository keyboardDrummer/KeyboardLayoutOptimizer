using System;
using System.Collections;
using System.Collections.Generic;

namespace Generic.Containers.Collections.Enumerators
{
    public abstract class DefaultEnumerator<T> : IEnumerator<T>
    {
        public abstract T Current { get; }

        public void Dispose()
        {
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public abstract bool MoveNext();

        public virtual void Reset()
        {
            throw new NotSupportedException();
        }
    }
}