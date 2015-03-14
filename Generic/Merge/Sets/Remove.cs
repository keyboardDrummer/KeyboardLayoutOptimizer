using System;
using Generic.Uncommon.Merge.Sets;

namespace Generic.Merge.Sets
{
    internal class Remove<TElement> : ISetChange
    {
        readonly TElement element;

        public Remove(TElement element)
        {
            this.element = element;
        }

        public TElement Element
        {
            get { return element; }
        }
    }
}