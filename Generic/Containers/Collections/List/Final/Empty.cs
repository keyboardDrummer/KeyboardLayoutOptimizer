using System;

namespace Generic.Containers.Collections.List.Final
{
    public class Empty<T> : DefaultReadOnlyList<T>
    {
        public override int Count
        {
            get { return 0; }
        }

        protected override T Get(int index)
        {
            throw new IndexOutOfRangeException();
        }
    }
}
