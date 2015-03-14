using Generic.Maths.Vectors;

namespace Generic.Maths.Lines
{
    public interface IBoundedLine : ILine
    {
        DoubleVector2 Start { get; }
        DoubleVector2 End { get; }
        double LeftX { get; }
        double RightX { get; }
        double TopY { get; }
        double BottomY { get; }
        Angle Angle { get; }
        double Length { get; }
    }
}