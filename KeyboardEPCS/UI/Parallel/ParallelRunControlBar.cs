using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Generic;
using Generic.Common;
using Generic.Containers.Collections.Set;
using KeyboardEPCS.Logic.Inputs;
using KeyboardEPCS.Logic.Transitions;

namespace KeyboardEPCS.UI
{
    public partial class ParallelRunControlBar : UserControl
    {
        int parallelRunCount = 1000;

        MainControl mainControl;
        WorkerThread reduceWorker;

        public ParallelRunControlBar()
        {
            InitializeComponent();
        }

        public void Init(MainControl mainControl)
        {
            this.mainControl = mainControl;
            stopButton.Click += StopButtonClick;
        }

        public int ParallelRunCount
        {
            get { return parallelRunCount; }
            set { parallelRunCount = value; }
        }

        MainControl.States State
        {
            get { return mainControl.State; }
            set { mainControl.State = value; }
        }

        public void Start()
        {
            State = MainControl.States.RunningParallel;
            reduceWorker = new WorkerThread(parallelRunCount, mainControl);
        }

        void Stop()
        {
            State = MainControl.States.Stopped;
            reduceWorker.Stop();
        }

        void StopButtonClick(object sender, EventArgs e)
        {
            Stop();
            mainControl.consoleTextBox.AppendText("Stopping current algorithm." + Environment.NewLine + Environment.NewLine);
        }
    }
}
