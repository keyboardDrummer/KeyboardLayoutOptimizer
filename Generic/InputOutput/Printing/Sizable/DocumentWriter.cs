using System;
using System.Collections.Generic;
using System.Globalization;
using Generic.Containers.Collections.List;
using JetBrains.Annotations;

namespace Generic.InputOutput.Printing.Sizable
{
    public class DocumentWriter
    {
        readonly Stack<int> indentations = new Stack<int>(); //One size smaller than indentStack.
        readonly Stack<Document> indentStack = new Stack<Document>(ListUtil.New(DocumentUtil.Empty));
        [NotNull]Document currentLine = DocumentUtil.Empty;
        const int DEFAULT_INDENT = 2;

        public DocumentWriter Write(char text)
        {
            return Write(text.ToString(CultureInfo.InvariantCulture));
        }

        Document Done { get { return indentStack.Peek(); } set { indentStack.Pop(); indentStack.Push(value); } }
        public DocumentWriter Write(String text)
        {
            return Write(text.Print());
        }

        public DocumentWriter Write([NotNull]Document text)
        {
            currentLine += text;
            return this;
        }

        public DocumentWriter WriteLine(String text)
        {
            return WriteLine(text.Print());
        }

        public DocumentWriter WriteLine(Document text)
        {
            Write(text);
            WriteLine();
            return this;
        }

        public Document ToDocument()
        {
            return Done ^ currentLine;
        }

        public DocumentWriter WriteLine()
        {
            Done ^= currentLine.IsEmpty ? DocumentUtil.Space : currentLine;
            currentLine = DocumentUtil.Empty;
            return this;
        }

        public DocumentWriter FinishLine()
        {
            Done ^= currentLine;
            currentLine = DocumentUtil.Empty;
            return this;
        }

        public void StartBlock(int indent = DEFAULT_INDENT)
        {
            FinishLine();
            indentStack.Push(DocumentUtil.Empty);
            indentations.Push(indent);
        }

        public void EndBlock()
        {
            WriteLine();
            var block = indentStack.Pop().Indent(indentations.Pop());
            Done ^= block;
        }

        public override string ToString()
        {
            return ToDocument().ToString();
        }
    }
}
