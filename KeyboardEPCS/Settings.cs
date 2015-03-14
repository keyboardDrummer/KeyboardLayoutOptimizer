using System;
using System.Collections.Generic;
using System.Linq;
using Generic;
using Generic.Containers.Maybes;
using Generic.InputOutput;
using KeyboardEPCS.Logic.Inputs;
using KeyboardEPCS.Logic.Transitions;
using KeyboardEPCS.Properties;

namespace KeyboardEPCS
{
    [Serializable]
    public class Settings
    {
        const String SETTINGS_FILE_NAME = "settings.kbep";
        readonly IList<Tuple<KeyboardLayout, string>> keyboardLayouts = new List<Tuple<KeyboardLayout, string>>();
        readonly IList<Tuple<TransitionTimeBuilder,string>> transitionTimess = new List<Tuple<TransitionTimeBuilder, string>>();
        readonly IList<Tuple<Corpus, string>> corpi = new List<Tuple<Corpus, string>>();

        Maybe<KeyboardLayout> currentLayout = new Nothing<KeyboardLayout>();
        Maybe<TransitionTimeBuilder> currentTimes = new Nothing<TransitionTimeBuilder>();
        Maybe<Corpus> currentCorpus = new Nothing<Corpus>();

        public IList<Tuple<KeyboardLayout, string>> KeyboardLayouts
        {
            get { return keyboardLayouts; }
        }

        public IList<Tuple<TransitionTimeBuilder, string>> TransitionTimess
        {
            get { return transitionTimess; }
        }

        public IList<Tuple<Corpus, string>> Corpi
        {
            get { return corpi; }
        }

        public Maybe<KeyboardLayout> CurrentLayout
        {
            get { return currentLayout; }
            set { currentLayout = value; }
        }

        public Maybe<Corpus> CurrentCorpus
        {
            get { return currentCorpus; }
            set { currentCorpus = value; }
        }

        public Maybe<TransitionTimeBuilder> CurrentTimes
        {
            get { return currentTimes; }
            set { currentTimes = value; }
        }

        public static Settings Standard
        {
            get
            {
                var result = new Settings();
                var exaggerated = TransitionTimeBuilder.Exaggerated;
                result.AddNamedTransitions("Exaggerated", exaggerated);
                var klauslerTimes = TransitionTimeBuilder.KlauslerSimple;
                result.AddNamedTransitions("Klausler", klauslerTimes);
                result.AddNamedTransitions("Quasi Random", TransitionTimeBuilder.QuasiRandom);
                result.AddNamedLayout("Dvorak",KeyboardLayout.Dvorak);
                result.AddNamedLayout("Abc",KeyboardLayout.Abc);
                var qwerty = KeyboardLayout.QWERTY;
                result.keyboardLayouts.Add(Tuple.Create(qwerty, "Qwerty"));
                result.AddNamedCorpus("huckleberry fin", Corpus.GetEmpty(Keyboard.StandardKeyboard).AddText(Resources.huckleberryfin));

                result.currentTimes = MaybeUtil.Just(klauslerTimes);
                result.currentLayout = MaybeUtil.Just(qwerty);
                result.currentCorpus = result.Corpi.MaybeFirst().Select(t => t.Item1);
                return result;
            }
        }

        public void AddNamedLayout(string name, KeyboardLayout layout)
        {
            keyboardLayouts.Add(Tuple.Create(layout, name));
        }

        public Boolean CanStartWithKnownLayout
        {
            get { return CanStart && currentLayout.IsJust; }
        }

        public Boolean CanStart
        {
            get
            {
                return currentCorpus.IsJust && currentTimes.IsJust;
            }
        }
        
        public void Save()
        {
            Serialize.Save(SETTINGS_FILE_NAME, this);
        }

        public static Settings Load()
        {
            return Serialize.Load<Settings>(SETTINGS_FILE_NAME);
        }

        public Double GetCurrentLayoutTime()
        {
            return GetTime(currentLayout.FromJust);
        }

        Double GetTime(KeyboardLayout keyboardLayout)
        {
            return -KeyboardEPAlgorithm.Score(keyboardLayout, currentCorpus.FromJust, currentTimes.FromJust.Times);
        }

        void AddNamedCorpus(string name, Corpus corpus)
        {
            corpi.Add(Tuple.Create(corpus,name));
        }

        public void AddNamedTransitions(string name, TransitionTimeBuilder times)
        {
            transitionTimess.Add(Tuple.Create(times,name));
        }
    }
}
