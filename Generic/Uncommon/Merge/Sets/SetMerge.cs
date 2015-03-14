using System;
using System.Collections.Generic;
using System.Linq;
using Generic.Graphs;
using Generic.Containers.Collections.Set;

namespace Generic.Uncommon.Merge.Sets
{
    internal class SetMerge<TElement>
        where TElement : IEquatable<TElement>
    {
        public static ConflictGraph Merge(ISet<TElement> baseMaybe, ISet<TElement> head, ISet<TElement> mine)
        {
            var headDiff = GetDiff(baseMaybe, head);
            var mineDiff = GetDiff(baseMaybe, mine);

            return GetConflictGraph(mineDiff, headDiff);
        }

        static Tuple<ISet<Add>, ISet<Remove>> GetDiff(ISet<TElement> baseSet, ISet<TElement> newSet)
        {
            var added = newSet.Difference(baseSet).SelectDisjunct(t => new Add(t), a => a.Element);
            var removed = baseSet.Difference(newSet).SelectDisjunct(t => new Remove(t), r => r.Element);
            return Tuple.Create(added, removed);
        }

        static ConflictGraph GetConflictGraph(Tuple<ISet<Add>, ISet<Remove>> mineDiff, Tuple<ISet<Add>, ISet<Remove>> headDiff)
        {
            var result = new ConflictGraph();
            var adds = mineDiff.Item1.Union(headDiff.Item1);
            var removes = mineDiff.Item2.Union(headDiff.Item2);
            foreach(var x in adds.Concat<ISetChange>(removes))
            {
                ((Action<ISetChange>)(change => result.AddNode(change)))(x);
            }
            return result;
        }

        #region Nested type: Add

        class Add : ISetChange
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

        #endregion

        #region Nested type: ConflictGraph

        internal class ConflictGraph : Graph<ISetChange>
        {
        }

        #endregion

        #region Nested type: Remove

        class Remove : ISetChange
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

        #endregion
    }
}
