using System;
using Generic.Containers.Collections.List.Final;

namespace Generic.Containers.Collections.List
{
    class Repeat<T> : DefaultReadOnlyList<T>
    {
        readonly Func<T> creator;
        readonly int count;

        public Repeat(Func<T> creator, int count)
        {
            this.creator = creator;
            this.count = count;
        }

        public override int Count
        {
            get { return count; }
        }

        protected override T Get(int index)
        {
            return creator();
        }
    }
}
