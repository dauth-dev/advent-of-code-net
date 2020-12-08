using System.Collections.Generic;

namespace AdventOfCode.Day_8
{
	public class Accumulator
	{
		private readonly IList<Operation> _operations;

		public Accumulator(IList<Operation> operations)
		{
			_operations = operations;
		}

		public int Process()
		{
			var index = 0;
			var currentValue = 0;
			var operation = _operations[index];
			bool finished;
			do
			{
				finished = operation.Processed;
				if (!finished)
				{
					var next = operation.ProcessOperation(currentValue, index);

					currentValue = next.Item1;
					index = next.Item2;

					if (index >= _operations.Count)
					{
						finished = true;
					}
					else
					{
						operation = _operations[index];
					}

				}

			} while (finished == false);

			return currentValue;
		}
	}
}
