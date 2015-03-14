using System;
using System.Collections.Generic;
using System.Linq;
using Generic.Containers.Collections.Set;
using Generic.Containers.Maybes;
using JetBrains.Annotations;

namespace Generic.Containers.Collections.Dictionaries
{
    public static class DictionaryUtil
    {
        public static IDictionary<K, V> TuplesToDictionary<K, V>(this IEnumerable<Tuple<K, V>> enumerable)
        {
            return enumerable.ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2);
        }

        public static IDictionary<K, IList<V>> ToMultiDictionary<T, K, V>(this IEnumerable<T> enumerable,
                                                                          Func<T, K> keySelector,
                                                                          Func<T, V> valueSelector)
        {
            var dictionary = new Dictionary<K, IList<V>>();
            foreach (T t in enumerable)
            {
                K key = keySelector(t);
                V value = valueSelector(t);
                if (!dictionary.ContainsKey(key))
                    dictionary[key] = new List<V>();
                dictionary[key].Add(value);
            }
            return dictionary;
        }

        public static void InsertOrUpdate<K, V>(this IDictionary<K, V> map, K key, [InstantHandle] Action<V> update,
                                                [InstantHandle] Func<V> insert = null)
        {
            insert = insert ?? (() => default(V));
            if (map.ContainsKey(key))
                update(map[key]);
            else
                map[key] = insert();
        }

        public static void InsertOrUpdate<K, V>(this IDictionary<K, V> map, K key, [InstantHandle] Func<V, V> update,
                                                [InstantHandle] Func<V> insert = null)
        {
            insert = insert ?? (() => default(V));
            map[key] = map.Get(key).Eval(insert(), update);
        }

        public static Maybe<V> Get<K, V>(this IDictionary<K, V> map, K key)
        {
            V value;
            bool success = map.TryGetValue(key, out value);
            return success ? MaybeUtil.Just(value) : new Nothing<V>();
        }

        public static V GetOrCreate<K, V>(this IDictionary<K, V> map, K key, Func<V> create)
        {
            return map.Get(key).Eval(() =>
                {
                    V value = create();
                    map[key] = value;
                    return value;
                }, x => x);
        }

        public static V GetOrDefault<K, V>(this IDictionary<K, V> map, K key, V defaultt = default(V))
        {
            return map.Get(key).Default(defaultt);
        }

        public static IDictionary<K, V> New<K, V>()
        {
            return new Dictionary<K, V>();
        }

        public static bool DictionaryEquals<K,V>(this IDictionary<K,V> first, IDictionary<K,V> second)
        {
            var keysEqual = first.Keys.SetEquals(second.Keys.ToHashSet());
            return keysEqual && first.Keys.All(key => first[key].Equals(second[key]));
        }
    }
}