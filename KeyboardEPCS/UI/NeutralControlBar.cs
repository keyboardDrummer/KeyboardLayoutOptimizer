using System;
using System.Windows.Forms;
using KeyboardEPCS.Logic.Inputs;
using KeyboardEPCS.Properties;

namespace KeyboardEPCS.UI
{
    public partial class NeutralControlBar : UserControl
    {
        MainControl mainControl;
        static readonly String startingNew = "Starting new evolutionairy algorithm." + Environment.NewLine + "Starting layout is:" + Environment.NewLine;
        static readonly String inComplete = "Cannot start. Settings are not complete." + Environment.NewLine;

        public NeutralControlBar()
        {
            InitializeComponent();
            startButton.Click += StartButtonClick;
            startWithRandom.Click += StartRandomButtonClick;
            startParallelBtn.Click += StartParallelButtonClick;
            showDocumentation.Click += DocButtonClick;
        }

        void StartParallelButtonClick(object sender, EventArgs e)
        {
            mainControl.StartParallelRun();
        }

        public void Init(MainControl mainControl)
        {
            this.mainControl = mainControl;
        }

        Settings Settings
        {
            get { return mainControl.settings; }
        }

        RichTextBox ConsoleTextBox
        {
            get { return mainControl.consoleTextBox; }
        }

        void StartButtonClick(object sender, EventArgs e)
        {
            if (!Settings.CanStartWithKnownLayout)
                return;

            var algorithm = new KeyboardEPAlgorithm(Settings.CurrentCorpus.FromJust, mainControl.settings.CurrentTimes.FromJust.Times, 
                mainControl.Logger);
            var layout = Settings.CurrentLayout.FromJust;
            algorithm.SetLayout(layout);

            ConsoleTextBox.AppendText(startingNew);
            ConsoleTextBox.AppendText(MainControl.PrintLayoutWithScore(layout, algorithm.CurrentScore));

            mainControl.Start(algorithm);
        }


        void DocButtonClick(object sender, EventArgs e)
        {
            ConsoleTextBox.Rtf = Resources.mainFormDoc;
        }


        void StartRandomButtonClick(object sender, EventArgs e)
        {
            if (Settings.CanStart)
            {
                mainControl.consoleTextBox.Clear();
                var layout = KeyboardLayout.Random(Keyboard.StandardKeyboard);
                ConsoleTextBox.AppendText("Starting algorithm with a random layout." + Environment.NewLine);
                var algorithm = new KeyboardEPAlgorithm(Settings.CurrentCorpus.FromJust, Settings.CurrentTimes.FromJust.Times, 
                    mainControl.Logger);
                algorithm.SetLayout(layout);

                ConsoleTextBox.AppendText(startingNew);
                ConsoleTextBox.AppendText(MainControl.PrintLayoutWithScore(layout, algorithm.CurrentScore));

                mainControl.Start(algorithm);
            }
            else
                ConsoleTextBox.AppendText(inComplete);
        }
    }
}
