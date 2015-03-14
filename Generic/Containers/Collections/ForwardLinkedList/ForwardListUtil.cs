using System.Collections.Generic;

namespace Generic.Containers.Collections.ForwardLinkedList
{
    public static class ForwardListUtil
    {

        public static ForwardList<T> ToForwardList<T>(this IEnumerable<T> enumerable)
        {
            return FromEnumerator(enumerable.GetEnumerator());
        }

        static ForwardList<T> FromEnumerator<T>(IEnumerator<T> enumerator)
        {
            if (enumerator.MoveNext())
                return new Cons<T>(enumerator.Current, () => FromEnumerator(enumerator));
            return new Empty<T>();
        }

        public static ForwardList<T> New<T>(params T[] items)
        {
            return items.ToForwardList();
        }

        public static ForwardList<int> From(int from)
        {
            return new Cons<int>(from, () => From(from + 1));
        }

        public static ForwardList<int> FromTo(int from, int to)
        {
            return new Cons<int>(from, () => from == to ? new Empty<int>() : FromTo(from + 1, to));
        }
    }
}
