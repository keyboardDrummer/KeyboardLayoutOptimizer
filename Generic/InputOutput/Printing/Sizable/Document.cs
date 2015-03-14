using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Generic.Common;
using Generic.Functions;
using Generic.InputOutput.Printing.Sized;

namespace Generic.InputOutput.Printing.Sizable
{
    public abstract class Document : IPrintable
    {
        readonly Memory<int> _minimumWidth;

        public override string ToString()
        {
            return Render(null);
        }

        protected Document()
        {
            _minimumWidth = new Memory<int>(GetMinimumWidth);
        }

        public Document Print()
        {
            return this;
        }

        public bool IsEmpty
        {
            get { return GetMinimumWidth() == 0; }
        }


        internal int MinimumWidth
        {
            get
            {
                return _minimumWidth.Value;
            }
        }
        internal abstract int GetMinimumWidth();
        internal abstract SizedDocument GetSizedDocument(int? preferredWidth, Func<Document, int?, SizedDocument> recursiveCall);
 
        public static implicit operator Document(char input)
        {
            return input.ToString(CultureInfo.InvariantCulture);
        }

        public static implicit operator Document(string input)
        {
            return new WrappedDocument(new Text(input));
        }

        public static Document operator +(Document left, Document right)
        {
            if (left.IsEmpty)
                return right;
            if (right.IsEmpty)
                return left;
            return new LeftRight(left, right);
        }

        public static Document operator -(Document left, Document right)
        {
            return left.Concat(DocumentUtil.Space, right);
        }

        public Document Concat(Document seperator, Document right)
        {
            if (IsEmpty || right.IsEmpty)
                return this + right;
            return this + seperator + right;
        }

        internal SizedDocument GetSizedDocument(int? preferredWidth)
        {
            return GetSizedDocument(preferredWidth, (d, s) => d.GetSizedDocument(s));
        }

        public static Document operator ^(Document top, Document bottom)
        {
            if (top.IsEmpty)
                return bottom;
            if (bottom.IsEmpty)
                return top;
            return new TopBottom(top, bottom);
        }

        public static String WhiteSpace(int i)
        {
            return new string(Enumerable.Repeat(' ', i).ToArray());
        }

        public Document Indent(int i)
        {
            return new WrappedDocument(new WhiteSpace(i, 0)) + this;
        }

        public string Render(int? size)
        {
            return GetSizedDocument(size).Render();
        }

        public static Document Seperated(Document seperator, params object[] items)
        {
            return items.Seperated(seperator);
        }
    }
}
