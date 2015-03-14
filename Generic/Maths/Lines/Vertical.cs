namespace Generic.Maths.Lines
{
    public class Vertical : IVertical
    {
        internal Vertical(double x)
        {
            X = x;
        }

        public double X { get; private set; }

        double ILine.X(double y)
        {
            return X;
        }

        public double DyDx
        {
            get { return double.PositiveInfinity; }
        }
    }
}
