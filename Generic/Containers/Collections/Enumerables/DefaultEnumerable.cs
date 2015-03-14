using System;
using System.Collections;
using System.Collections.Generic;
using Generic.Common;

namespace Generic.Containers.Collections.Enumerables
{
    [Serializable]
    public abstract class DefaultEnumerable<T> : DefaultObject, IEnumerable<T>
    {
        public abstract IEnumerator<T> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
