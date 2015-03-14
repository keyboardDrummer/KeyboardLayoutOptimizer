using Generic.Common;

namespace Generic.Maths.Lines
{
    public class Line : DefaultObject, INonVertical
    {
        public double DyDx { get; private set; }
        public double Y0 { get; private set; }

        public double Y(double x) { return DyDx * x + Y0; }
        public double X(double y) { return (y - Y0) / DyDx; }

        internal Line(double dyDx, double y0)
        {
            DyDx = dyDx;
            Y0 = y0;
        }
    }
}
