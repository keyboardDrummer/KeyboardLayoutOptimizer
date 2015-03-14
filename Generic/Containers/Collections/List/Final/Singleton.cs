using System;

namespace Generic.Containers.Collections.List.Final
{
    public class Singleton<T> : DefaultReadOnlyList<T>
    {
        private readonly T unknown;

        public Singleton(T unknown)
        {
            this.unknown = unknown;
        }

        public override int Count
        {
            get { return 1; }
        }

        protected override T Get(int index)
        {
            if (index == 0)
            {
                return unknown;
            }
            throw new IndexOutOfRangeException();
        }
    }
}