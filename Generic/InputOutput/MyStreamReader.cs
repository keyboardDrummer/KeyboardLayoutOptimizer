using System;
using System.IO;

namespace Generic.InputOutput
{
    public class MyStreamReader
    {
        const int CACHE_SIZE = 1000;
        readonly byte[] cache = new byte[CACHE_SIZE];
        readonly Stream stream;
        int cacheLeft;
        int position;
        readonly long streamLength;

        public MyStreamReader(Stream stream)
        {
            this.stream = stream;
            streamLength = stream.Length;
            cacheLeft = -CACHE_SIZE;
        }

        public int Position
        {
            get { return position; }
            set { position = value; }
        }

        public bool EndOfStream
        {
            get { return position >= streamLength; }
        }

        public char Read()
        {
            if (EndOfStream)
                throw new EndOfStreamException();
            int cachePosition = position - cacheLeft;
            if (cachePosition >= 0 && cachePosition < CACHE_SIZE)
            {
                position++;
                return (char)cache[cachePosition];
            }
            cacheLeft = Math.Max(0, position - CACHE_SIZE / 2);
            stream.Position = cacheLeft;
            stream.Read(cache, 0, CACHE_SIZE);
            return Read();
        }
    }
}
