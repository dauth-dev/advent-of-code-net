using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    internal class MainRunner
    {
        private IList<IRunner> runners = new List<IRunner>() {
            new Day_01.Runner(),
            new Day_02.Runner(),
            new Day_03.Runner(),
             new Day_04.Runner(),
             new Day_05.Runner(),
             new Day_06.Runner(),
             new Day_07.Runner()
        };

        public static void Main()
        {
            var runner = new MainRunner();
            runner.runners.Last().Run();
        }
    }
}