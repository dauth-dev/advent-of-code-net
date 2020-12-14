using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Utils;

namespace AdventOfCode.Day_14
{
	public class Runner : AbstractRunner
	{
		public Runner() : base(14)
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
			string bitmask = string.Empty;

			/*
			input.Clear();
			input.Add("mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X");
			input.Add("mem[8] = 11");
			input.Add("mem[7] = 101");
			input.Add("mem[8] = 0");
			*/

			Dictionary<long, string> memValues = new Dictionary<long, string>();
			foreach(var line in input)
            {
				var temp = line.Split(new char[] { '=' });
				if (line.StartsWith("mask"))
				{
					// neue Maske
					bitmask = temp[1].Trim();
                }
				else if (line.StartsWith("mem"))
                {
					// schreibe an Adresse
					var memAdr = temp[0].Trim();
					int memAdress = Convert.ToInt32(Regex.Replace(memAdr, "[^0-9]", string.Empty));
					var value = temp[1].Trim();
					var bitValue = Convert.ToString(int.Parse(value), 2).PadLeft(36, '0').ToArray();

					for (var i = 0; i < bitmask.Length; i++)
						bitValue[i] = bitmask[i] == 'X' ? bitValue[i] : bitmask[i];

					memValues[memAdress] = string.Join("", bitValue);
					/*
					if (memValues[memAdress] == null)
                    {
						memValues[memAdress] = "000000000000000000000000000000000000";
					}
					*/
				}
				else
                {
					throw new ArgumentException();
                }
            }
			Logger.Log($"´First Part: {memValues.Sum(x => Convert.ToInt64(x.Value, 2))}");
		}

		private void Part2()
		{

        }
    }
}