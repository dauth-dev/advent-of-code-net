using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utils;

namespace AdventOfCode.Day_22
{
	public class Runner : AbstractRunner
	{
		List<string> input;
		List<long> cardsPlayer1 = new List<long>();
		List<long> cardsPlayer2 = new List<long>();

		public Runner() : base(22)
		{
		}

		private void LoadData()
        {
			input = InputLoader.Instance.LoadInputAsEnumerableOfStrings(Day).ToList();
			bool p1 = true;
			for (int i = 1; i < input.Count; i++)
            {
				if (input[i].StartsWith("Player"))
                {
					p1 = false;
                }
				else if (string.IsNullOrEmpty(input[i]))
                {
					continue;
                }
                else
                {
					if (p1)
                    {
						cardsPlayer1.Add(Convert.ToInt32(input[i]));
                    }
					else
                    {
						cardsPlayer2.Add(Convert.ToInt32(input[i]));
					}
                }
            }
		}

		protected override void Process()
		{
			LoadData();
			Part1();
			Part2();
		}

        private void Part1()
        {
            // play a game of combat
            while (cardsPlayer1.Count > 0 && cardsPlayer2.Count > 0)
            {
                var p1Card = cardsPlayer1.First();
                var p2Card = cardsPlayer2.First();
                if (p1Card > p2Card)
                {
                    cardsPlayer2.Remove(p2Card);

                    cardsPlayer1.Remove(p1Card);
                    // unten wieder anfügen, darunter die des anderen
                    cardsPlayer1.Add(p1Card);
                    cardsPlayer1.Add(p2Card);
                }
                else if (p1Card < p2Card)
                {
                    cardsPlayer1.Remove(p1Card);

                    cardsPlayer2.Remove(p2Card);
                    // unten wieder anfügen, darunter die des anderen
                    cardsPlayer2.Add(p2Card);
                    cardsPlayer2.Add(p1Card);
                }
                // Gleichstand ist nicht definiert ?
            }

            var winList = cardsPlayer1.Count > 0 ? cardsPlayer1 : cardsPlayer2;
            long winSum = 0;

            for (int i = winList.Count - 1; i >= 0; i--)
            {
                long factor = Convert.ToInt64(winList.Count - i);
                winSum += winList[i] * factor;
            }
            Logger.Log($"First Part: {winSum}");
        }

        private void Part2()
		{

        }
    }
}