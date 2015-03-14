using System;

namespace Generic.Containers.Collections.List.Final
{
    [Serializable]
    public abstract class FixedSizeList<T> : DefaultList<T>
    {
        public override void Clear()
        {
            throw new NotSupportedException();
        }

        public override void Insert(int index, T item)
        {
            throw new NotSupportedException();
        }

        public override void RemoveAt(int index)
        {
            throw new NotSupportedException();
        }
    }
}
