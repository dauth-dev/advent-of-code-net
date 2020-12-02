using AdventOfCode.Utils;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Day_01
{
    public class Runner : AbstractRunner
    {
        public Runner() : base(1) { }

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

        protected override void Process()
        {
            var watch = Stopwatch.StartNew();
            var orderdInput = LoadInput().Result;

            var first = FirstPart(orderdInput);
            Logger.Log($"The Result for the first part is: {first}!");

            var second = SecondPart(orderdInput);
            Logger.Log($"The Result for the second part is: {second}!");

            Logger.Log($"Elapsed seconds: {watch.Elapsed.TotalSeconds}");
        }
    }
}
