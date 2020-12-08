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
			var operations = ParseInput(InputLoader.Instance.LoadInputAsEnumerableOfStrings(Day));

			var accumulator = new Accumulator(operations.ToList());
			var firstResult = accumulator.Process();
			Logger.Log($"First Part: {firstResult}");
		}


	}
}