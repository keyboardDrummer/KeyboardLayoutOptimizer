using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generic.Merge.Sets
{
    class Change<TElement>


    {
        readonly TElement original;
        readonly TElement neww;

        public Change(TElement original, TElement neww)
        {
            this.original = original;
            this.neww = neww;
        }

        public TElement Original
        {
            get { return original; }
        }

        public TElement Neww
        {
            get { return neww; }
        }
    }
}
