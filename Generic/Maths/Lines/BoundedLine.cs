using System;
using Generic.Maths.Vectors;

namespace Generic.Maths.Lines
{
    //public class BoundedLine : Line, IBoundedLine
    //{
    //    private readonly double _xLeft;
    //    private readonly double _xRight;
    //    bool leftToRight;

    //    public BoundedLine(float firstX, float secondX, float yPerX, float y0)
    //        : base(yPerX, y0)
    //    {
    //        leftToRight = firstX <= secondX;

    //        _xLeft = Math.Min(firstX,secondX);
    //        _xRight = Math.Max(firstX,secondX);
    //    }

    //    public double LeftX
    //    {
    //        get { return _xLeft; }
    //    }

    //    public double RightX
    //    {
    //        get { return _xRight; }
    //    }

    //    public double TopY
    //    {
    //        get { return Math.Max(Y(_xLeft), Y(_xRight)); }
    //    }

    //    public double BottomY
    //    {
    //        get { return Math.Min(Y(_xLeft), Y(_xRight)); }
    //    }

    //    public DoubleVector2 LeftPoint()
    //    {
    //        return new DoubleVector2(LeftX, Y(LeftX));
    //    }
        
    //    public DoubleVector2 RightPoint()
    //    {
    //        return new DoubleVector2(RightX, Y(RightX));
    //    }

    //    public override string ToString()
    //    {
    //        return "(" + LeftPoint() + ", " + RightPoint() + ")";
    //    }
    //}
}
