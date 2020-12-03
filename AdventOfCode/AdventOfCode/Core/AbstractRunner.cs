using AdventOfCode.Utils;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace AdventOfCode.Core
{
    public abstract class AbstractRunner : IRunner
    {
        protected ILogger<AbstractRunner> Logger { get; }

        public AbstractRunner(int day, ILogger<AbstractRunner> logger)
        {
            Day = day;
            this.Logger = logger;
        }

        public int Day { get; }

        public void Run()
        {
            var watch = Stopwatch.StartNew();
            Logger.LogInformation($"===========================");
            Logger.LogInformation($"processing Day {Day }...");
            Logger.LogInformation($"===========================");

            this.Process();

            watch.Stop();

            Logger.LogInformation($"===========================");
            Logger.LogInformation($"Elapsed Seconds: {watch.Elapsed.TotalSeconds}");
            Logger.LogInformation($"===========================");

        }

        protected abstract void Process();
    }
}
