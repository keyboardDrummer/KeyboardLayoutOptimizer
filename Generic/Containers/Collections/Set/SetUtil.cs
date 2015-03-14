using System;
using System.Collections.Generic;
using System.Linq;
using Generic.Containers.Collections.Enumerables;

namespace Generic.Containers.Collections.Set
{
    public static class SetUtil
    { 
        public static ISet<T> Intersect<T>(this ISet<T> a, ISet<T> b)
        {
            return new Intersection<T>(a,b);
        }

        public static ISet<T> Difference<T>(this ISet<T> a, ISet<T> b)
        {
            return new Difference<T>(a,b);
        }

        public static ISet<T> Union<T>(this ISet<T> a, ISet<T> b)
        {
            return new Union<T>(a, b);
        }

        public static ISet<T> ToHashSet<T>(this IEnumerable<T> xs)
        {
            return new HashSet<T>(xs);
        }

        public static ISet<TB> SelectDisjunct<TA,TB>(this ISet<TA> xs, Func<TA,TB> to, Func<TB,TA> from)
        {
            return new SelectDisjunct<TA, TB>(xs, to, from);
        }

        public static bool Overlaps<T>(this ISet<T> first, ISet<T> second)
        {
            return !first.Intersect(second).IsEmpty();
        }

        public static bool SetEquals<T>(this ICollection<T> a, ISet<T> b)
        {
            return a.All(b.Contains) && a.Count == b.Count;
        }

        public static ISet<T> NewHash<T>(IEnumerable<T> values)
        {
            return new HashSet<T>(values);
        }

        public static ISet<T> NewHash<T>(params T[] values)
        {
            return new HashSet<T>(values);
        }

        public static int SetHash<T>(ISet<T> set)
        {
            return set.Count;
        }
    }
}
