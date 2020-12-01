using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCode.Utils;

namespace AdventOfCode
{
    class Run
    {
        static void Main(string[] args)
        {
            var watch = Stopwatch.StartNew();
            var day1 = new Run();
            var orderdInput = day1.LoadInput().Result;

            var first = day1.FirstPart(orderdInput);
            Logger.Log($"The Result for the first part is: {first}!");

            var second = day1.SecondPart(orderdInput);
            Logger.Log($"The Result for the second part is: {second}!");

            Logger.Log($"Elapsed seconds: {watch.Elapsed.TotalSeconds}");
        }

        private async Task<IEnumerable<long>> LoadInput(string inputFile = "input")
        {
            var input = await InputLoader.Instance.LoadInputAsEnumerableOfNumbers(2020, 1, inputFile);
            Logger.Log($"{input.Count()} Input numbers found in file '{inputFile}'!");

            return input.OrderBy(i => i);
        }

        private long FirstPart(IEnumerable<long> input)
        {
            var r = ArrayHelper.Instance.FindTwoItemsWith(input, (i, j) => i + j == 2020);
            return r.Item1 * r.Item2;
        }

        private long SecondPart(IEnumerable<long> input)
        {
            var r = ArrayHelper.Instance.FindThreeItemsWith(input, (i, j, k) => i + j + k == 2020);

            return r.Item1 * r.Item2 * r.Item3;
        }
    }
}
