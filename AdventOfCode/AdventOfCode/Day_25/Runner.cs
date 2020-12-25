using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utils;

namespace AdventOfCode.Day_25
{
	public class Runner : AbstractRunner
	{
		public Runner() : base(25)
		{
		}

		protected override void Process()
		{
			Part1();
			Part2();
		}

        private void Part1()
        {
			var input = InputLoader.Instance.LoadInputAsEnumerableOfStrings(Day).ToList();
			var subject = Convert.ToInt64(input[0]);
			var pubKey = Convert.ToInt64(input[1]);

			long counter = 1;
			long test = 1;
			bool found = false;

			// card specific loop size
			while (!found)
            {
				test = (test * 7) % 20201227;
				if (test == pubKey)
                {
                    break;
                }
				counter++;
            }

			// transform
			long result = 1;
			for (int i = 1; i <= counter; i++)
			{
				result = result * subject % 20201227;
			}

			Logger.Log($"First Part: {result}");
		}

		private void Part2()
		{
			Logger.Log($"Second Part: Party :)");
		}
    }
}