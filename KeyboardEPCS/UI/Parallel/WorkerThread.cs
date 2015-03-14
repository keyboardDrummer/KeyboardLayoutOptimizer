using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Generic;
using Generic.Containers.Collections.List;
using Generic.Containers.Collections.Set;
using Generic.InputOutput.Printing;
using KeyboardEPCS.Logic.Inputs;
using KeyboardEPCS.Logic.Transitions;

namespace KeyboardEPCS.UI
{
    class WorkerThread
    {
        const int TIME_OUT = 5000;
        static readonly bool useFullInfo = DebugUtil.True;
        readonly QueueWorker innerWorker = new QueueWorker();
        readonly IDictionary<KeyboardEPAlgorithm, BetterFoundInfo> infos = new Dictionary<KeyboardEPAlgorithm, BetterFoundInfo>();
        readonly IList<IColumnSpec> columnSpecs = ListUtil.New<IColumnSpec>(new TimerSpec(), new MaximumScoreDifferenceColumn());
        readonly IList<KeyboardEPAlgorithm> algorithms = new List<KeyboardEPAlgorithm>();
        readonly IList<Thread> workingThreads = new List<Thread>();
        readonly Safe safe = new Safe();
        readonly MainControl mainControl;

        public WorkerThread(int parallelRunCount, MainControl mainControl)
        {
            this.mainControl = mainControl;

            innerWorker.AddWorkItem(() =>
            {
                safe.AddLock();
                safe.SafeOpened += () => mainControl.Invoke(new Action(() => mainControl.State = MainControl.States.Stopped));
                foreach (var _ in Enumerable.Range(0, parallelRunCount))
                    StartAlgorithm();
                safe.RemoveLock();
            });
        }

        void StartAlgorithm()
        {
            safe.AddLock();
            var workingThread = new Thread(() =>
            {
                var algorithm = GetAlgorithm();

                algorithm.FoundBetter += oldScore => InvokeBetterSampleFound(algorithm);
                algorithms.Add(algorithm);
                InvokeBetterSampleFound(algorithm);
                while (mainControl.IsRunning)
                {
                    if (mainControl.UntilBetter(algorithm))
                        continue;

                    break;
                }
                safe.RemoveLock();
            });
            workingThread.Priority = ThreadPriority.BelowNormal;
            workingThreads.Add(workingThread);
            workingThread.Start();
        }

        Settings Settings
        {
            get { return mainControl.settings; }
        }

        KeyboardEPAlgorithm GetAlgorithm()
        {
            return DebugUtil.False ? GetTestAlgorithm() : GetStandardWithCurrentSettings();
        }

        KeyboardEPAlgorithm GetTestAlgorithm()
        {
            const int width = 3;
            const int height = 3;
            const int keyboardKeyCount = width * height;
            var keyboard = new Keyboard(Enumerable.Range(0, height).Select(_ => width).ToArray(),
                Keyboard.StandardKeyboard.AllChars.Take(keyboardKeyCount).ToHashSet());
            var timeBuilder = TransitionTimeBuilder.GetEmpty(keyboard);
            var fractions = new double[keyboardKeyCount, keyboardKeyCount];
            for (var i = 0; i < keyboardKeyCount; i++)
            {
                for (var j = i; j < keyboardKeyCount; j++)
                {
                    var miliseconds = i + j * keyboardKeyCount;
                    timeBuilder[i, j].AddMeasurement(miliseconds);
                    fractions[i, j] = miliseconds;
                }
            }

            var corpus = new Corpus(fractions, keyboardKeyCount, keyboard);
            var algorithm = new KeyboardEPAlgorithm(corpus, timeBuilder.Times, GetLogger());
            algorithm.SetLayout(KeyboardLayout.Random(keyboard));
            return algorithm;
        }

        Action<string> GetLogger()
        {
            if (DebugUtil.False)
            {
                var index = algorithms.Count;
                Action<string> logger = s => mainControl.Logger(string.Format("Algorithm {0}: {1}", index, s));
                return logger;
            }
            return s => { };
        }

        KeyboardEPAlgorithm GetStandardWithCurrentSettings()
        {
            var corpus = Settings.CurrentCorpus.FromJust;
            var style = Settings.CurrentTimes.FromJust.Times;
            var algorithm = new KeyboardEPAlgorithm(corpus, style, GetLogger());
            algorithm.SetLayout(KeyboardLayout.Random(Keyboard.StandardKeyboard));
            return algorithm;
        }

        void InvokeBetterSampleFound(KeyboardEPAlgorithm algorithm)
        {
           innerWorker.AddWorkItem(() => BetterSampleFound(new BetterFoundInfo(DateTime.Now, algorithm, algorithm.Current, algorithm.CurrentScore)));
        }

        void BetterSampleFound(BetterFoundInfo info)
        {
            IDictionary<IColumnSpec, string> messages = new Dictionary<IColumnSpec, string>();
            foreach (var columnSpec in columnSpecs)
                messages[columnSpec] = columnSpec.GetColumnString(infos, info);
            var algorithmIndex = algorithms.IndexOf(info.Algorithm);

            mainControl.Invoke(new Action(() =>
            {
                if (useFullInfo)
                {
                    ConsoleTextBox.AppendText(Environment.NewLine + String.Format("Better sample found for {0}! Layout is:", algorithmIndex)
                        + Environment.NewLine + MainControl.PrintLayoutWithScore(info.Current, info.CurrentScore));
                    ConsoleTextBox.AppendText(messages.Select(kv => kv.Key.Caption - DocumentUtil.Colon - kv.Value).Seperated("; ").ToString() + Environment.NewLine);
                }
                else
                    ConsoleTextBox.AppendText(messages.Seperated("; ").ToString() + Environment.NewLine);
            }));
        }

        RichTextBox ConsoleTextBox
        {
            get { return mainControl.consoleTextBox; }
        }

        public void Stop()
        {
            innerWorker.StopWorking();
            foreach (var workingThread in workingThreads)
                workingThread.Join(TIME_OUT);
        }
    }
}