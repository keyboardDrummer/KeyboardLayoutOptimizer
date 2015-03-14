using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generic.Containers.Maybes;

namespace Generic.Common
{
    public class Memory<T>
    {
        readonly Func<T> func;
        Maybe<T> cache = MaybeUtil.Nothing<T>();

        public Memory(Func<T> func)
        {
            this.func = func;
        }

        public T Value
        {
            get { return cache.IsJust ? cache.FromJust : (cache = MaybeUtil.Just(func())).FromJust; }
        }

        public Func<U> Select<U>(Func<T,U> uFunc)
        {
            return cache.Eval<Func<U>>(() => () => uFunc(func()), value => () => uFunc(value));
        }
    }
}
