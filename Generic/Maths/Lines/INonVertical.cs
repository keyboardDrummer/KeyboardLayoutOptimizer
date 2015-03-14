namespace Generic.Maths.Lines
{
    public interface INonVertical : ILine
    {
        double Y0 { get; }
        double Y(double x);
        double DyDx { get; }
    }
}
