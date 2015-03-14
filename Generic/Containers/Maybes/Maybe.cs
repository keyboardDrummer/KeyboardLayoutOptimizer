using System;
using System.Security.Principal;
using Generic.Cloneables;
using Generic.Common;
using Generic.InputOutput.Printing;
using Generic.InputOutput.Printing.Sizable;
using JetBrains.Annotations;

namespace Generic.Containers.Maybes
{
    [Serializable]
    public abstract class Maybe<T> : DefaultObject, IGenCloneable1<Maybe<T>,T>, IPrintable
    {
        public abstract bool IsJust { get; }
        public abstract T FromJust { get; }
        public static readonly Maybe<T> Nothing = new Nothing<T>(); 
        public bool IsNothing
        {
            get { return !IsJust; }
        }

        public abstract R Eval<R>([InstantHandle]Func<R> nothing, [InstantHandle]Func<T, R> just);

        public R Eval<R>(R nothing, [InstantHandle]Func<T, R> result)
        {
            return Eval(() => nothing, result);
        }

        public abstract void Exec([InstantHandle]Action nothing, [InstantHandle]Action<T> just);
        public void Exec([InstantHandle]Action<T> just)
        {
            Exec(() => { }, just);
        }


        public static Maybe<T> Or(Func<Maybe<T>> a, Func<Maybe<T>> b)
        {
            var ar = a();
            return ar.Eval(b(), x => ar);
        }

        public virtual Maybe<B> Select<B>(Func<T, B> func)
        {
            return Eval(Nothing<B>.Nothing, t => MaybeUtil.Just(func(t)));
        }

        public T Default([InstantHandle]Func<T> defaultt)
        {
            return IsJust ? FromJust : defaultt();
        }

        public T Default(T defaultt = default(T))
        {
            return IsJust ? FromJust : defaultt ;
        }

        [Obsolete]
        public T JustOrDefault()
        {
            return IsJust ? FromJust : default(T);
        }

        public abstract Maybe<T> Clone(Func<T, T> cloneElement);
        public Document Print()
        {
            return Eval(() => "Nothing", t => "Just" - t.Print());
        }


    }
}