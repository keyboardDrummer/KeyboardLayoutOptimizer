using System;
using System.Collections.Generic;

namespace Generic.Containers.Collections.List.Final
{
    public class DivideInfinite<T> : DefaultReadOnlyList<IList<T>>
    {
        private readonly ListFromEnumerable<T> core;
        private readonly int width;

        public DivideInfinite(IEnumerable<T> core, int width)
        {
            if (width == 0)
                throw new ArgumentException("width may not be zero");

            this.core = core.ToLazyList();
            this.width = width;
        }

        public override int Count
        {
            get { throw new InvalidOperationException("Cannot count an infinite list"); }
        }

        public bool HasMinimumCount(int minimum)
        {
            /* 0 = true
             * 1 = core.Count > 0
             * 2 = core.Count > width
             * 3 = core.Count > 2*width
             */ 
            return core.HasMinimumCount(1+(minimum-1)*width);
        }

        protected override IList<T> Get(int index)
        {
            return core.Drop(index * width).TakeList(width);
        }

        public override IEnumerator<IList<T>> GetEnumerator()
        {
            int index = 0;
            while(HasMinimumCount(index+1))
            {
                yield return Get(index);
                index++;
            }
        }
    }
}