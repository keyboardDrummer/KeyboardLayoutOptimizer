using System;
using System.Collections.Generic;
using Generic.Containers.Collections.List;

namespace Generic.Common
{
    public static class ObjectUtil
    {
        public static IList<T> Singleton<T>(this T obj)
        {
            return ListUtil.Singleton(obj);
        }

        public static void Maybe<T>(this T t, Action action) where T : class
        {
            if (t != null)
                action();
        }

        public static void Maybe<T>(this T t, Action<T> action) where T : class
        {
            if (t != null)
                action(t);
        }

        public static U Maybe<T, U>(this T? t, Func<U> func, U u = default(U)) where T : struct
        {
            return t == null ? u : func();
        }

        public static U Maybe<T, U>(this T t, Func<U> func, U u = default(U)) where T : class
        {
            return t == null ? u : func();
        }

        public static U Maybe<T,U>(this T t, Func<T,U> func, U u = default(U)) where T : class
        {
            return t == null ? u : func(t);
        }
    }
}
