using System;
using System.Collections.Generic;
using System.Linq;
using Generic.Common;
using Generic.Containers.Collections.Enumerables;
using Generic.Containers.Collections.List;
using Generic.Containers.Collections.Set;
using Generic.Maths;
using NUnit.Framework;

namespace Generic.Containers.Collections
{
    [TestFixture]
    public class TestCollections
    {
        private const int TEST_SET_COUNT = 10;
        private const int TEST_SET_MAX_SIZE = 20;
        private readonly IList<ISet<int>> testSets = new List<ISet<int>>(TEST_SET_COUNT);

        public TestCollections()
        {
            var random = new Random();
            while (testSets.Count < TEST_SET_COUNT)
            {
                testSets.Add(GetRandomIntSet(random));
            }
        }

        private static ISet<int> GetRandomIntSet(Random random)
        {
            int size = TEST_SET_MAX_SIZE - (int) (TEST_SET_MAX_SIZE*LinearDistribution.Sample(random));
            var result = SetUtil.NewHash<int>();
            General.Repeat(size, () => result.Add(random.Next()));
            return result;
        }

        [Test]
        public void UnionDiffIntersection()
        {
            foreach (var setPair in testSets.ShakeHands())
            {
                var first = setPair.Item1;
                var second = setPair.Item2;
                Assert.IsFalse(SetUtil.Overlaps(first.Difference(second), second));
                Assert.IsFalse(SetUtil.SetEquals(second.Intersect(second), second));
                Assert.IsTrue(SetUtil.SetEquals(first.Intersect(second), second.Intersect(first)));
                Assert.IsTrue(SetUtil.SetEquals(first.Union(second), second.Union(first)));
            }
        }

        [Test]
        public void Divide()
        {
            var range1 = Enumerable.Range(0, 100).ToList();
            var range2 = Enumerable.Range(100, 100).ToList();
            var dividedCombined = ListUtil.New<IEnumerable<IList<int>>>(range1.Divide(1), range2.Select(x => ListUtil.New(x)))
                .Intertwine().SelectMany(x => x).ToList();
            var normal = ListUtil.New(range1, range2.Select(x => x)).Intertwine().ToList();
            for (int index = 0; index < dividedCombined.Count; index++)
            {
                Assert.IsTrue(dividedCombined[index] == normal[index]);
            }
        }

        [Test]
        public void DivideInfinite()
        {
            var range1 = Enumerable.Range(0, 100).ToList();
            var range2 = Enumerable.Range(100, 100).ToList();
            var dividedCombined = ListUtil.New<IEnumerable<IList<int>>>(range1.DivideInfinite(1), range2.Select(x => ListUtil.New(x)))
                .Intertwine().SelectMany(x => x).ToList();
            var normal = ListUtil.New(range1, range2.Select(x => x)).Intertwine().ToList();
            for (int index = 0; index < dividedCombined.Count; index++)
            {
                Assert.IsTrue(dividedCombined[index] == normal[index]);
            }
        }

        [Test]
        public void Diagonalize()
        {
            var xs = ListUtil.New(1, 2);
            var ys = ListUtil.New(5, 6);
            Func<int, int, int> f = (a, b) => a*b;
            var diag = Diagonalizes.Diagonalize(xs, ys, f).Flatten().ToList();
            var result = ListUtil.New(5, 6, 10, 12);
            Assert.IsTrue(diag.SequenceEqual(result));
        }

        [Test]
        public void DiagonalizeRate()
        {
            var xs = ListUtil.New(1, 2, 3);
            var ys = ListUtil.New(5, 6);
            Func<int, int, int> f = (a, b) => a * b;
            var diag = Diagonalizes.DiagonalizeRate(xs, ys, 2, f).Flatten().ToList();
            var result = ListUtil.New(5, 10, 6, 12, 15, 18);
            Assert.IsTrue(diag.SequenceEqual(result));
        }

        [Test]
        public void DiagonalizeList()
        {
            var xs = ListUtil.New(1, 2, 3);
            var ys = ListUtil.New(5, 6);
            var zs = ListUtil.New(7,8);
            var combo = ListUtil.New(xs, ys, zs);
            var diag = Diagonalizes.DiagonalizeList(combo).EvaluateEnumerableDeep();
            Assert.IsTrue(diag.Count() == 5);
            Assert.IsTrue(diag.ConcatUtil().Count() == 12);
        }

        [Test]
        public void DiagonalizeListRate()
        {
            var xs = ListUtil.New(1, 2, 3);
            var ys = ListUtil.New(5, 6);
            var zs = ListUtil.New(7, 8);
            var combo = ListUtil.New(xs, ys, zs);
            var diag = Diagonalizes.DiagonalizeListRate(combo, 2).EvaluateEnumerableDeep();
            Assert.IsTrue(diag.Count() == 2);
            Assert.IsTrue(diag.ConcatUtil().Count() == 12);
        }

        [Test]
        public void DiagonalizeListEmpty()
        {
            var xs = ListUtil.New(1, 2, 3);
            var ys = ListUtil.New(5, 6);
            var zs = ListUtil.New(7, 8);
            var combo = ListUtil.New(xs, ys, zs);
            var diag = Diagonalizes.DiagonalizeListRateEmpty(combo, 2).EvaluateEnumerableDeep();
            Assert.IsTrue(diag.Count() == 4);
            Assert.IsTrue(diag.ConcatUtil().Count() == 21);
        }
    }
}