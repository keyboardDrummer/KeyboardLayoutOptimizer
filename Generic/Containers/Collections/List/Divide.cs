using System;
using System.Collections.Generic;
using Generic.Containers.Collections.List.Final;

namespace Generic.Containers.Collections.List
{
    public class Divide<T> : DefaultReadOnlyList<IList<T>>
    {
        private readonly IList<T> core;
        private readonly int width;

        public Divide(IList<T> core, int width)
        {
            this.core = core;
            this.width = width;
        }

        public override int Count
        {
            get { return (int) Math.Ceiling((double)core.Count/width); }
        }

        protected override IList<T> Get(int index)
        {
            return core.Drop(index * width).TakeList(width);
        }

    }
}