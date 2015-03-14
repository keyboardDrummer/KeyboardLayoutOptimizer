using Generic.Containers.Maybes;
using Generic.Graphs;

namespace Generic.Uncommon.Merge.Maybes
{
    internal class MaybeMerge<TElement, TInnerConflictGraph> where TElement : IMergeable<TElement, TInnerConflictGraph>
    {
        public static ConflictGraph Merge(Maybe<TElement> baseMaybe, Maybe<TElement> head, Maybe<TElement> mine)
        {
            var headDiff = GetDiff(baseMaybe, head);
            var mineDiff = GetDiff(baseMaybe, mine);

            return GetConflictGraph(baseMaybe, mineDiff, headDiff);
        }

        private static IMaybeChange GetDiff(Maybe<TElement> baseMaybe, Maybe<TElement> newMaybe)
        {
            if (baseMaybe.IsJust && newMaybe.IsNothing)
                return new Deleted();

            if (baseMaybe.IsNothing && newMaybe.IsJust)
                return new New<TElement>(newMaybe.FromJust);

            if (baseMaybe.IsJust && newMaybe.IsJust)
                return new Changed<TElement>(newMaybe.FromJust);

            return new NoChange();
        }

        static ConflictGraph GetConflictGraph(Maybe<TElement> baseMaybe, IMaybeChange mineDiff, IMaybeChange headDiff) 
        {
            var result = new ConflictGraph();
            if (headDiff is NoChange && !(mineDiff is NoChange))
                result.AddNode(mineDiff);
            else if (mineDiff is NoChange && !(headDiff is NoChange))
                result.AddNode(headDiff);
            else if (headDiff is Deleted && mineDiff is Deleted)
                result.AddNode(headDiff);
            else if (headDiff is New<TElement> && mineDiff is New<TElement>)
                result.AddNode(headDiff);
            else if (headDiff is Deleted && mineDiff is Changed<TElement> || headDiff is Changed<TElement> && mineDiff is Deleted)
            {
                var headNode = result.AddNode(headDiff);
                var mineNode = result.AddNode(mineDiff);
                result.AddEdge(headNode, mineNode, MaybeUtil.Nothing<TInnerConflictGraph>());
            }
            else
            {
                var headNode = result.AddNode(headDiff);
                var mineNode = result.AddNode(mineDiff);
                var innerConflictGraph = baseMaybe.FromJust.Merge(((Changed<TElement>)headDiff).NewValue, ((Changed<TElement>)mineDiff).NewValue);
                result.AddEdge(headNode, mineNode, MaybeUtil.Just(innerConflictGraph));
            }

            return result;
        }

        internal class ConflictGraph : Graph<IMaybeChange, Maybe<TInnerConflictGraph>>
        {
            
        }
    }
}