using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Generic.Containers.Collections.List;
using Generic.Containers.Maybes;
using Generic.Containers.Pointers;
using KeyboardEPCS.Logic.Inputs;
using KeyboardEPCS.Logic.Transitions;

namespace KeyboardEPCS.UI
{
    public partial class SettingsControl : UserControl
    {
        readonly MainControl mainForm;
        readonly MyBindingList<Corpus> corpiList;
        readonly MyBindingList<KeyboardLayout> layoutList;
        readonly MyBindingList<TransitionTimeBuilder> styleList;
        Timer keyPressTimer;

        public SettingsControl(MainControl mainForm)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.mainForm = mainForm;
            statusIcon.Icon = Properties.Resources.Keyboard3;
            viewTextBox.Rtf = Properties.Resources.settingFormDoc;

            corpiList = InitBox(corpiBox, PointerUtil.New(() => Settings.CurrentCorpus, v => Settings.CurrentCorpus = v), Settings.Corpi);
            layoutList = InitBox(permutationsBox, PointerUtil.New(() => Settings.CurrentLayout, v => Settings.CurrentLayout = v), Settings.KeyboardLayouts);
            styleList = InitBox(typeStylesBox, PointerUtil.New(() => Settings.CurrentTimes, v => Settings.CurrentTimes = v), Settings.TransitionTimess);
        }


        public new void Show()
        {
            this.Visible = true;
            corpiList.RaiseListChanged();
            layoutList.RaiseListChanged();
            styleList.RaiseListChanged();
        }

        static MyBindingList<T> InitBox<T>(ListBox box, Pointer<Maybe<T>> current, IList<Tuple<T,string>> list) where T : class
        {
            var bindingList = new MyBindingList<T>(list,box,current);
            box.DisplayMember = "Item2";
            box.DataSource = bindingList;
            box.SelectedItem = current.Value.ToNull();
            box.SelectedIndexChanged += (s, e) => SetSelectedIndex(box, current);
            return bindingList;
        }

        static Maybe<T> SetSelectedIndex<T>(ListBox box, Pointer<Maybe<T>> current) where T : class
        {
            return current.Value = ((Tuple<T,string>)box.SelectedItem).FromNull().Select(tup => tup.Item1);
        }

        class MyBindingList<T> : DefaultBindingList<Tuple<T,string>>
            where T : class
        {
            readonly ListBox box;
            readonly Pointer<Maybe<T>> currentPointer;

            public MyBindingList(IList<Tuple<T, string>> content, ListBox box, Pointer<Maybe<T>> currentPointer)
                : base(content)
            {
                this.box = box;
                this.currentPointer = currentPointer;
            }

            public override void Insert(int index, Tuple<T, string> item)
            {
                base.Insert(index, item);
                SetSelectedIndex(box, currentPointer);
            }
        }

        Settings Settings { get { return mainForm.Settings; } set { mainForm.Settings = value; } }
        
        private void SaveSettingsButtonClick(object sender, EventArgs e)
        {
            Visible = false;
            mainForm.Visible = true;
            Settings.Save();
        }

        private void AddCorpusTextButtonClick(object sender, EventArgs e)
        {
            if (Settings.CurrentCorpus.IsJust)
                openTxtFileDialog.ShowDialog();
        }

        private void OpenTxtFileDialogFileOk(object sender, CancelEventArgs e)
        {
            Settings.CurrentCorpus.FromJust.AddFileText(openTxtFileDialog.FileName);
        }

        private void ViewCorpusButtonClick(object sender, EventArgs e)
        {
            if (Settings.CurrentCorpus.IsJust)
            {
                viewTextBox.Clear();
                viewTextBox.AppendText("Current corpus is:" + Environment.NewLine);
                viewTextBox.AppendText(Settings.CurrentCorpus.FromJust.Print().ToString());
                viewTextBox.AppendText(Environment.NewLine);
            }
        }

        static readonly bool viewTypeStyleUsingCurrentLayout = true;
        private void ViewTypeStylesButtonClick(object sender, EventArgs e)
        {
            if (Settings.CurrentTimes.IsJust)
            {
                viewTextBox.Clear();
                viewTextBox.AppendText("Current Typestyle is:" + Environment.NewLine);
                if (viewTypeStyleUsingCurrentLayout && Settings.CurrentLayout.IsJust)
                {
                    viewTextBox.AppendText(Settings.CurrentTimes.FromJust.Print(Settings.CurrentLayout.FromJust).ToString());
                }
                else
                    viewTextBox.AppendText(Settings.CurrentTimes.FromJust.Print().ToString());
                viewTextBox.AppendText(Environment.NewLine);
                
            }
        }

        private void ViewPermutationButtonClick(object sender, EventArgs e)
        {
            if (!Settings.CurrentLayout.IsJust)
                return;

            viewTextBox.Clear();
            var keyboardLayout = Settings.CurrentLayout.FromJust;
            viewTextBox.AppendText("Current layout is:" + Environment.NewLine +
                (Settings.CanStartWithKnownLayout ? MainControl.PrintLayoutWithScore(keyboardLayout, 
                KeyboardEPAlgorithm.Score(keyboardLayout, Settings.CurrentCorpus.FromJust, Settings.CurrentTimes.FromJust.Times))
                : keyboardLayout.ToString()));
            viewTextBox.AppendText(Environment.NewLine);
        }

        private void AddCorpusButtonClick(object sender, EventArgs e)
        {
            using(var nameForm = new EnterNameForm())
            {
                if (nameForm.ShowDialog() == DialogResult.OK)
                    corpiList.Add(Tuple.Create(Corpus.GetEmpty(Keyboard.StandardKeyboard), nameForm.EnteredName));
            }
        }

        private void CreateTypeStyleButtonClick(object sender, EventArgs e)
        {
            using (var nameForm = new EnterNameForm())
            {
                if (nameForm.ShowDialog() == DialogResult.OK)
                    styleList.Add(Tuple.Create(TransitionTimeBuilder.GetEmpty(Keyboard.StandardKeyboard), nameForm.EnteredName));
            }
        }

        public void AddPermutation(KeyboardLayout layout, string name)
        {
            layoutList.Add(Tuple.Create(layout, name));
        }

        private void CreatePermButtonClick(object sender, EventArgs e)
        {
            var layoutForm = new CreateLayoutForm(this);
            layoutForm.SetLayout(Settings.CurrentLayout.Default(KeyboardLayout.QWERTY));
            layoutForm.Show();
        }

        private void RemoveCorpusButtonClick(object sender, EventArgs e)
        {
            Settings.CurrentCorpus.Exec(Console.Beep, s => corpiList.Remove(i => i.Item1 == s));
        }

        private void RemovePermutationButtonClick(object sender, EventArgs e)
        {
            Settings.CurrentLayout.Exec(Console.Beep, s => layoutList.Remove(i => i.Item1 == s));
        }

        private void RemoveTypeStylesButtonClick(object sender, EventArgs e)
        {
            Settings.CurrentTimes.Exec(Console.Beep, s => styleList.Remove(i => i.Item1 == s));
        }

        private void DefaultButtonClick(object sender, EventArgs e)
        {
            var standard = Settings.Standard;
            Settings.Corpi.Clear();
            Settings.Corpi.AddRange(standard.Corpi);
            Settings.KeyboardLayouts.Clear();
            Settings.KeyboardLayouts.AddRange(standard.KeyboardLayouts);
            Settings.TransitionTimess.Clear();
            Settings.TransitionTimess.AddRange(standard.TransitionTimess);
            Show();
        }

        private void MonitorButtonClick(object sender, EventArgs e)
        {
            if (Settings.CurrentTimes.IsJust)
            {
                Parent.Hide();
                keyPressTimer = new KeypressTimer(Settings.CurrentTimes.FromJust);
                statusIcon.Visible = true;
            }
            else
                System.Media.SystemSounds.Beep.Play();
        }

        private void StatusIconClick(object sender, EventArgs e)
        {
            keyPressTimer.Enabled = false;
            Parent.Show();
            statusIcon.Visible = false;
        }

        private void showDocumentationBtn_Click(object sender, EventArgs e)
        {
            viewTextBox.Rtf = Properties.Resources.settingFormDoc;
        }
    }
}
