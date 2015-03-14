using System;
using System.Drawing;
using System.Windows.Forms;
using Generic.Maths.Vectors;

namespace Generic.InputOutput.UI
{
    public partial class MoveBar : UserControl
    {
        private Point lastLocation;
        private Boolean mouseDown;

        public MoveBar()
        {
            InitializeComponent();
        }

        public override AnchorStyles Anchor
        {
            get { return AnchorStyles.Left | AnchorStyles.Top; }
        }

        private void MoveBar_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void MoveBar_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
        }

        private void MoveBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                Parent.Location = (IntVector2) Parent.Location + e.Location - lastLocation;
            }
            else
                lastLocation = e.Location;
        }
    }
}