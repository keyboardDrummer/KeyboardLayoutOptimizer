using System;
using System.Collections.Generic;
using System.Linq;
using Generic.Containers.Collections.Enumerables;
using Generic.Containers.Collections.Enumerators;
using Generic.Containers.Collections.List;
using KeyboardEPCS.Logic.Inputs;

namespace KeyboardEPCS.Logic
{

    public class Mutate
    {
        readonly Action<string> printLogMessage;

        public Mutate(Action<string> printLogMessage)
        {
            this.printLogMessage = printLogMessage;
        }

        public IEnumerable<KeyboardLayout> GetMutations(Random random, KeyboardLayout keyboardLayout)
        {
            var result = ListUtil.FromTo(2, keyboardLayout.TotalPositions).SelectMany(cycleLength => GetMutations(random, keyboardLayout, cycleLength));
            return new NewEnumerable(result,printLogMessage);
        }

        class NewEnumerable : DefaultEnumerable<KeyboardLayout>
        {
            readonly IEnumerable<KeyboardLayout> inner;
            readonly Action<string> printLogMesssage;

            public NewEnumerable(IEnumerable<KeyboardLayout> inner, Action<string> printLogMesssage)
            {
                this.inner = inner;
                this.printLogMesssage = printLogMesssage;
            }

            public override IEnumerator<KeyboardLayout> GetEnumerator()
            {
                return new NewEnumerator(inner.GetEnumerator(),printLogMesssage);
            }
        }

        class NewEnumerator : DefaultEnumerator<KeyboardLayout>
        {
            readonly IEnumerator<KeyboardLayout> inner;
            readonly Action<string> printLogMesssage;

            public NewEnumerator(IEnumerator<KeyboardLayout> inner, Action<string> printLogMesssage)
            {
                this.inner = inner;
                this.printLogMesssage = printLogMesssage;
            }

            public override KeyboardLayout Current
            {
                get { return inner.Current; }
            }

            public override bool MoveNext()
            {
                var result = inner.MoveNext();
                if (!result)
                    printLogMesssage("Finished.");
                return result;
            }
        }

        IEnumerable<KeyboardLayout> GetMutations(Random random, KeyboardLayout keyboardLayout, int cycleLength)
        {
            printLogMessage(string.Format("Getting mutation with cycle length {0}", cycleLength));
            var cyclePositionIndices = GetCyclePositionIndices(random, cycleLength,keyboardLayout);
            var perms = GetPermutations(cyclePositionIndices.Length); //.Where(IsPermMovingAll); //.ToList(); XXX for some reason removing not-all-movers makes the mutato not complete.
            var maps = perms.Select(perm => perm.Select((from, to) => Tuple.Create(cyclePositionIndices[from], cyclePositionIndices[to])));
            return maps.Select(map =>
            {
                var clone = keyboardLayout.Clone();
                foreach (var tuple in map)
                    clone[tuple.Item1] = keyboardLayout[tuple.Item2];
                clone.CheckConsistency();
                return clone;
            });
        }

        static bool IsPermMovingAll(IEnumerable<int> perm)
        {
            return perm.WithIndex().All(tup => tup.Item1 != tup.Item2);
        }
        
        //XXX build a solution that doesn't copy lists all the time. Use linked lists.
        static IEnumerable<IList<int>> GetPermutations(int length)
        {
            if (length == 0)
                return ListUtil.New(ListUtil.New<int>());
            var recursiveSolutions = GetPermutations(length - 1);
            return Enumerable.Range(0, length).SelectMany(insertPosition => recursiveSolutions.Select(recursiveSolution =>
            {
                var copy = recursiveSolution.ToList();
                copy.Insert(insertPosition, length - 1);
                return copy;
            }));
        }

        //XXX allows not moving an item, which it optimally should not.
        static IEnumerable<IList<int>> GetPermutations2(int from, int to)
        {
            if (from == to)
                return ListUtil.Singleton(ListUtil.Singleton(from));
            var recSolutions = GetPermutations2(from + 1, to);
            return ListUtil.FromTo(0, to-from).SelectMany(position => recSolutions.Select(recSolution =>
            {
                var copy = recSolution.ToList();
                copy.Insert(position, from);
                return copy;
            }));
        }

        static int[] GetCyclePositionIndices(Random random, int cycleLength, KeyboardLayout keyboardLayout)
        {
            var positionIndices = ListUtil.FromTo(0, keyboardLayout.TotalPositions - 1).ToList();
            var result = new int[cycleLength];
            foreach (int cycleIndex in result.Indexes())
            {
                var index = random.Next(0, positionIndices.Count - 1);
                result[cycleIndex] = positionIndices[index];
                positionIndices.RemoveAt(index);
            }
            return result;
        }
    }
}