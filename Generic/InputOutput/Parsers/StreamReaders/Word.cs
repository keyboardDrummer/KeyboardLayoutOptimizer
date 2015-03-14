using System;
using System.IO;
using Generic.InputOutput.Printing.Sizable;

namespace Generic.InputOutput.Parsers.StreamReaders
{
    public class Word : StreamParser<String>
    {
        readonly String symbols;

        public Word(String symbols)
        {
            this.symbols = symbols;
        }

        public override ParseResult<string> InnerParse(MyStreamReader input)
        {
            var success = true;
            try
            {
                foreach (char t in symbols)
                    success &= t == input.Read();
                return success ? Success(symbols) : FailResult("could not parse" - (Document)symbols);
            }
            catch (EndOfStreamException)
            {
                return FailResult("end of stream");
            }
        }
    }
}
