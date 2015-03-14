//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Generic.Containers.Collections.Enumerables;
//using Generic.Uncommon.Merge;
//using Generic.Uncommon.Merge.Sets;

//namespace Generic.Merge.Sets
//{
//    class SetMergeMergeable<TElement,TConflictGraph>
//        where TElement : IMergeable<TElement,TConflictGraph>
//    {
//        public static Diff GetDiff(ISet<TElement> baseSet, ISet<TElement> newSet)
//        {
//            var added = newSet.Difference(baseSet).SelectDisjunct(t => new Add<TElement>(t), a => a.Element);
//            var removed = baseSet.Difference(newSet).SelectDisjunct(t => new Remove<TElement>(t), r => r.Element);
//            return new Diff<TElement>(added, removed);
//        }

//        public static void ApplyDiff(ISet<TElement> set, Diff<TElement> diff)
//        {
//            foreach (var remove in diff.Item2)
//            {
//                set.Remove(remove.Element);
//            }
//            foreach (var add in diff.Item1)
//            {
//                set.Add(add.Element);
//            }
//        }

//        static ConflictGraph MergeDiffs(Diff<TElement> mineDiff, Diff<TElement> headDiff)
//        {
//            var result = new ConflictGraph();
//            var adds = mineDiff.Item1.Union(headDiff.Item1);
//            var removes = mineDiff.Item2.Union(headDiff.Item2);
//            adds.Concat<ISetChange>(removes).ForEach(change => result.AddNode(change));
//            return result;
//        }

//        public static bool CheckConsistency(Diff diff)
//        {
//            return diff.Item1.Any(add => diff.Item2.Any(remove => add.Element.Equals(remove.Element)));
//        }

//        internal class Diff : Tuple<ISet<Add<TElement>>, ISet<Change<TElement>>, ISet<Remove<TElement>>>
//        {
//            public Diff(T1 item1, T2 item2, T3 item3) : base(item1, item2, item3)
//            {
//            }
//        }
//    }
//}
