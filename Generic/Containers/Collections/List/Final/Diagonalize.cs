using System;
using System.Collections.Generic;
using System.Linq;
using Generic.Containers.Collections.Enumerables;
using Generic.Containers.Collections.Enumerators;

namespace Generic.Containers.Collections.List.Final
{
    public class Diagonalize<TA, TB> : DefaultReadOnlyList<IEnumerable<TB>>
    {
        private readonly Func<IList<TA>, TB> aggregator;
        private readonly IList<IList<TA>> lists;

        public Diagonalize(IList<IList<TA>> lists, Func<IList<TA>, TB> aggregator)
        {
            this.lists = lists;
            this.aggregator = aggregator;
        }

        public override int Count
        {
            get { return lists.Aggregate(1,(i,x) => i * x.Count); }
        }

        protected override IEnumerable<TB> Get(int depth)
        {
            return EnumerableUtil.FromEnumeratorFunc(() => new DiagonalizeHelper(depth, lists, aggregator));
        }

        private class DiagonalizeHelper : DefaultEnumerator<TB>
        {
            private readonly Func<IList<TA>, TB> aggregator;
            private TB current;
            private readonly int[] listDepths;
            private readonly IList<IList<TA>> lists;
            private readonly int depth;

            public DiagonalizeHelper(int depth, IList<IList<TA>> lists, Func<IList<TA>, TB> aggregator)
            {
                this.lists = lists;
                this.aggregator = aggregator;
                listDepths = new int[lists.Count];
                this.depth = depth;
            }

            public override TB Current
            {
                get { return current; }
            }

            public override bool MoveNext()
            {
                bool success = MoveNextIndex();
                if (!success)
                    return false;
                current = aggregator(lists.Indexes().SelectList(i => lists[i][listDepths[i]]));
                return true;
            }

            private Boolean MoveNextIndex()
            {
                listDepths[0]++;
                int listIndex = 0;
                while (listDepths[listIndex] > depth)
                {
                    listDepths[listIndex] = 0;
                    listIndex++;

                    if (listIndex == lists.Count)
                        return false;

                    listDepths[listIndex]++;
                }
                return true;
            }

        }
    }
}