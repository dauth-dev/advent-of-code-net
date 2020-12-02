using System.Linq;
using AdventOfCode.Utils;
using System;
using AdventOfCode.Day_2.Models;

namespace AdventOfCode.Day_02
{
    public class Runner : AbstractRunner
    {

        public Runner() : base(2) { }

        protected override void Process()
        {
            var input = InputLoader.Instance.LoadInputAsEnumerableOfStrings(2020, 2);

            var validInputCounts = input.Select(i => Mapper.MapToPolicy<Policy_Part_1>(i))
                .Where(t => t.Item1.IsValid(t.Item2))
                .Count();


            Logger.Log($"'{ validInputCounts } Inputs are valid for Part 1");


            var validInputCounts_2 = input.Select(i => Mapper.MapToPolicy<Policy_Part_2>(i))
                .Where(t => t.Item1.IsValid(t.Item2))
                .Count();

            Logger.Log($"'{ validInputCounts_2 } Inputs are valid for Part 2");
        }
    }
}
