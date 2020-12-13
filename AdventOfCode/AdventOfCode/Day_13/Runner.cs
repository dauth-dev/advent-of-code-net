using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utils;

namespace AdventOfCode.Day_13
{
	public class Runner : AbstractRunner
	{
		public Runner() : base(13)
		{
		}

		protected override void Process()
		{
			Part1();
			Part2();
		}

        private void Part1()
        {
			var input = InputLoader.Instance.LoadInputAsEnumerableOfStrings(Day).ToArray();
			var myNumber = Convert.ToInt32(input[0]);
			var busLinesTemp = input[1].Split(new char[] { ',' });
			List<string> busLines = new List<string>();
			Dictionary<int, int> delays = new Dictionary<int, int>();
			foreach(var activeLine in busLinesTemp)
            {
				if (activeLine == "x")
				{
					continue;
				}
				busLines.Add(activeLine);
            }
			foreach (var activeLine in busLines)
            {
				int tempVal = Convert.ToInt32(activeLine);
				delays.Add(tempVal - myNumber % tempVal, tempVal);
            }
			int minDelay = int.MaxValue;
			foreach(var lineDelay in delays.Keys)
            {
				if (lineDelay < minDelay)
                {
					minDelay = lineDelay;
                }
            }
			Logger.Log($"First Part: {minDelay * delays[minDelay]}");
		}

		private void Part2()
		{
			var input = InputLoader.Instance.LoadInputAsEnumerableOfStrings(Day).ToArray();
			var times = input[1].Split(',');

			var offset = long.Parse(times[0]);
			var increment = offset;
			for (int i = 1; i < times.Length; i++)
			{
				if (times[i] == "x") continue;

				var curTime = long.Parse(times[i]);
				var modulo = curTime - (i % curTime);
				while (offset % curTime != modulo)
					offset += increment;
				increment = lcm(increment, curTime);
			}
			Logger.Log($"Second Part: {offset}");
		}

		public static long gcd(long a, long b)
		{
			while (b != 0) b = a % (a = b);
			return a;
		}

		public static long lcm(long a, long b) =>
			a * b / gcd(a, b);
	}
}