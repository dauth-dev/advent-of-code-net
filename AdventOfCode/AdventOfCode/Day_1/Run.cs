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
            long result = 0;
            var array = input.ToArray();
            var arrayHelper = new ArrayHelper();

            var n = arrayHelper.FindTwoNumbers(array, (i, j) => (i + j) == 2020);



            for (int i = 0; i < array.Length-1; i++)
            {
                for (int j = array.Length-1; j > 0; j--) {
                    
                    for (int k = 0; k < array.Length -1; k++) {
                        var first = array[i];
                        var second = array[j];
                        var third = array[k];

                        if (first == second || second == third || first == third) continue;

                        var sum = first + second + third;
                        if (sum == 2020)
                        {
                            Logger.Log($"numbers found: {first} + {second} + {third} == 2020");
                            result = first * second * third;
                        }
                    }
                    
                }
                
            }

            return result;
        }
    }
}
