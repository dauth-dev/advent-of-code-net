using System.Linq;
using AdventOfCode.Utils;
using System;
using AdventOfCode.Day_2.Models;

namespace AdventOfCode.Day_2
{
    public class Runner : AbstractRunner
    {

        public Runner() : base(2) { }

        protected override void Process()
        {
            var input = InputLoader.Instance.LoadInputAsEnumerableOfStrings(2020, 2);

            var validInputCounts = input.Select(i => Mapper.MapToPolicy(i))
                .Where(t => t.Item1.IsValid(t.Item2))
                .Count();


            Logger.Log($"'{ validInputCounts } Inputs are valid");
        }
    }
}
