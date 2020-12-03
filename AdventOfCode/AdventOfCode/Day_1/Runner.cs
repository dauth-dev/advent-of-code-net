using AdventOfCode.Core;
using AdventOfCode.Utils;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day_01
{
    public class Runner : AbstractRunner
    {
        private readonly IInputLoader inputLoader;
        private readonly IArrayHelper arrayHelper;

        public Runner(IInputLoader inputLoader, IArrayHelper arrayHelper, ILogger<Runner> logger) : base(1, logger)
        {
            this.inputLoader = inputLoader;
            this.arrayHelper = arrayHelper;
        }

        private IEnumerable<long> LoadInput()
        {
            var input = inputLoader.LoadInputAsEnumerableOfNumbers(Day);
            Logger.LogInformation($"{input.Count()} Input numbers found!");

            return input.OrderBy(i => i);
        }

        private long FirstPart(IEnumerable<long> input)
        {
            var r = arrayHelper.FindTwoItemsWith(input, (i, j) => i + j == 2020);
            return r.Item1 * r.Item2;
        }

        private long SecondPart(IEnumerable<long> input)
        {
            var r = arrayHelper.FindThreeItemsWith(input, (i, j, k) => i + j + k == 2020);

            return r.Item1 * r.Item2 * r.Item3;
        }

        protected override void Process()
        {
            var orderdInput = LoadInput();

            var first = FirstPart(orderdInput);
            Logger.LogInformation($"The Result for the first part is: {first}!");

            var second = SecondPart(orderdInput);
            Logger.LogInformation($"The Result for the second part is: {second}!");
        }
    }
}
