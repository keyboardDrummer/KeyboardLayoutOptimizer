using System;
using Generic.Uncommon.Merge.Sets;

namespace Generic.Merge.Sets
{
    internal class Add<TElement> : ISetChange
    {
        readonly TElement element;

        public Add(TElement element)
        {
            this.element = element;
        }

        public TElement Element
        {
            get { return element; }
        }
    }
}