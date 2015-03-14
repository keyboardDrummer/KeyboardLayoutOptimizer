namespace KeyboardEPCS.UI
{
    partial class SettingsControl
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.permutationsBox = new System.Windows.Forms.ListBox();
            this.corpiBox = new System.Windows.Forms.ListBox();
            this.typeStylesBox = new System.Windows.Forms.ListBox();
            this.addCorpusButton = new System.Windows.Forms.Button();
            this.corpusTitle = new System.Windows.Forms.Label();
            this.permutationTitle = new System.Windows.Forms.Label();
            this.typeStyleTitle = new System.Windows.Forms.Label();
            this.addCorpusTextButton = new System.Windows.Forms.Button();
            this.removeCorpusButton = new System.Windows.Forms.Button();
            this.removePermutationButton = new System.Windows.Forms.Button();
            this.createPermButton = new System.Windows.Forms.Button();
            this.viewCorpusButton = new System.Windows.Forms.Button();
            this.viewPermutationButton = new System.Windows.Forms.Button();
            this.monitorButton = new System.Windows.Forms.Button();
            this.removeTypeStylesButton = new System.Windows.Forms.Button();
            this.viewTypeStylesButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.openTxtFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.viewTextBox = new System.Windows.Forms.RichTextBox();
            this.defaultButton = new System.Windows.Forms.Button();
            this.statusIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.createTypeStyleButton = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.corpusPanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.typeStylePanel = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.permutationPanel = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.showDocumentationBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.corpusPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.typeStylePanel.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.permutationPanel.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // permutationsBox
            // 
            this.permutationsBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.permutationsBox.FormattingEnabled = true;
            this.permutationsBox.Location = new System.Drawing.Point(0, 16);
            this.permutationsBox.Name = "permutationsBox";
            this.permutationsBox.Size = new System.Drawing.Size(96, 326);
            this.permutationsBox.TabIndex = 0;
            // 
            // corpiBox
            // 
            this.corpiBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.corpiBox.FormattingEnabled = true;
            this.corpiBox.Location = new System.Drawing.Point(0, 16);
            this.corpiBox.Name = "corpiBox";
            this.corpiBox.Size = new System.Drawing.Size(96, 323);
            this.corpiBox.TabIndex = 1;
            // 
            // typeStylesBox
            // 
            this.typeStylesBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.typeStylesBox.FormattingEnabled = true;
            this.typeStylesBox.Location = new System.Drawing.Point(0, 16);
            this.typeStylesBox.Name = "typeStylesBox";
            this.typeStylesBox.Size = new System.Drawing.Size(90, 323);
            this.typeStylesBox.TabIndex = 2;
            // 
            // addCorpusButton
            // 
            this.addCorpusButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addCorpusButton.Location = new System.Drawing.Point(3, 90);
            this.addCorpusButton.Name = "addCorpusButton";
            this.addCorpusButton.Size = new System.Drawing.Size(99, 23);
            this.addCorpusButton.TabIndex = 3;
            this.addCorpusButton.Text = "Create blank";
            this.addCorpusButton.UseVisualStyleBackColor = true;
            this.addCorpusButton.Click += new System.EventHandler(this.AddCorpusButtonClick);
            // 
            // corpusTitle
            // 
            this.corpusTitle.AutoSize = true;
            this.corpusTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.corpusTitle.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.corpusTitle.Location = new System.Drawing.Point(0, 0);
            this.corpusTitle.Name = "corpusTitle";
            this.corpusTitle.Size = new System.Drawing.Size(48, 16);
            this.corpusTitle.TabIndex = 4;
            this.corpusTitle.Text = "Corpi";
            // 
            // permutationTitle
            // 
            this.permutationTitle.AutoSize = true;
            this.permutationTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.permutationTitle.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.permutationTitle.Location = new System.Drawing.Point(0, 0);
            this.permutationTitle.Name = "permutationTitle";
            this.permutationTitle.Size = new System.Drawing.Size(64, 16);
            this.permutationTitle.TabIndex = 5;
            this.permutationTitle.Text = "Layouts";
            // 
            // typeStyleTitle
            // 
            this.typeStyleTitle.AutoSize = true;
            this.typeStyleTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.typeStyleTitle.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.typeStyleTitle.Location = new System.Drawing.Point(0, 0);
            this.typeStyleTitle.Name = "typeStyleTitle";
            this.typeStyleTitle.Size = new System.Drawing.Size(88, 16);
            this.typeStyleTitle.TabIndex = 6;
            this.typeStyleTitle.Text = "Typestyles";
            // 
            // addCorpusTextButton
            // 
            this.addCorpusTextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addCorpusTextButton.Location = new System.Drawing.Point(3, 61);
            this.addCorpusTextButton.Name = "addCorpusTextButton";
            this.addCorpusTextButton.Size = new System.Drawing.Size(99, 23);
            this.addCorpusTextButton.TabIndex = 7;
            this.addCorpusTextButton.Text = "Add Text";
            this.addCorpusTextButton.UseVisualStyleBackColor = true;
            this.addCorpusTextButton.Click += new System.EventHandler(this.AddCorpusTextButtonClick);
            // 
            // removeCorpusButton
            // 
            this.removeCorpusButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.removeCorpusButton.Location = new System.Drawing.Point(3, 32);
            this.removeCorpusButton.Name = "removeCorpusButton";
            this.removeCorpusButton.Size = new System.Drawing.Size(99, 23);
            this.removeCorpusButton.TabIndex = 8;
            this.removeCorpusButton.Text = "Remove";
            this.removeCorpusButton.UseVisualStyleBackColor = true;
            this.removeCorpusButton.Click += new System.EventHandler(this.RemoveCorpusButtonClick);
            // 
            // removePermutationButton
            // 
            this.removePermutationButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.removePermutationButton.Location = new System.Drawing.Point(3, 32);
            this.removePermutationButton.Name = "removePermutationButton";
            this.removePermutationButton.Size = new System.Drawing.Size(101, 23);
            this.removePermutationButton.TabIndex = 11;
            this.removePermutationButton.Text = "Remove";
            this.removePermutationButton.UseVisualStyleBackColor = true;
            this.removePermutationButton.Click += new System.EventHandler(this.RemovePermutationButtonClick);
            // 
            // createPermButton
            // 
            this.createPermButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.createPermButton.Location = new System.Drawing.Point(3, 61);
            this.createPermButton.Name = "createPermButton";
            this.createPermButton.Size = new System.Drawing.Size(101, 49);
            this.createPermButton.TabIndex = 9;
            this.createPermButton.Text = "Go to layout creator";
            this.createPermButton.UseVisualStyleBackColor = true;
            this.createPermButton.Click += new System.EventHandler(this.CreatePermButtonClick);
            // 
            // viewCorpusButton
            // 
            this.viewCorpusButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.viewCorpusButton.Location = new System.Drawing.Point(3, 3);
            this.viewCorpusButton.Name = "viewCorpusButton";
            this.viewCorpusButton.Size = new System.Drawing.Size(99, 23);
            this.viewCorpusButton.TabIndex = 12;
            this.viewCorpusButton.Text = "View";
            this.viewCorpusButton.UseVisualStyleBackColor = true;
            this.viewCorpusButton.Click += new System.EventHandler(this.ViewCorpusButtonClick);
            // 
            // viewPermutationButton
            // 
            this.viewPermutationButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.viewPermutationButton.Location = new System.Drawing.Point(3, 3);
            this.viewPermutationButton.Name = "viewPermutationButton";
            this.viewPermutationButton.Size = new System.Drawing.Size(101, 23);
            this.viewPermutationButton.TabIndex = 13;
            this.viewPermutationButton.Text = "View";
            this.viewPermutationButton.UseVisualStyleBackColor = true;
            this.viewPermutationButton.Click += new System.EventHandler(this.ViewPermutationButtonClick);
            // 
            // monitorButton
            // 
            this.monitorButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.monitorButton.Location = new System.Drawing.Point(3, 61);
            this.monitorButton.Name = "monitorButton";
            this.monitorButton.Size = new System.Drawing.Size(101, 23);
            this.monitorButton.TabIndex = 14;
            this.monitorButton.Text = "Monitor strokes";
            this.monitorButton.UseVisualStyleBackColor = true;
            this.monitorButton.Click += new System.EventHandler(this.MonitorButtonClick);
            // 
            // removeTypeStylesButton
            // 
            this.removeTypeStylesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.removeTypeStylesButton.Location = new System.Drawing.Point(3, 32);
            this.removeTypeStylesButton.Name = "removeTypeStylesButton";
            this.removeTypeStylesButton.Size = new System.Drawing.Size(101, 23);
            this.removeTypeStylesButton.TabIndex = 15;
            this.removeTypeStylesButton.Text = "Remove";
            this.removeTypeStylesButton.UseVisualStyleBackColor = true;
            this.removeTypeStylesButton.Click += new System.EventHandler(this.RemoveTypeStylesButtonClick);
            // 
            // viewTypeStylesButton
            // 
            this.viewTypeStylesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.viewTypeStylesButton.Location = new System.Drawing.Point(3, 3);
            this.viewTypeStylesButton.Name = "viewTypeStylesButton";
            this.viewTypeStylesButton.Size = new System.Drawing.Size(101, 23);
            this.viewTypeStylesButton.TabIndex = 16;
            this.viewTypeStylesButton.Text = "View";
            this.viewTypeStylesButton.UseVisualStyleBackColor = true;
            this.viewTypeStylesButton.Click += new System.EventHandler(this.ViewTypeStylesButtonClick);
            // 
            // backButton
            // 
            this.backButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.backButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.backButton.Location = new System.Drawing.Point(726, 8);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 20;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.SaveSettingsButtonClick);
            // 
            // openTxtFileDialog
            // 
            this.openTxtFileDialog.Filter = "Text files|*.txt";
            this.openTxtFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenTxtFileDialogFileOk);
            // 
            // viewTextBox
            // 
            this.viewTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewTextBox.Location = new System.Drawing.Point(0, 0);
            this.viewTextBox.Name = "viewTextBox";
            this.viewTextBox.ReadOnly = true;
            this.viewTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.viewTextBox.Size = new System.Drawing.Size(493, 501);
            this.viewTextBox.TabIndex = 21;
            this.viewTextBox.Text = "";
            // 
            // defaultButton
            // 
            this.defaultButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.defaultButton.AutoSize = true;
            this.defaultButton.Location = new System.Drawing.Point(466, 8);
            this.defaultButton.Name = "defaultButton";
            this.defaultButton.Size = new System.Drawing.Size(131, 23);
            this.defaultButton.TabIndex = 25;
            this.defaultButton.Text = "Reset settings to default";
            this.defaultButton.UseVisualStyleBackColor = true;
            this.defaultButton.Click += new System.EventHandler(this.DefaultButtonClick);
            // 
            // statusIcon
            // 
            this.statusIcon.Text = "notifyIcon1";
            this.statusIcon.Click += new System.EventHandler(this.StatusIconClick);
            // 
            // createTypeStyleButton
            // 
            this.createTypeStyleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.createTypeStyleButton.Location = new System.Drawing.Point(3, 90);
            this.createTypeStyleButton.Name = "createTypeStyleButton";
            this.createTypeStyleButton.Size = new System.Drawing.Size(100, 23);
            this.createTypeStyleButton.TabIndex = 26;
            this.createTypeStyleButton.Text = "Create blank";
            this.createTypeStyleButton.UseVisualStyleBackColor = true;
            this.createTypeStyleButton.Click += new System.EventHandler(this.CreateTypeStyleButtonClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.splitContainer1.Location = new System.Drawing.Point(3, 41);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.viewTextBox);
            this.splitContainer1.Size = new System.Drawing.Size(808, 501);
            this.splitContainer1.SplitterDistance = 300;
            this.splitContainer1.SplitterWidth = 15;
            this.splitContainer1.TabIndex = 27;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tableLayoutPanel1.Controls.Add(this.corpusPanel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.typeStylePanel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.permutationPanel, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(300, 501);
            this.tableLayoutPanel1.TabIndex = 27;
            // 
            // corpusPanel
            // 
            this.corpusPanel.Controls.Add(this.panel2);
            this.corpusPanel.Controls.Add(this.panel1);
            this.corpusPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.corpusPanel.Location = new System.Drawing.Point(3, 3);
            this.corpusPanel.Name = "corpusPanel";
            this.corpusPanel.Size = new System.Drawing.Size(96, 495);
            this.corpusPanel.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.corpiBox);
            this.panel2.Controls.Add(this.corpusTitle);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(96, 339);
            this.panel2.TabIndex = 23;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 339);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(96, 156);
            this.panel1.TabIndex = 23;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.viewCorpusButton);
            this.flowLayoutPanel1.Controls.Add(this.removeCorpusButton);
            this.flowLayoutPanel1.Controls.Add(this.addCorpusTextButton);
            this.flowLayoutPanel1.Controls.Add(this.addCorpusButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(96, 156);
            this.flowLayoutPanel1.TabIndex = 28;
            // 
            // typeStylePanel
            // 
            this.typeStylePanel.Controls.Add(this.panel4);
            this.typeStylePanel.Controls.Add(this.panel3);
            this.typeStylePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.typeStylePanel.Location = new System.Drawing.Point(105, 3);
            this.typeStylePanel.Name = "typeStylePanel";
            this.typeStylePanel.Size = new System.Drawing.Size(90, 495);
            this.typeStylePanel.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.typeStylesBox);
            this.panel4.Controls.Add(this.typeStyleTitle);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(90, 339);
            this.panel4.TabIndex = 28;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.flowLayoutPanel2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 339);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(90, 156);
            this.panel3.TabIndex = 27;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel2.Controls.Add(this.viewTypeStylesButton);
            this.flowLayoutPanel2.Controls.Add(this.removeTypeStylesButton);
            this.flowLayoutPanel2.Controls.Add(this.monitorButton);
            this.flowLayoutPanel2.Controls.Add(this.createTypeStyleButton);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(90, 156);
            this.flowLayoutPanel2.TabIndex = 29;
            // 
            // permutationPanel
            // 
            this.permutationPanel.Controls.Add(this.panel6);
            this.permutationPanel.Controls.Add(this.panel5);
            this.permutationPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.permutationPanel.Location = new System.Drawing.Point(201, 3);
            this.permutationPanel.Name = "permutationPanel";
            this.permutationPanel.Size = new System.Drawing.Size(96, 495);
            this.permutationPanel.TabIndex = 2;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.permutationsBox);
            this.panel6.Controls.Add(this.permutationTitle);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(96, 342);
            this.panel6.TabIndex = 15;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.flowLayoutPanel3);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 342);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(96, 153);
            this.panel5.TabIndex = 14;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.viewPermutationButton);
            this.flowLayoutPanel3.Controls.Add(this.removePermutationButton);
            this.flowLayoutPanel3.Controls.Add(this.createPermButton);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(96, 153);
            this.flowLayoutPanel3.TabIndex = 30;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Controls.Add(this.backButton);
            this.flowLayoutPanel4.Controls.Add(this.showDocumentationBtn);
            this.flowLayoutPanel4.Controls.Add(this.defaultButton);
            this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel4.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Padding = new System.Windows.Forms.Padding(5);
            this.flowLayoutPanel4.Size = new System.Drawing.Size(814, 41);
            this.flowLayoutPanel4.TabIndex = 28;
            // 
            // showDocumentationBtn
            // 
            this.showDocumentationBtn.AutoSize = true;
            this.showDocumentationBtn.Location = new System.Drawing.Point(603, 8);
            this.showDocumentationBtn.Name = "showDocumentationBtn";
            this.showDocumentationBtn.Size = new System.Drawing.Size(117, 23);
            this.showDocumentationBtn.TabIndex = 26;
            this.showDocumentationBtn.Text = "Show documentation";
            this.showDocumentationBtn.UseVisualStyleBackColor = true;
            this.showDocumentationBtn.Click += new System.EventHandler(this.showDocumentationBtn_Click);
            // 
            // SettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel4);
            this.Controls.Add(this.splitContainer1);
            this.Name = "SettingsControl";
            this.Size = new System.Drawing.Size(814, 542);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.corpusPanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.typeStylePanel.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.permutationPanel.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button addCorpusButton;
        private System.Windows.Forms.Label corpusTitle;
        private System.Windows.Forms.Label permutationTitle;
        private System.Windows.Forms.Label typeStyleTitle;
        private System.Windows.Forms.Button addCorpusTextButton;
        private System.Windows.Forms.Button removeCorpusButton;
        private System.Windows.Forms.Button removePermutationButton;
        private System.Windows.Forms.Button createPermButton;
        private System.Windows.Forms.Button viewCorpusButton;
        private System.Windows.Forms.Button viewPermutationButton;
        private System.Windows.Forms.Button monitorButton;
        private System.Windows.Forms.Button removeTypeStylesButton;
        private System.Windows.Forms.Button viewTypeStylesButton;
        private System.Windows.Forms.Button backButton;
        public System.Windows.Forms.ListBox permutationsBox;
        public System.Windows.Forms.ListBox corpiBox;
        public System.Windows.Forms.ListBox typeStylesBox;
        private System.Windows.Forms.OpenFileDialog openTxtFileDialog;
        private System.Windows.Forms.RichTextBox viewTextBox;
        private System.Windows.Forms.Button defaultButton;
        private System.Windows.Forms.NotifyIcon statusIcon;
        private System.Windows.Forms.Button createTypeStyleButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel corpusPanel;
        private System.Windows.Forms.Panel typeStylePanel;
        private System.Windows.Forms.Panel permutationPanel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.Button showDocumentationBtn;
    }
}