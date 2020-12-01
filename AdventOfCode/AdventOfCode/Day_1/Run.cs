using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCode.Utils;

namespace AdventOfCode
{
	class Run
    {
        static void Main(string[] args)
        {
            var day1 = new Run();
            var orderdInput = day1.LoadInput().Result;
            var result = day1.Process(orderdInput);

            Logger.Log($"The Result is: {result}!");
        }

        private async Task<IEnumerable<long>> LoadInput()
        {
	        var input = await InputLoader.Instance.LoadInputAsEnumerableOfNumbers(2020, 1);
            Logger.Log($"{input.Count()} Input numbers found!");

            return input.OrderBy(i => i);
        }

        private long Process(IEnumerable<long> input)
        {
            // todo: ;-)

            return 42;
        }
    }
}
