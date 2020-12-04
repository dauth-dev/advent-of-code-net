using AdventOfCode.Core;
using AdventOfCode.Utils;
using Microsoft.Extensions.Logging;

namespace AdventOfCode.Day_24
{
    public class Runner : AbstractRunner
    {
        private readonly IInputLoader inputLoader;

        public Runner(ILogger<AbstractRunner> logger, IInputLoader inputLoader) : base("24", logger)
        {
            this.inputLoader = inputLoader;
        }

        protected override void Process()
        {

        }


    }
}