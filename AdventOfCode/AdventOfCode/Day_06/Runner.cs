using AdventOfCode.Core;
using AdventOfCode.Utils;
using Microsoft.Extensions.Logging;

namespace AdventOfCode.Day_06
{
    public class Runner : AbstractRunner
    {
        private readonly IInputLoader inputLoader;

        public Runner(ILogger<AbstractRunner> logger, IInputLoader inputLoader) : base("06", logger)
        {
            this.inputLoader = inputLoader;
        }

        protected override void Process()
        {

        }


    }
}