using AdventOfCode.Core;
using AdventOfCode.Utils;
using Microsoft.Extensions.Logging;

namespace AdventOfCode.Day_03
{
    public class Runner : AbstractRunner
    {
        private readonly IInputLoader inputLoader;

        public Runner(IInputLoader inputLoader, ILogger<Runner> logger) : base(3, logger)
        {
            this.inputLoader = inputLoader;
        }

        protected override void Process()
        {
            var input = inputLoader.LoadInputAsBitMatrix(3);

            int hits = BitArrayHelper.countHitsBySearchPattern(input, 0, 0, 1, 3);
            int hits2 = BitArrayHelper.countHitsBySearchPattern(input, 0, 0, 1, 1);
            int hits3 = BitArrayHelper.countHitsBySearchPattern(input, 0, 0, 1, 5);
            int hits4 = BitArrayHelper.countHitsBySearchPattern(input, 0, 0, 1, 7);
            int hits6 = BitArrayHelper.countHitsBySearchPattern(input, 0, 0, 2, 1);
        }
    }
}
