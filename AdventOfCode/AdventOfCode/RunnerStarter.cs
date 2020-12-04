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

        private IEnumerable<IRunner> GetActiveRunners() => runners.Where(r => r.IsActive);

        public void StartLatestRunner() => GetActiveRunners().OrderBy(r => r.Day).Last().Run();

        public void StartRunnerForDay(int day) => GetRunnerForDay(day).Run();

        public IRunner GetRunnerForDay(int day) => runners.First(r => r.Day == day);
    }
}
