using System.Data;

namespace Generic.Containers.Collections.List.Final
{
    public abstract class DefaultReadOnlyList<T> : DefaultList<T>
    {
        public abstract override int Count { get; }

        public override T this[int index]
        {
            get { return Get(index); }
            set { throw new ReadOnlyException(); }
        }

        public override void Clear()
        {
            throw new ReadOnlyException();
        }

        public override void Insert(int index, T item)
        {
            throw new ReadOnlyException();
        }

        public override void RemoveAt(int index)
        {
            throw new ReadOnlyException();
        }

        protected abstract T Get(int index);
    }
}