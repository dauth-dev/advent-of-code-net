using System;
using System.Collections.Generic;

namespace AdventOfCode.Utils
{
    public interface IArrayHelper
    {
        long[] FindTwoNumbers(IEnumerable<long> numbers, Func<long, long, bool> operatorFunc);
        long[] FindThreeNumbers(IEnumerable<long> numbers, Func<long, long, long, bool> operatorFunc);
    }

    public class ArrayHelper : IArrayHelper
    {
        public long[] FindThreeNumbers(IEnumerable<long> numbers, Func<long, long, long, bool> operatorFunc)
        {
            throw new NotImplementedException();
        }

        public long[] FindTwoNumbers(IEnumerable<long> numbers, Func<long, long, bool> operatorFunc)
        {
            throw new NotImplementedException();
        }
    }
}
