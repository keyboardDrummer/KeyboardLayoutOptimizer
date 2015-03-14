using Generic.Containers.Collections.Set;
using KeyboardEPCS.Logic;
using KeyboardEPCS.Logic.Inputs;
using KeyboardEPCS.Logic.Inputs.Transitions;
using KeyboardEPCS.Logic.Transitions;
using KeyboardEPCS.Properties;
using NUnit.Framework;

namespace KeyboardEPCS
{
    [TestFixture]
    class TestClass
    {
        [Test]
        public void KlauslerStyleDvorakVersusAbc()
        {
            var corpus = Corpus.GetEmpty(Keyboard.StandardKeyboard).AddText(Resources.huckleberryfin);
            var times = TransitionTimeBuilder.KlauslerSimple.Times;
            var goodScore = KeyboardEPAlgorithm.Score(KeyboardLayout.Dvorak, corpus, times);
            var badScore = KeyboardEPAlgorithm.Score(KeyboardLayout.Abc, corpus, times);
            Assert.That(goodScore > badScore);
        }

        [Test]
        public void TestAbKeyboard()
        {
            var keyboard = new Keyboard(new[]{2},SetUtil.NewHash('a','b'));

            var firstPos = new KeyboardPosition(0,0);
            var secondPos = new KeyboardPosition(0,1);
            var times = new TransitionTimes(keyboard);
            times[firstPos, secondPos] = 1;
            times[firstPos, firstPos] = 2;
            times[secondPos, secondPos] = 2;
            times[secondPos, firstPos] = 2;
            var goodLayout = new KeyboardLayout("ab", keyboard);
            var badLayout = new KeyboardLayout("ba",keyboard);
            var occurrences = new double[keyboard.AllChars.Count, keyboard.AllChars.Count];
            var corpus = new Corpus(occurrences, 1000,keyboard);
            corpus['a', 'b'] = 100;
            var goodScore = KeyboardEPAlgorithm.Score(goodLayout, corpus, times);
            var badScore = KeyboardEPAlgorithm.Score(badLayout, corpus, times);
            Assert.That(goodScore > badScore);
        }
    }
}
