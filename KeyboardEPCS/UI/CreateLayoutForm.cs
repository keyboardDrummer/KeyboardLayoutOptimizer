using System;
using System.Drawing;
using System.Windows.Forms;
using KeyboardEPCS.Logic.Inputs;

namespace KeyboardEPCS.UI
{
    public partial class CreateLayoutForm : Form
    {
        KeyboardLayout currentLayout;
        readonly SettingsControl settingsControl;
        readonly Button[] keyboardButtons;
        int? selectedPosition;
        readonly Color stdBackColor = SystemColors.ActiveBorder;
        readonly Color selectedBackColor = SystemColors.GradientActiveCaption;

        public void SetLayout(KeyboardLayout layout)
        {
            currentLayout = layout.Clone();

            for (var keyboardPosition = 0; keyboardPosition < keyboardButtons.Length; keyboardPosition++)
            {
                keyboardButtons[keyboardPosition].Tag = (Int16)keyboardPosition;
                keyboardButtons[keyboardPosition].Click += KeyboardButtonClick;
                UpdateText(keyboardPosition);
            }
        }

        public CreateLayoutForm(SettingsControl parent)
        {
            InitializeComponent();
            settingsControl = parent;
            keyboardButtons = new Button[30];
            keyboardButtons[0] = pos10Button;
            keyboardButtons[1] = pos11Button;
            keyboardButtons[2] = pos12Button;
            keyboardButtons[3] = pos13Button;
            keyboardButtons[4] = pos14Button;
            keyboardButtons[5] = pos15Button;
            keyboardButtons[6] = pos16Button;
            keyboardButtons[7] = pos17Button;
            keyboardButtons[8] = pos18Button;
            keyboardButtons[9] = pos19Button;
            keyboardButtons[10] = pos20Button;
            keyboardButtons[11] = pos21Button;
            keyboardButtons[12] = pos22Button;
            keyboardButtons[13] = pos23Button;
            keyboardButtons[14] = pos24Button;
            keyboardButtons[15] = pos25Button;
            keyboardButtons[16] = pos26Button;
            keyboardButtons[17] = pos27Button;
            keyboardButtons[18] = pos28Button;
            keyboardButtons[19] = pos29Button;
            keyboardButtons[20] = pos30Button;
            keyboardButtons[21] = pos31Button;
            keyboardButtons[22] = pos32Button;
            keyboardButtons[23] = pos33Button;
            keyboardButtons[24] = pos34Button;
            keyboardButtons[25] = pos35Button;
            keyboardButtons[26] = pos36Button;
            keyboardButtons[27] = pos37Button;
            keyboardButtons[28] = pos38Button;
            keyboardButtons[29] = pos39Button;

            KeyPreview = true;
        }

        void UpdateText(int i)
        {
            keyboardButtons[i].Text = currentLayout[i].ToString();
        }

        private void KeyboardButtonClick(object sender, EventArgs e)
        {
            if (selectedPosition.HasValue)
                keyboardButtons[selectedPosition.Value].BackColor = stdBackColor;
            selectedPosition = (Int16)((Button)sender).Tag;
            keyboardButtons[selectedPosition.Value].BackColor = selectedBackColor;
        }

        private void BackButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void CreatePermutationButtonClick(object sender, EventArgs e)
        {
            using(var nameForm = new EnterNameForm())
            {
                if (nameForm.ShowDialog() == DialogResult.OK)
                {
                    settingsControl.AddPermutation(currentLayout, nameForm.EnteredName);
                    Close();
                }
            }
        }

        int? FindValue(char character)
        {
            for (var position = 0; position < KeyboardLayout.QWERTY.TotalPositions; position++)
            {
                if (currentLayout[position].RegularChar == character)
                    return position;
            }
            return null;
        }

        private void CreatePermutationKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!selectedPosition.HasValue)
                return;

            var pressedCharValue = e.KeyChar;
            var collidingButton = FindValue(pressedCharValue);
            if (collidingButton.HasValue)
            {
                var currentCharValue = currentLayout[selectedPosition.Value].RegularChar;
                if (currentCharValue != pressedCharValue)
                {
                    currentLayout[collidingButton.Value].RegularChar = currentCharValue;
                    currentLayout[selectedPosition.Value].RegularChar = pressedCharValue;
                    UpdateText(collidingButton.Value);
                    UpdateText(selectedPosition.Value);
                }
            }
        }
    }
}
