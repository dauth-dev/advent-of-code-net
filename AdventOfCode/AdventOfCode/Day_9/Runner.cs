using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Day_8;
using AdventOfCode.Utils;

namespace AdventOfCode.Day_09
{
	public class Runner : AbstractRunner
	{
		public Runner() : base(9)
		{
		}

        private long _firstinvalidnumer = 0;

		protected override void Process()
		{
			Part1();
			Part2();
		}

        private void Part1()
        {
            var input = InputLoader.Instance.LoadInputAsEnumerableOfNumbers(Day).ToList();
            for (int i = 25; i < input.Count; i++)
            {
                var inputItems = input.Skip(i  - 25).Take(25).ToList();
                if (!checkIsSum(inputItems, input[i]))
                {
                    _firstinvalidnumer = input[i];
                    Logger.Log($"First Part: {input[i]}");
                }
            }
        }

		private void Part2()
		{
            findContiguousSet();
        }

        private void findContiguousSet()
        {
            var input = InputLoader.Instance.LoadInputAsEnumerableOfNumbers(Day).ToList();
            int startIndex = 0;
            int endIndex = 0;
            bool found = false;
            for (int i = 0; i < input.Count - 1; i++)
            {
                if (found)
                {
                    break;
                }
                long tempSum = input[i];
                startIndex = i;
                for (int j = i + 1; j < input.Count; j++)
                {
                    tempSum += input[j];
                    if (tempSum > _firstinvalidnumer)
                    {
                        break;
                    }
                    else if (tempSum == _firstinvalidnumer)
                    {
                        endIndex = j;
                        found = true;
                    }
                }
            }

            var contiguousSet = input.Skip(startIndex).Take(endIndex - startIndex + 1).ToList();
            contiguousSet.Sort();
            Logger.Log($"Second Part lowest: {contiguousSet[0]}");
            Logger.Log($"Second Part highest: {contiguousSet[contiguousSet.Count - 1]}");
            Logger.Log($"Second Part result (sum): {contiguousSet[contiguousSet.Count - 1] + contiguousSet[0]}");
        }

        private bool checkIsSum(List<long> baseNumbers, long checkNumber)
        {
            for (int i = 0; i < baseNumbers.Count - 1; i++)
            {
                for (int j = 1; j < baseNumbers.Count; j++)
                {
                    if (baseNumbers[i] == baseNumbers[j])
                    {
                        // keine 2 gleichen
                        continue;
                    }

                    if ((baseNumbers[i] + baseNumbers[j]) == checkNumber)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

	}
}