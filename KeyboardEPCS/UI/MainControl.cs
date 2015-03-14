using System;
using System.Windows.Forms;
using KeyboardEPCS.Logic.Inputs;
using KeyboardEPCS.Properties;

namespace KeyboardEPCS.UI
{
    public partial class MainControl : UserControl
    {
        public enum States
        {
            Stopped,
            Running,
            RunningParallel
        }

        public Settings settings;
        States _state;

        public bool IsRunning { get { return _state == States.Running || _state == States.RunningParallel; } }

        public Settings Settings
        {
            get { return settings; }
            set { settings = value; }
        }

        readonly SettingsControl settingsControl;
        public MainControl()
        {
            InitializeComponent();
            consoleTextBox.Rtf = Resources.mainFormDoc;
            try
            {
                settings = Settings.Load();
            }
            catch (Exception)
            {
                settings = Settings.Standard;
            }
            settingsControl = new SettingsControl(this);
            this.ParentChanged += (sender, args) => { settingsControl.Visible = false; Parent.Controls.Add(settingsControl); };
            neutralControlBar1.Init(this);
            runningControlBar1.Init(this);
            parallelRunControlBar1.Init(this);
            State = States.Stopped;
        }

        public States State
        {
            get { return _state; }
            set
            {
                _state = value;
                SetButtonAvailability();
            }
        }

        void SettingsButtonClick(object sender, EventArgs e)
        {
            Visible = false;
            settingsControl.Show();
        }

        void SetButtonAvailability()
        {
            neutralControlBar1.Visible = _state == States.Stopped;
            runningControlBar1.Visible = _state == States.Running;
            parallelRunControlBar1.Visible = _state == States.RunningParallel;
        }

        public Action<string> Logger
        {
            get { return s => Invoke(new Action(() => consoleTextBox.AppendText(Environment.NewLine + s + Environment.NewLine))); }
        }

        void ClearButtonClick(object sender, EventArgs e)
        {
            consoleTextBox.Clear();
        }

        public bool UntilBetter(KeyboardEPAlgorithm algorithm)
        {
            var startingSolution = algorithm.Current;
            while (IsRunning && algorithm.Current.Equals(startingSolution))
            {
                if (!algorithm.MoveNext())
                    return false;
            }
            return true;
        }

        public static string PrintLayoutWithScore(KeyboardLayout layout, double score)
        {
            return (layout.Print().ToString() + Environment.NewLine
                + "Score is: " + -score + Environment.NewLine);
        }

        public void StartParallelRun()
        {
            parallelRunControlBar1.Start();
        }

        public void Start(KeyboardEPAlgorithm algorithm)
        {
            runningControlBar1.Start(algorithm);
        }
    }
}
