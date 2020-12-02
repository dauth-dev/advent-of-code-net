using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode.Utils
{

    public class ArrayHelper : IArrayHelper
    {
        public static IArrayHelper Instance => new ArrayHelper();

        public Tuple<T, T, T> FindThreeItemsWith<T>(IEnumerable<T> numbers, Func<T, T, T, bool> operatorFunc)
        {
            var foundTuple = Join(numbers, numbers, numbers)
                .Where(t => !t.Item1.Equals(t.Item2) && !t.Item1.Equals(t.Item3))
                .FirstOrDefault(t => operatorFunc(t.Item1, t.Item2, t.Item3));

            if (foundTuple == null)
            {
                throw new Exception("No Items where found!");
            }

            return foundTuple;
        }

        public Tuple<T, T> FindTwoItemsWith<T>(IEnumerable<T> numbers, Func<T, T, bool> operatorFunc)
        {
            var foundTuple = Join(numbers, numbers)
                .Where(t => !t.Item1.Equals(t.Item2))
                .FirstOrDefault(t => operatorFunc(t.Item1, t.Item2));

            if (foundTuple == null)
            {
                throw new Exception("No Items where found!");
            }

            return foundTuple;
        }

        public IEnumerable<Tuple<T, T>> Join<T>(IEnumerable<T> first, IEnumerable<T> second)
        {
            if (first.Count() != second.Count())
            {
                throw new InvalidOperationException("Cannot join two arrays with differnt item size");
            }

            return first.SelectMany(i => second, (i, j) => Tuple.Create(i, j));
        }

        public IEnumerable<Tuple<T, T, T>> Join<T>(IEnumerable<T> first, IEnumerable<T> second, IEnumerable<T> third)
        {
            var sizesMatch = first.Count() == second.Count() && first.Count() == third.Count() && second.Count() == third.Count();
            if (sizesMatch == false)
            {
                throw new InvalidOperationException("Cannot join three arrays with differnt item size");
            }

            var joined = this.Join(first, second);
            return joined.SelectMany(i => third, (j, t) => Tuple.Create(j.Item1, j.Item2, t));
        }
    }
}
