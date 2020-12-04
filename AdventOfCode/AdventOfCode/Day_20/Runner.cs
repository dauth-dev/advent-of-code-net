using AdventOfCode.Core;
using AdventOfCode.Utils;
using Microsoft.Extensions.Logging;

namespace AdventOfCode.Day_20
{
    public class Runner : AbstractRunner
    {
        private readonly IInputLoader inputLoader;

        public Runner(ILogger<AbstractRunner> logger, IInputLoader inputLoader) : base("20", logger)
        {
            this.inputLoader = inputLoader;
        }

        protected override void Process()
        {
            
        }

        
    }
}