using System;
using System.Collections.Generic;
using Generic.Common;
using Generic.InputOutput.Printing;
using Generic.InputOutput.Printing.Sizable;

namespace Generic.InputOutput.Parsers
{
    public abstract class Parser<TIn, TOut>
    {
        String name;

        public virtual ParseResult<TOut> Parse(TIn input)
        {
            //Name.Maybe(() => Console.WriteLine("attempting to parse " + Name));

            var result = InnerParse(input);
            
            //if (result.Success)
            //    WriteSuccess(result.JustResult);
            if (result.Failure)
                return new ParseResult<TOut>(NamePrefix - result.FinalLog);
            return result;
        }

        Document NamePrefix { get { return name.Maybe(() => name + DocumentUtil.Colon, DocumentUtil.Empty); } }

        void WriteSuccess(TOut result)
        {
            Name.Maybe(n => Console.WriteLine("parsed " + n + ", resulting in " + result));
        }

        public abstract ParserFuncs<TIn> Funcs { get;}
        public abstract ParseResult<TOut> InnerParse(TIn input);
        public Parser<TIn, TOut2> Bind<TOut2>(Func<TOut, Parser<TIn, TOut2>> other)
        {
            return Funcs.Bind(this, other);
        }
        public Parser<TIn, TOut> Or(Parser<TIn, TOut> other)
        {
            return Funcs.Or(this,other);
        }

        public virtual Parser<TIn, IList<TOut>> Many()
        {
            return Funcs.Many(this);

        }

        public string Name
        {
            get { return name; }
        }

        public Parser<TIn,TOut> SetName(string name)
        {
            this.name = name;
            return this;
        }

        public ParseResult<TOut> FailResult(Document log)
        {
            return new ParseResult<TOut>(log);
        }

        public ParseResult<TOut> Success(TOut value, Document log = null)
        {
            //log = log ?? Name.Maybe(() => Document.Empty + Name | "succeeded with" | Document.Text(value), Document.Empty);
            return new ParseResult<TOut>(log,value);
        }
    }
}