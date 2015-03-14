using System;

namespace Generic.Common
{
    public class Cached<T>
    {
        private readonly Func<T> func;

        public Cached(Func<T> func)
        {
            this.func = func;
        }

        public static implicit operator T(Cached<T> lazy)
        {
            return lazy.func();
        }

        public T Value { get { return func(); } }

        public static implicit operator Cached<T>(Func<T> func)
        {
            return new Cached<T>(func);
        }
    }
}
