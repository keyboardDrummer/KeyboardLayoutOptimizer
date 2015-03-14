namespace Generic.Containers.Pairs
{
    public interface IPair<out A, out B>
    {
        A Item1 { get; }
        B Item2 { get; }
    }
}
