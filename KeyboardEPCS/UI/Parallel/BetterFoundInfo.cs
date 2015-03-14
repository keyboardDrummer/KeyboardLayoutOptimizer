using System;
using KeyboardEPCS.Logic.Inputs;

namespace KeyboardEPCS.UI
{
    class BetterFoundInfo
    {
        readonly DateTime currentTime;
        readonly KeyboardEPAlgorithm algorithm;
        readonly KeyboardLayout current;
        readonly double currentScore;

        public BetterFoundInfo(DateTime currentTime, KeyboardEPAlgorithm algorithm, KeyboardLayout current, double currentScore)
        {
            this.currentTime = currentTime;
            this.algorithm = algorithm;
            this.current = current;
            this.currentScore = currentScore;
        }

        public KeyboardEPAlgorithm Algorithm
        {
            get { return algorithm; }
        }

        public DateTime CurrentTime
        {
            get { return currentTime; }
        }

        public KeyboardLayout Current
        {
            get { return current; }
        }

        public double CurrentScore
        {
            get { return currentScore; }
        }
    }
}