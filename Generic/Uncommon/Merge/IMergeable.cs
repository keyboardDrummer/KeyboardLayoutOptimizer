namespace Generic.Uncommon.Merge
{
    interface IMergeable<in T, out TConflictGraph>
    {
        TConflictGraph Merge(T mine, T head);
    }
}
