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
					{
						bitValue[i] = bitmask[i] == 'X' ? bitValue[i] : bitmask[i];
					}

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
			var input = InputLoader.Instance.LoadInputAsEnumerableOfStrings(Day).ToList();
			string bitmask = string.Empty;

			/*
			input.Clear();
			input.Add("mask = 000000000000000000000000000000X1001X");
			input.Add("mem[42] = 100");
			input.Add("mask = 00000000000000000000000000000000X0XX");
			input.Add("mem[26] = 1");
			*/

			Dictionary<long, string> memValues = new Dictionary<long, string>();
			foreach (var line in input)
			{
				var temp = line.Split(new char[] { '=' });
				if (line.StartsWith("mask"))
				{
					// neue Maske
					bitmask = temp[1].Trim();
				}
				else if (line.StartsWith("mem"))
				{
					var memAdr = temp[0].Trim();
					int memAdress = Convert.ToInt32(Regex.Replace(memAdr, "[^0-9]", string.Empty));
					var value = line.Split(" = ")[1];
					var indexBitValue = Convert.ToString(memAdress, 2).PadLeft(36, '0').ToArray();
					var bitValue = Convert.ToString(int.Parse(value), 2).PadLeft(36, '0');

					for (int i = 0; i < bitmask.Length; i++)
						indexBitValue[i] = bitmask[i] == '0' ? indexBitValue[i] : bitmask[i];

					// hole alle die Adressen auf die es Auswirkungen hat
					var manipulateMemAdr = memAddressesToManipulate(string.Join("", indexBitValue));

					foreach (var temMemAdr in manipulateMemAdr)
					{
						memValues[Convert.ToInt64(temMemAdr, 2)] = bitValue;
					}
				}
				else
				{
					throw new ArgumentException();
				}
			}
			Logger.Log($"´Second Part: {memValues.Sum(x => Convert.ToInt64(x.Value, 2))}");
		}

		private static IEnumerable<string> memAddressesToManipulate(string myAdr)
		{
			if (!myAdr.Contains("X"))
			{
				// keine X => nur eine ADresse
				return new List<string> { myAdr };
			}
			else
			{
				// alle X bits durchgehen
				var memAdr1 = ReplaceFirstOccurence(myAdr, "X", "0");
				var memAdr2 = ReplaceFirstOccurence(myAdr, "X", "1");
				return memAddressesToManipulate(memAdr1).Concat(memAddressesToManipulate(memAdr2));
			}
		}

		public static string ReplaceFirstOccurence(string text, string search, string replace)
		{
			int pos = text.IndexOf(search);
			if (pos < 0)
			{
				return text;
			}
			return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
		}
	}
}