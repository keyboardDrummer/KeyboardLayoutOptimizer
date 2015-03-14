using System;
using System.Collections.Generic;
using System.Linq;
using Generic.Common;
using Generic.Containers.Collections.List;
using Generic.InputOutput.Printing.Sized;

namespace Generic.InputOutput.Printing.Sizable
{
    internal class WrappedList : Document
    {
        readonly IList<Document> documents;

        public WrappedList(IList<Document> documents)
        {
            this.documents = documents;
        }

        internal override int GetMinimumWidth()
        {
            return documents.Select(d => d.GetMinimumWidth()).Aggregate(Math.Max);
        }

        public bool Equals(WrappedList other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return other.documents.SequenceEqual(documents);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != typeof(WrappedList))
                return false;
            return Equals((WrappedList)obj);
        }

        public override int GetHashCode()
        {
            return (documents != null ? HashUtil.GetOrderDependentHashCode(documents) : 0);
        }

        public static bool operator ==(WrappedList left, WrappedList right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(WrappedList left, WrappedList right)
        {
            return !Equals(left, right);
        }

        internal override SizedDocument GetSizedDocument(int? preferredWidth, Func<Document, int?, SizedDocument> recursiveCall)
        {
            var pDocs = documents.SelectList(document => recursiveCall(document,preferredWidth));
            if (!preferredWidth.HasValue)
                return pDocs.Aggregate((d1, d2) => d1 + d2);

            var currentWidth = 0;
            var docIndex = 0;
            var lineDoc = SizedDocument.Empty;
            var result = SizedDocument.Empty;
            while (docIndex < pDocs.Count)
            {
                var doc = pDocs[docIndex];
                if (currentWidth + doc.Size.X > preferredWidth)
                {
                    result = result ^ lineDoc;
                    lineDoc = SizedDocument.Empty;
                    currentWidth = 0;
                }
                lineDoc = lineDoc + doc;
                currentWidth += doc.Size.X;
                docIndex++;
            }
            result = result ^ lineDoc;
            return result;
        }
    }
}
