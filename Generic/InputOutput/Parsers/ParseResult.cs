using System;
using Generic.Containers.Maybes;
using Generic.InputOutput.Printing;
using Generic.InputOutput.Printing.Sizable;

namespace Generic.InputOutput.Parsers
{
    public class ParseResult<Out>
    {
        Maybe<Out> result;
        Document _log;

        public ParseResult(Out value) : this(null,value)
        {
            
        }

        public ParseResult(Document log) : this(log,MaybeUtil.Nothing<Out>())
        {
            
        }

        public ParseResult(Document log, Out result)
            : this(log, MaybeUtil.Just(result))
        {

        }

        ParseResult(Document log, Maybe<Out> result)
        {
            this._log = log;
            this.result = result;
        }

        public Maybe<Out> Result
        {
            get { return result; }
        }

        public Document FinalLog
        {
            get { return Success ? "Succeeded with" - ((object)result.FromJust).Print() : _log; }
        }

        Document Log
        {
            get { return _log; }
        }

        public bool Success 
        {
            get { return result.IsJust; }
        }

        public Out JustResult
        {
            get { return result.FromJust; }
        }

        public bool Failure
        {
            get { return !Success; }
        }

        public ParseResult<Out2> Bind<Out2>(Func<Out,ParseResult<Out2>> func)
        {
            if (Success)
                return func(result.FromJust);
            return new ParseResult<Out2>(Log);
        }

        public ParseResult<Out> Or(Document log)
        {
            if (Failure)
                return new ParseResult<Out>(log ^ Log,result);
            return this;
        }

    }
}