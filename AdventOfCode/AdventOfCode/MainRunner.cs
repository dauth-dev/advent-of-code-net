using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class MainRunner
    {
        private IList<IRunner> runners = new List<IRunner>() {
            new Day1.Runner(),
            new Day_2.Runner()
        };

        public static void Main()
        {
            var runner = new MainRunner();
            runner.runners.Last().Run();
        }
    }
}
