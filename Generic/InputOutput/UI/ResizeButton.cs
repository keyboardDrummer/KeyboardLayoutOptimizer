using System;
using System.Drawing;
using System.Windows.Forms;
using Generic.Maths.Vectors;

namespace Generic.InputOutput.UI
{
    public partial class ResizeButton : UserControl
    {
        private Boolean mouseDown;
        private Point lastLocation;

        public ResizeButton()
        {
            InitializeComponent();
        }

        public override AnchorStyles Anchor
        {
            get { return AnchorStyles.Bottom | AnchorStyles.Right; }
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                Parent.Size = (IntVector2)Parent.Size + e.Location - lastLocation;
                Parent.Refresh();
            } else
                lastLocation = e.Location;
        }
    }
}
