using System.Collections.Generic;

namespace Generic.Containers.Collections.List
{
    public abstract class ListWrapper<T,ListT> : DefaultList<T> where ListT : IList<T>
    {
        protected readonly ListT content;

        protected ListWrapper(ListT content)
        {
            this.content = content;
        }


        public override IEnumerator<T> GetEnumerator()
        {
            return content.GetEnumerator();
        }

        public override void Add(T item)
        {
            content.Add(item);
        }

        public override void Clear()
        {
            content.Clear();
        }

        public override bool Contains(T item)
        {
            return content.Contains(item);
        }

        public override void CopyTo(T[] array, int arrayIndex)
        {
            content.CopyTo(array,arrayIndex);
        }

        public override bool Remove(T item)
        {
            return content.Remove(item);
        }

        public override int Count
        {
            get { return content.Count; }
        }

        public override bool IsReadOnly
        {
            get { return content.IsReadOnly; }
        }

        public override int IndexOf(T item)
        {
            return content.IndexOf(item);
        }

        public override void Insert(int index, T item)
        {
            content.Insert(index,item);
        }

        public override void RemoveAt(int index)
        {
            content.RemoveAt(index);
        }

        public override T this[int index]
        {
            get { return content[index]; }
            set { content[index] = value; }
        }
    }
}
