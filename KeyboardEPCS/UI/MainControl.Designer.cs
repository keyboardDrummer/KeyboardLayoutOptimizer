using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using KeyboardEPCS.Logic.Inputs;

namespace KeyboardEPCS.UI
{
    partial class MainControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.clearButton = new Button();
            this.settingsButton = new Button();
            this.progressLabel = new ToolStripStatusLabel();
            this.consoleTextBox = new RichTextBox();
            this.runningControlBar1 = new RunningControlBar();
            this.neutralControlBar1 = new NeutralControlBar();
            this.parallelRunControlBar1 = new ParallelRunControlBar();
            this.SuspendLayout();
            // 
            // clearButton
            // 
            this.clearButton.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
            this.clearButton.Location = new Point(949, 12);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new Size(104, 23);
            this.clearButton.TabIndex = 10;
            this.clearButton.Text = "Clear console";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new EventHandler(this.ClearButtonClick);
            // 
            // settingsButton
            // 
            this.settingsButton.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
            this.settingsButton.Location = new Point(1059, 12);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new Size(75, 23);
            this.settingsButton.TabIndex = 14;
            this.settingsButton.Text = "Settings";
            this.settingsButton.UseVisualStyleBackColor = true;
            this.settingsButton.Click += new EventHandler(this.SettingsButtonClick);
            // 
            // progressLabel
            // 
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new Size(199, 17);
            this.progressLabel.Text = "Progress for current mutationCount:";
            // 
            // consoleTextBox
            // 
            this.consoleTextBox.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) 
            | AnchorStyles.Left) 
            | AnchorStyles.Right)));
            this.consoleTextBox.BackColor = Color.White;
            this.consoleTextBox.Font = new Font("Courier New", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.consoleTextBox.Location = new Point(12, 41);
            this.consoleTextBox.Name = "consoleTextBox";
            this.consoleTextBox.ReadOnly = true;
            this.consoleTextBox.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
            this.consoleTextBox.Size = new Size(1122, 696);
            this.consoleTextBox.TabIndex = 18;
            this.consoleTextBox.Text = "";
            // 
            // runningControlBar1
            // 
            this.runningControlBar1.AutoSize = true;
            this.runningControlBar1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.runningControlBar1.Location = new Point(12, 6);
            this.runningControlBar1.Name = "runningControlBar1";
            this.runningControlBar1.Size = new Size(247, 29);
            this.runningControlBar1.TabIndex = 19;
            // 
            // neutralControlBar1
            // 
            this.neutralControlBar1.AutoSize = true;
            this.neutralControlBar1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.neutralControlBar1.Location = new Point(12, 6);
            this.neutralControlBar1.Name = "neutralControlBar1";
            this.neutralControlBar1.Size = new Size(352, 29);
            this.neutralControlBar1.TabIndex = 20;
            // 
            // parallelRunControlBar1
            // 
            this.parallelRunControlBar1.AutoSize = true;
            this.parallelRunControlBar1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.parallelRunControlBar1.Location = new Point(12, 6);
            this.parallelRunControlBar1.Name = "parallelRunControlBar1";
            this.parallelRunControlBar1.ParallelRunCount = 10;
            this.parallelRunControlBar1.Size = new Size(81, 29);
            this.parallelRunControlBar1.TabIndex = 21;
            // 
            // MainControl
            // 
            this.Controls.Add(this.runningControlBar1);
            this.Controls.Add(this.consoleTextBox);
            this.Controls.Add(this.settingsButton);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.neutralControlBar1);
            this.Controls.Add(this.parallelRunControlBar1);
            this.Name = "MainControl";
            this.Size = new Size(1146, 749);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button clearButton;
        private Button settingsButton;
        private ToolStripStatusLabel progressLabel;
        public RichTextBox consoleTextBox;
        public RunningControlBar runningControlBar1;
        private NeutralControlBar neutralControlBar1;
        private ParallelRunControlBar parallelRunControlBar1;
    }
}

