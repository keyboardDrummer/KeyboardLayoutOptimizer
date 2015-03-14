using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace KeyboardEPCS.UI
{
    public partial class EnterNameForm : Form
    {
        public EnterNameForm(string initialName = "")
        {
            InitializeComponent();
            nameTextBox.Text = initialName;
            nameTextBox.Validating += ValidateTextBox;
        }

        void ValidateTextBox(object sender, CancelEventArgs cancelEventArgs)
        {
            if (String.IsNullOrEmpty(nameTextBox.Text) || nameTextBox.Text.Contains("%")) //XXX fix check.
                cancelEventArgs.Cancel = true;
        }

        public string EnteredName
        {
            get { return nameTextBox.Text; }
            set { nameTextBox.Text = value; }
        }
    }
}
