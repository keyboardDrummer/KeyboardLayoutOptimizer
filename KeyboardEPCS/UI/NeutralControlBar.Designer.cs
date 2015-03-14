namespace KeyboardEPCS.UI
{
    partial class NeutralControlBar
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.startButton = new System.Windows.Forms.Button();
            this.startWithRandom = new System.Windows.Forms.Button();
            this.showDocumentation = new System.Windows.Forms.Button();
            this.startParallelBtn = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.startButton);
            this.flowLayoutPanel1.Controls.Add(this.startWithRandom);
            this.flowLayoutPanel1.Controls.Add(this.startParallelBtn);
            this.flowLayoutPanel1.Controls.Add(this.showDocumentation);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(451, 29);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // startButton
            // 
            this.startButton.AutoSize = true;
            this.startButton.Location = new System.Drawing.Point(3, 3);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            // 
            // startWithRandom
            // 
            this.startWithRandom.AutoSize = true;
            this.startWithRandom.Location = new System.Drawing.Point(84, 3);
            this.startWithRandom.Name = "startWithRandom";
            this.startWithRandom.Size = new System.Drawing.Size(130, 23);
            this.startWithRandom.TabIndex = 2;
            this.startWithRandom.Text = "Start with random layout";
            this.startWithRandom.UseVisualStyleBackColor = true;
            // 
            // showDocumentation
            // 
            this.showDocumentation.AutoSize = true;
            this.showDocumentation.Location = new System.Drawing.Point(319, 3);
            this.showDocumentation.Name = "showDocumentation";
            this.showDocumentation.Size = new System.Drawing.Size(129, 23);
            this.showDocumentation.TabIndex = 1;
            this.showDocumentation.Text = "Show Documentation";
            this.showDocumentation.UseVisualStyleBackColor = true;
            // 
            // startParallelBtn
            // 
            this.startParallelBtn.AutoSize = true;
            this.startParallelBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.startParallelBtn.Location = new System.Drawing.Point(220, 3);
            this.startParallelBtn.Name = "startParallelBtn";
            this.startParallelBtn.Size = new System.Drawing.Size(93, 23);
            this.startParallelBtn.TabIndex = 3;
            this.startParallelBtn.Text = "Start parallel run";
            this.startParallelBtn.UseVisualStyleBackColor = true;
            // 
            // NeutralControlBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "NeutralControlBar";
            this.Size = new System.Drawing.Size(451, 29);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        public System.Windows.Forms.Button startButton;
        public System.Windows.Forms.Button showDocumentation;
        public System.Windows.Forms.Button startWithRandom;
        private System.Windows.Forms.Button startParallelBtn;
    }
}
