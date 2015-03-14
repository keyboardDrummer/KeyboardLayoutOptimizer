using System;
using System.Threading;
using System.Windows.Forms;
using KeyboardEPCS.Logic.Inputs;

namespace KeyboardEPCS.UI
{
    public partial class RunningControlBar : UserControl
    {
        const int TIME_OUT = 500;

        KeyboardEPAlgorithm _algorithm;
        Thread workingThread;
        MainControl mainControl;

        public RunningControlBar()
        {
            InitializeComponent();
        }

        public void Init(MainControl mainControl)
        {
            this.mainControl = mainControl;
            stopButton.Click += StopButtonClick;
            saveCurrentToSettings.Click += SavePermButtonClick;
        }

        KeyboardEPAlgorithm Algorithm
        {
            get { return _algorithm; }
            set
            {
                _algorithm = value;
                _algorithm.FoundBetter += InvokeBetterSampleFound;
            }
        }
        
        MainControl.States State
        {
            get { return mainControl.State; }
            set { mainControl.State = value; }
        }

        RichTextBox ConsoleTextBox
        {
            get { return mainControl.consoleTextBox; }
        }

        Settings Settings
        {
            get { return mainControl.settings; }
        }

        public void Start(KeyboardEPAlgorithm algorithm)
        {
            Algorithm = algorithm;
            ThreadStart work = () =>
            {
                while (State == MainControl.States.Running)
                {
                    if (mainControl.UntilBetter(Algorithm))
                        continue;

                    Invoke(new Action(() => { mainControl.State = MainControl.States.Stopped; }));
                    break;
                }
            };
            State = MainControl.States.Running;
            workingThread = new Thread(work);
            workingThread.Start();
        }

        void InvokeBetterSampleFound(double oldScores)
        {
            Invoke(new Action(() => BetterSampleFound(Algorithm.Current, Algorithm.CurrentScore)));
        }

        void BetterSampleFound(KeyboardLayout layout, double score)
        {
            ConsoleTextBox.AppendText(Environment.NewLine +
                "Better sample found! Layout is:"
                + Environment.NewLine + MainControl.PrintLayoutWithScore(layout, score));
        }


        void Stop()
        {
            State = MainControl.States.Stopped;
            workingThread.Join(TIME_OUT);
        }

        void StopButtonClick(object sender, EventArgs e)
        {
            Stop();
            ConsoleTextBox.AppendText("Stopping current algorithm." + Environment.NewLine + Environment.NewLine);
        }

        void SavePermButtonClick(object sender, EventArgs e)
        {
            Settings.AddNamedLayout("From Algorithm.." + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLocalTime(), Algorithm.Current);
        }
    }
}
