using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AdventOfCode.Utils;

namespace AdventOfCode.Day_10
{
	public class Runner : AbstractRunner
	{
		public Runner() : base(10)
		{
		}

		protected override void Process()
		{
			Part1();
			Part2();
		}

		private void Part1()
		{
			var input = InputLoader.Instance.LoadInputAsEnumerableOfNumbers(Day).ToList();
			input.Sort();
			int jumps_1Jolt = 0;
			int jumps_2Jolt = 0;
			int jumps_3Jolt = 0;
			long currentJolt = 0;
			long deviceJolt = input.Last() + 3;
			for (int i = 0; i < input.Count; i++)
			{
				long adpaterJolt = input[i];
				var delta = adpaterJolt - currentJolt;
				switch (delta)
				{
					case 1:
						jumps_1Jolt++;
						break;
					case 2:
						jumps_2Jolt++;
						break;
					case 3:
						jumps_3Jolt++;
						break;
					default:
						throw new System.ArgumentOutOfRangeException();
				}
				currentJolt += delta;
			}
			jumps_3Jolt++; // letzter Adapter bis Device
			Logger.Log($"First Part 1-Jolts: {jumps_1Jolt}");
			Logger.Log($"First Part 3-Jolts: {jumps_3Jolt}");
			Logger.Log($"First Part: {jumps_1Jolt * jumps_3Jolt}");
		}

		private void Part2()
		{
			var input = InputLoader.Instance.LoadInputAsEnumerableOfNumbers(Day).ToList();

			/*
			input.Clear();
			input.Add(16);
			input.Add(10);
			input.Add(15);
			input.Add(5);
			input.Add(1);
			input.Add(11);
			input.Add(7);
			input.Add(19);
			input.Add(6);
			input.Add(12);
			input.Add(4);
			*/

			input.Add(0);
			input.Sort();
			input.Add(input.Last() + 3); // letzter Adapter

			var test = new Dictionary<int, long>();
			test[input.Count() - 1] = 1; // vorletzter

			for (int i = input.Count() - 2; i >= 0; i--) // rückwärts alle möglichen Adapter
			{
				long currentCount = 0;
				for (var connected = i + 1; connected < input.Count() && input[connected] - input[i] <= 3; connected++)
				{
					currentCount += test[connected];
				}
				test[i] = currentCount;
			}

			Logger.Log($"Second Part: {test[0].ToString()}");
		}
	}
}
