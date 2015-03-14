using System;
using Generic.InputOutput.Printing;
using Generic.InputOutput.Printing.Sizable;

namespace KeyboardEPCS.Logic.Transitions
{

    [Serializable]
    public class TransitionInfo : IPrintable
    {
        Int32 count;
        Double mean;
        Double standardDeviation;

        public TransitionInfo()
        {
            mean = 0;
            standardDeviation = 0;
            count = 0;
        }

        Double Term
        {
            get { return standardDeviation * standardDeviation * Count + Mean * Mean; }
        }

        public Int32 Count
        {
            get { return count; }
            set { count = value; }
        }

        public Double Mean
        {
            get { return mean; }
            set { mean = value; }
        }

        public void AddMeasurement(Double miliseconds)
        {
            var newCount = Count + 1;
            var newMean = (Mean * Count + miliseconds) / newCount;
            var diff = Math.Abs(newMean - miliseconds);
            //s = root(1/n * sum((x_i - u)^2) = root ( 1/n *  ( sum((x_i-u)^2) + (x_last-u)^2)
            //(x_i+u-u-u')^2 = x_i^2 - 2x_i*u' + u'^2 = (x_i-u)^2 - 2*x_i*u' + 2*x_i*u - u^2 + u'^2 
            // = (x_i-u)^2 + 2*x_i*(u-u') - u^2 + u'^2 

            //root ( sum((x_i-u)^2)/n + ((x_last-u')^2)/n')
            // s' = root ( (s^2*n + (x_last-u)^2)/n)
            //s' = root((s*s*c + m*m + c' * m' * m') / c)
            var newStandardDeviation = Math.Sqrt((Math.Pow(standardDeviation, 2) * Count + Math.Pow(miliseconds - newMean, 2)) / newCount);
            standardDeviation = newStandardDeviation;
            if (diff > 2 * newStandardDeviation)
                return;

            mean = newMean;
            count = newCount;
        }

        public Document Print()
        {
            return Document.Seperated(", ","Mean:" - mean.Print(), "Count:" - count.Print(), "Deviation:" - standardDeviation.Print());
        }
    }
}
