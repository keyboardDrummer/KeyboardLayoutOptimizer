using System;
using System.Collections.Generic;
using Generic;
using Generic.Uncommon;
using KeyboardEPCS.Logic;
using KeyboardEPCS.Logic.Inputs;
using KeyboardEPCS.Logic.Inputs.Transitions;

namespace KeyboardEPCS
{
    public class KeyboardEPAlgorithm : HillClimberAlgorithm<KeyboardLayout>
    {
        readonly Random random = new Random(0);
        readonly Corpus corpus;
        readonly TransitionTimes times;
        readonly Mutate mutator;

        public KeyboardEPAlgorithm(Corpus corpus, TransitionTimes times, Action<string> logger)
        {
            this.corpus = corpus;
            this.times = times;
            mutator = new Mutate(logger);
        }

        public static double Score(KeyboardLayout keyboardLayout, Corpus corpus, TransitionTimes times)
        {
            var totalTime = 0.0;
            foreach(var fromTo in keyboardLayout.Keyboard.CharPairs)
            {
                var occurrence = corpus[fromTo.Item1, fromTo.Item2];
                var fromPosition = keyboardLayout.GetCharPosition(fromTo.Item1);
                var toPosition = keyboardLayout.GetCharPosition(fromTo.Item2);
                var time = times[fromPosition, toPosition];
                totalTime += time * occurrence;
            }
            return -totalTime;
        }

        protected override IEnumerable<KeyboardLayout> GetOutgoingOfCurrent()
        {
            return mutator.GetMutations(random, Current);
        }

        protected override double Score(KeyboardLayout solution)
        {
            return Score(solution, corpus, times);
        }

        public void SetLayout(KeyboardLayout layout)
        {
            SetCurrent(layout);
        }
    }
}