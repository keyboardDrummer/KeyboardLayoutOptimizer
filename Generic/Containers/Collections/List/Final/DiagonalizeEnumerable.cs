using System;
using System.Collections.Generic;
using System.Linq;
using Generic.Containers.Collections.Enumerables;
using Generic.Containers.Collections.Enumerators;

namespace Generic.Containers.Collections.List.Final
{
    public class DiagonalizeEnumerable<TA, TB> : DefaultEnumerable<IEnumerable<TB>>
    {
        private readonly Func<IList<TA>, TB> aggregator;
        private readonly IList<IList<TA>> lists;

        public DiagonalizeEnumerable(IList<IList<TA>> lists, Func<IList<TA>, TB> aggregator)
        {
            this.lists = lists;
            this.aggregator = aggregator;
        }

        private class DiagonalizeHelper : DefaultEnumerator<TB>
        {
            private readonly Func<IList<TA>, TB> aggregator;
            private TB current;
            private readonly int[] listDepths;
            private readonly IList<IList<TA>> lists;
            private readonly int maxDepth;
            private readonly int currentDepth;

            public DiagonalizeHelper(int maxDepth, IList<IList<TA>> lists, Func<IList<TA>, TB> aggregator)
            {
                this.lists = lists;
                this.aggregator = aggregator;
                listDepths = new int[lists.Count];
                this.maxDepth = maxDepth;
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
                while (listDepths[listIndex] > maxDepth)
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

        public override IEnumerator<IEnumerable<TB>> GetEnumerator()
        {
            return EnumerableUtil.From(0).Select(depth => EnumerableUtil.FromEnumeratorFunc(
                () => new DiagonalizeHelper(depth, lists, aggregator))).GetEnumerator();
        }
    }
}