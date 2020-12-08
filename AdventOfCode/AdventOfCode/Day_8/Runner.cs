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
			// Part1();
			Part2();
		}

		private void Part2()
		{
			var operations = ParseInput(InputLoader.Instance.LoadInputAsEnumerableOfStrings(Day));
            var opCount = operations.Count();
            int testedLine = 0;

            bool found = false;
            int result = 0;
			// jmp prüfen
            while (!found && testedLine < opCount)
            {
                List<Operation> opList = operations.ToList();
                if (opList[testedLine].GetType() == typeof(JmpOperation))
                {
                    opList[testedLine] = new NopOperation();
                    var accumulator = new Accumulator(opList);
                    result = accumulator.Process();
                    if (accumulator.lastIndex >= opList.Count)
                    {
                        found = true;
                    }
                    else
                    {
                        // nicht gefunden
                        testedLine++;
                    }
                }
                else
                {
                    testedLine++;
                }

            }

            Logger.Log($"Second Part: {result}");

            testedLine = 0;
            // nop prüfen
            while (!found && testedLine < opCount)
            {
                List<Operation> opList = operations.ToList();
                if (opList[testedLine].GetType() == typeof(NopOperation))
                {
                    opList[testedLine] = new JmpOperation(Convert.ToInt32(((NopOperation) opList[testedLine])._arg));
                    var accumulator = new Accumulator(opList);
                    result = accumulator.Process();
                    if (accumulator.lastIndex >= opList.Count)
                    {
                        found = true;
                    }
                    else
                    {
                        // nicht gefunden
                        testedLine++;
                    }
                }
                else
                {
                    testedLine++;
                }

            }

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