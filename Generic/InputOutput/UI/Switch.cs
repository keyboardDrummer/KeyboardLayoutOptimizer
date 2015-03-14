using System;
using System.Windows.Forms;

namespace Generic.InputOutput.UI
{
    public partial class Switch : UserControl
    {
        public Boolean IsOn { get; set; }

        public Switch()
        {
            InitializeComponent();
            RefreshButton();
        }

        public event EventHandler SwitchedOn = delegate { };
        public event EventHandler SwitchedOff = delegate { };
        public event EventHandler Switched = delegate { };

        public String OnLabel
        {
            get { return _onLabel; }
            set 
            { 
                _onLabel = value;
                RefreshButton(); 
            }
        }

        public String OffLabel 
        { 
            get { return _offLabel; } 
            set 
            { 
                _offLabel = value;
                RefreshButton();
            }
        }

        private void RefreshButton()
        {
            button.Text = IsOn ? OnLabel : OffLabel;
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            IsOn = !IsOn;
            RefreshButton();
            Switched(sender,e);
            if (IsOn)
                SwitchedOn(sender, e);
            else
                SwitchedOff(sender, e);
        }

        private String _offLabel = "Off";
        private String _onLabel = "On";
    }
}
