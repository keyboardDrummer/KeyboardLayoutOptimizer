using System;
using System.Windows.Forms;

namespace KeyboardEPCS.UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            this.mainControl1.Settings.Save();
        }
    }
}
