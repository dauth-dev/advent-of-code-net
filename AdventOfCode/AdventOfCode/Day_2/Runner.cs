using System.Linq;
using AdventOfCode.Day_2.Models;
using AdventOfCode.Utils;

namespace AdventOfCode.Day_02
{
    public class Runner : AbstractRunner
    {

        public Runner() : base(2) { }

        protected override void Process()
        {
            var input = InputLoader.Instance.LoadInputAsEnumerableOfStrings(Day);

            var validInputCounts = input
                .Select(Mapper.MapToPolicy<Policy_Part_1>)
                .Count(t => t.Item1.IsValid(t.Item2));


            Logger.Log($"'{ validInputCounts } Inputs are valid for Part 1");


            var validInputCounts_2 = input
                .Select(Mapper.MapToPolicy<Policy_Part_2>)
                .Count(t => t.Item1.IsValid(t.Item2));

            Logger.Log($"'{ validInputCounts_2 } Inputs are valid for Part 2");
        }
    }
}
