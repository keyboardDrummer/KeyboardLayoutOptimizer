using System;
using Generic.InputOutput.Printing.Sized;

namespace Generic.InputOutput.Printing.Sizable
{
    class WrappedDocument : Document
    {
        readonly SizedDocument document;

        public WrappedDocument(SizedDocument document)
        {
            this.document = document;
        }

        internal override int GetMinimumWidth()
        {
            return document.Size.X;
        }

        internal override SizedDocument GetSizedDocument(int? preferredWidth, Func<Document, int?, SizedDocument> recursiveCall)
        {
            return document;
        }
    }
}
