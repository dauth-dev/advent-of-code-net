using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class MainRunner
    {
        private IList<IRunner> runners = new List<IRunner>() {
            new Day_01.Runner(),
            new Day_02.Runner(),
            new Day_03.Runner()
        };

        public static void Main()
        {
            var runner = new MainRunner();
            runner.runners.Last().Run();
        }
    }
}
