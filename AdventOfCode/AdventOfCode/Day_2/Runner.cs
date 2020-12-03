using AdventOfCode.Core;
using AdventOfCode.Day_2.Models;
using AdventOfCode.Utils;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace AdventOfCode.Day_02
{
    public class Runner : AbstractRunner
    {
        private readonly IInputLoader inputLoader;

        public Runner(IInputLoader inputLoader, ILogger<Runner> logger) : base(2, logger)
        {
            this.inputLoader = inputLoader;
        }


        protected override void Process()
        {
            var input = inputLoader.LoadInputAsEnumerableOfStrings(Day);

            var validInputCounts = input.Select(i => Mapper.MapToPolicy<Policy_Part_1>(i))
                .Where(t => t.Item1.IsValid(t.Item2))
                .Count();


            Logger.LogInformation($"'{ validInputCounts } Inputs are valid for Part 1");


            var validInputCounts_2 = input.Select(i => Mapper.MapToPolicy<Policy_Part_2>(i))
                .Where(t => t.Item1.IsValid(t.Item2))
                .Count();

            Logger.LogInformation($"'{ validInputCounts_2 } Inputs are valid for Part 2");
        }
    }
}
