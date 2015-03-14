using System.Collections.Generic;
using System.IO;

namespace Generic.InputOutput.Parsers.ListParsers
{
    public class ListStream<T>
    {
        readonly IList<T> list;
        int position = 0;

        public ListStream(IList<T> list)
        {
            this.list = list;
        }

        public int Position
        {
            get { return position; }
            set { position = value; }
        }

        public bool EndOfStream 
        {
            get { return list.Count == position; }
        }

        public T Read()
        {
            if (EndOfStream)
                throw new EndOfStreamException();
            return list[position++];
        }

        public T Peek()
        {
            return list[position];
        }
    }
}
