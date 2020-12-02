using AdventOfCode.Utils;
using System.Diagnostics;

namespace AdventOfCode.Core
{
    public abstract class AbstractRunner : IRunner
    {

        public AbstractRunner(int day)
        {
            Day = day;
        }

        public int Day { get; }

        public void Run()
        {
            var watch = Stopwatch.StartNew();
            Logger.Log($"===========================");
            Logger.Log($"processing Day {Day }...");
            Logger.Log($"===========================");

            this.Process();

            watch.Stop();

            Logger.Log($"===========================");
            Logger.Log($"Elapsed Seconds: {watch.Elapsed.TotalSeconds}");
            Logger.Log($"===========================");

        }

        protected abstract void Process();
    }
}
