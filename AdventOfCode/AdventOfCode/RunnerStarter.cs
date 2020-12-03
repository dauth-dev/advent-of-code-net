using AdventOfCode.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class RunnerStarter
    {
        private readonly IEnumerable<IRunner> runners;

        public RunnerStarter(IEnumerable<IRunner> runners)
        {
            this.runners = runners;
        }

        public void StartLatestRunner() => runners.OrderBy(r => r.Day).Last().Run();

        public void StartRunnerForDay(int day) => GetRunnerForDay(day).Run();

        public IRunner GetRunnerForDay(int day) => runners.First(r => r.Day == day);
    }
}
