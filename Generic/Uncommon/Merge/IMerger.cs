namespace Generic.Uncommon.Merge
{
    interface IMerger<T,Diff>
    {
        T Apply(T basy, IConflictGraph graph);
        IConflictGraph Merge(T baseMaybe, T head, T mine);

        Diff GetDiff(T baseSet, T newSet);

        IConflictGraph GetConflictGraph(Diff mineDiff, Diff headDiff);
    }
}
