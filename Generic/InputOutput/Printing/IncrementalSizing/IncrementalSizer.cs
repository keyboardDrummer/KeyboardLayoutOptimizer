using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generic.Common;
using Generic.InputOutput.Printing.Sizable;
using Generic.InputOutput.Printing.Sized;
using Generic.Containers.Collections.Dictionaries;
using Generic.Maths.Vectors;

namespace Generic.InputOutput.Printing.IncrementalSizing
{
    class IncrementalSizer
    {
        class IncrementalDocumentSizer
        {
            readonly IDictionary<int, SizedDocument> cache = new Dictionary<int, SizedDocument>();
            readonly IDictionary<int, IntVector2> sizeWithPreferredWidthRange = new Dictionary<int, IntVector2>();
            readonly Document document;
            readonly IncrementalSizer sizer;

            public IncrementalDocumentSizer(Document document, IncrementalSizer sizer)
            {
                this.document = document;
                this.sizer = sizer;
                var maxWidth = GetSized(int.MaxValue).Size.X;
                GetSized(int.MinValue + maxWidth);
            }

            int? FindWrapValue(int preferredWidth)
            {
                return sizeWithPreferredWidthRange.Where(range => range.Value.X <= preferredWidth && preferredWidth <= range.Value.Y).Select(kv => (int?)kv.Key).FirstOrDefault();
            }

            public SizedDocument GetSized(int preferredWidth)
            {
                var mbWrapValue = FindWrapValue(preferredWidth);
                if (mbWrapValue.HasValue)
                    return cache[mbWrapValue.Value];

                var result = document.GetSizedDocument(preferredWidth, (d, w) => sizer.GetSized(d, w.Value));
                var resultWidth = result.Size.X;
                
                cache[resultWidth] = result;
                sizeWithPreferredWidthRange.Get(resultWidth).Exec(
                    () => sizeWithPreferredWidthRange[resultWidth] = new IntVector2(Math.Min(resultWidth, preferredWidth), Math.Max(resultWidth, preferredWidth)), 
                    range => sizeWithPreferredWidthRange[resultWidth] = new IntVector2(Math.Min(range.X, preferredWidth), Math.Max(range.Y, preferredWidth)));
                return result;
            }
        }

        readonly IDictionary<Document, IncrementalDocumentSizer> documentSizers = new Dictionary<Document, IncrementalDocumentSizer>();
        readonly IDictionary<Document, SizedDocument> unwrappables = new Dictionary<Document, SizedDocument>();

        public SizedDocument GetSized(Document document, int preferredWidth)
        {
            var sizeFunc = documentSizers.Get(document).Eval(
                () => unwrappables.Get(document).Select<Func<int, SizedDocument>>(s => i => s).Default(() =>
            {
                var maxSize = document.GetSizedDocument(int.MaxValue).Size.X;
                var minSizedDocument = document.GetSizedDocument(int.MinValue + maxSize);
                var wrappable = minSizedDocument.Size.X != maxSize;
                if (wrappable)
                    return (documentSizers[document] = new IncrementalDocumentSizer(document, this)).GetSized;
                unwrappables[document] = minSizedDocument;
                return s => minSizedDocument;
            }), d => d.GetSized);
            return sizeFunc(preferredWidth);
        }
    }
}
