using System;
using System.Collections.Generic;
using AdventOfCode.Utils;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests.Utils
{
    [TestClass]
    public class ArrayHelperTest
    {
        private IArrayHelper Sut { get; }


        public ArrayHelperTest()
        {
            Sut = new ArrayHelper();
        }

        [TestMethod]
        public void ArrayCanBeJoined()
        {
            var first = new List<int>() { 1, 2, 3 };

            var expected = new List<Tuple<int, int>>() {
                new Tuple<int, int>(1, 1),
                new Tuple<int, int>(1, 2),
                new Tuple<int, int>(1, 3),
                new Tuple<int, int>(2, 1),
                new Tuple<int, int>(2, 2),
                new Tuple<int, int>(2, 3),
                new Tuple<int, int>(3, 1),
                new Tuple<int, int>(3, 2),
                new Tuple<int, int>(3, 3),
            };

            var actual = Sut.Join(first, first);
            actual.Should().BeEquivalentTo(expected, o => o.WithoutStrictOrdering());
        }

        [TestMethod]
        public void Three_Arrays_can_be_joined()
        {
            var first = new List<int>() { 1, 2 };

            var expected = new List<Tuple<int, int, int>>() {
                Tuple.Create(1, 1, 1),
                Tuple.Create(1, 1, 2),
                Tuple.Create(1, 2, 1),
                Tuple.Create(1, 2, 2),
                Tuple.Create(2, 1, 1),
                Tuple.Create(2, 1, 2),
                Tuple.Create(2, 2, 1),
                Tuple.Create(2, 2, 2),
            };

            var actual = Sut.Join(first, first, first);
            actual.Should().BeEquivalentTo(expected, o => o.WithoutStrictOrdering());

        }

        [TestMethod]
        public void An_Error_is_thrown_if_size_does_not_match()
        {
            var withItems = new List<int>() { 1, 2, 3 };
            var emptyList = new List<int>();

            var actions = new List<Action>() {
                () => Sut.Join(withItems, emptyList),
                () => Sut.Join(emptyList, withItems),
                () => Sut.Join(withItems, withItems, emptyList),
                () => Sut.Join(withItems, emptyList, withItems),
                () => Sut.Join(emptyList, withItems, withItems)
            };

            actions.ForEach(e => e.Should().Throw<InvalidOperationException>());
        }


        [TestMethod]
        public void Found_Items_returns_the_correct_tuple() {
            var items = new List<int>() { 2, 3, 5 };
            var found = Sut.FindTwoItemsWith(items, (i, j) => i + j == 7);

            found.Should().Be(Tuple.Create(2, 5));
        }
    }
}
