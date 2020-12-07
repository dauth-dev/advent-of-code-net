using System.Linq;
using AdventOfCode.Utils;
using System;
using AdventOfCode.Day_2.Models;

namespace AdventOfCode.Day_03
{
    public class Runner : AbstractRunner
    {

        public Runner() : base(3) { }

        protected override void Process()
        {
            var input = InputLoader.Instance.LoadInputAsBitMatrix(Day);

            int hits = BitArrayHelper.countHitsBySearchPattern(input, 0, 0, 1, 3);
            int hits2 = BitArrayHelper.countHitsBySearchPattern(input, 0, 0, 1, 1);
            int hits3 = BitArrayHelper.countHitsBySearchPattern(input, 0, 0, 1, 5);
            int hits4 = BitArrayHelper.countHitsBySearchPattern(input, 0, 0, 1, 7);
            int hits6 = BitArrayHelper.countHitsBySearchPattern(input, 0, 0, 2, 1);
        }
    }
}
