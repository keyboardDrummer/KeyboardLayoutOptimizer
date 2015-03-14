using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Generic.InputOutput.Printing;
using Generic.InputOutput.Printing.IncrementalSizing;
using Generic.InputOutput.Printing.Sizable;
using JetBrains.Annotations;

namespace Generic.InputOutput.UI
{
    public class DocumentViewer : UserControl
    {
        readonly TextBox textBox;
        DocumentWriter writer;
        IncrementalSizer sizer = new IncrementalSizer();

        public DocumentViewer()
        {
            textBox = new TextBox();
            textBox.Location = new Point(0, 0);
            Controls.Add(textBox);
            textBox.Dock = DockStyle.Fill;
            textBox.ReadOnly = true;
            textBox.WordWrap = false;
            textBox.Multiline = true;
            textBox.ScrollBars = ScrollBars.Vertical; // RichTextBoxScrollBars.ForcedVertical;

            Font = new Font("Courier New", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            writer = new DocumentWriter();
        }

        public override Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; textBox.BackColor = value; }
        }

        public override Font Font
        {
            get { return base.Font; }
            set { base.Font = value; textBox.Font = value; }
        }

        Tuple<int, Document> lastKey;
        void SetText()
        {
            var charsPerLine = GetCharsPerLine();
            SetText(charsPerLine);
        }

        void SetText(int charsPerLine)
        {
            var sizedDocument = sizer.GetSized(Document, charsPerLine);
            var text = sizedDocument.Render();
            var index = textBox.GetCharIndexFromPosition(new Point(0,0));
            var lineSize = sizedDocument.Size.X + 1;
            var line = index / lineSize + 1;
            textBox.Text = text;
            var newIndex = lineSize * line;
            textBox.Select(newIndex, 0);
            textBox.ScrollToCaret();
        }

        int GetCharsPerLine()
        {
            var graphics = CreateGraphics();
            const string testString = "abcefghijklmnopqrstuvwxyz";
            var size = graphics.MeasureString(testString, textBox.Font);
            var charsPerLine = (int)(testString.Length * textBox.ClientSize.Width / size.Width);
            return charsPerLine;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            SetText();
        }

        [PublicAPI]
        public void Write(Document document)
        {
            writer.Write(document);
            SetText();
        }

        [PublicAPI]
        public void InvokeWrite(Document document)
        {
            writer.Write(document);
            InvokeSetText();
        }

        [PublicAPI]
        public void InvokeWriteLine(Document document)
        {
            writer.WriteLine(document);
            InvokeSetText();
        }

        void InvokeSetText()
        {
            Invoke(new Action(() =>
            {
                var newKey = Tuple.Create(GetCharsPerLine(), Document);
                if (Equals(lastKey, newKey))
                    return;

                lastKey = newKey;
                SetText(newKey.Item1);
            }));
        }

        [NotNull]
        [PublicAPI]
        public Document Document
        {
            get { return writer.ToDocument(); }
            set
            {
                writer = new DocumentWriter();
                writer.Write(value);
                SetText();
            }
        }
    }
}
