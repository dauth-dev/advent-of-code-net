using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Day_8;
using AdventOfCode.Utils;

namespace AdventOfCode.Day_08
{
	public class Runner : AbstractRunner
	{
		public Runner() : base(8)
		{
		}

		public IEnumerable<Operation> ParseInput(IEnumerable<string> lines)
		{
			return lines.Select(Operation.CreateFromLine); ;
		}

		protected override void Process()
		{
			//Part1();
			Part2();
		}

		private void Part2()
		{
			var operations = ParseInput(InputLoader.Instance.LoadInputAsEnumerableOfStrings(Day, "input-part2"));

			var accumulator = new Accumulator(operations.ToList());
			var result = accumulator.Process(false);
			Logger.Log($"Second Part: {result}");
		}

		private void Part1()
		{
			var operations = ParseInput(InputLoader.Instance.LoadInputAsEnumerableOfStrings(Day));

			var accumulator = new Accumulator(operations.ToList());
			var firstResult = accumulator.Process();
			Logger.Log($"First Part: {firstResult}");

		}
	}
}