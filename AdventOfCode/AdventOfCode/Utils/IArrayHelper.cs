using System;
using System.Collections.Generic;

namespace AdventOfCode.Utils
{
    public interface IArrayHelper
    {
        Tuple<T, T> FindTwoItemsWith<T>(IEnumerable<T> numbers, Func<T, T, bool> operatorFunc);

        Tuple<T, T, T> FindThreeItemsWith<T>(IEnumerable<T> numbers, Func<T, T, T, bool> operatorFunc);

        IEnumerable<Tuple<T, T>> Join<T>(IEnumerable<T> first, IEnumerable<T> second);

        IEnumerable<Tuple<T, T, T>> Join<T>(IEnumerable<T> first, IEnumerable<T> second, IEnumerable<T> third);
    }
}
