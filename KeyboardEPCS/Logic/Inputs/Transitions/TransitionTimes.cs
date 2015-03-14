using System;

namespace KeyboardEPCS.Logic.Inputs.Transitions
{
    [Serializable]
    public class TransitionTimes
    {
        readonly double[,] transitionTimes;
        readonly Keyboard keyboard;

        public TransitionTimes(Keyboard keyboard)
        {
            this.keyboard = keyboard;
            transitionTimes = new double[keyboard.AllChars.Count,keyboard.AllChars.Count];
        }

        public TransitionTimes(double[,] data, Keyboard keyboard)
        {
            this.transitionTimes = data;
            this.keyboard = keyboard;
        }

        public Double this[KeyboardPosition fromPos, KeyboardPosition toPos]
        {
            get { return transitionTimes[keyboard.PositionToPositionIndex(fromPos), 
                keyboard.PositionToPositionIndex(toPos)]; }
            set
            {
                transitionTimes[keyboard.PositionToPositionIndex(fromPos),
                    keyboard.PositionToPositionIndex(toPos)] = value;
            }
        }
    }
}
