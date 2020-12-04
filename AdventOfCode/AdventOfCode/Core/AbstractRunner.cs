using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace AdventOfCode.Core
{
    public abstract class AbstractRunner : IRunner
    {
        protected ILogger<AbstractRunner> Logger { get; }

        protected AbstractRunner(string day, ILogger<AbstractRunner> logger)
        {
            Day = day;
            IsActive = false;
            this.Logger = logger;
        }

        public string Day { get; }
        public bool IsActive { get; protected set; }

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
