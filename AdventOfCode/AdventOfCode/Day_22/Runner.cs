using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utils;

namespace AdventOfCode.Day_22
{
	public class Runner : AbstractRunner
	{
		List<string> input;
		List<int> cardsPlayer1 = new List<int>();
		List<int> cardsPlayer2 = new List<int>();

		public Runner() : base(22)
		{
		}

		private void LoadData()
        {
            cardsPlayer1.Clear();
            cardsPlayer2.Clear();
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
            LoadData();
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
            Logger.Log($"First Part: {calcWinSum(winList)}");
        }

        private int calcWinSum(List<int> winningCards)
        {
            int winSum = 0;
            for (int i = winningCards.Count - 1; i >= 0; i--)
            {
                int factor = winningCards.Count - i;
                winSum += winningCards[i] * factor;
            }
            return winSum;
        }

        private void Part2()
		{
            // recursive comabt
            var winner = playRecursive(cardsPlayer1, cardsPlayer2);
            var winList = winner == 1 ? cardsPlayer1 : cardsPlayer2;
            Logger.Log($"Second Part: {calcWinSum(winList)}");
        }

        private int playRecursive(List<int> cardsPlayer1, List<int> cardsPlayer2)
        {
            var player1History = new List<List<int>>();
            var player2History = new List<List<int>>();

            while (cardsPlayer1.Count > 0 && cardsPlayer2.Count > 0)
            {
                /* Before either player deals a card, if there was a previous round in this game that had exactly the same cards 
                 * in the same order in the same players' decks, the game instantly ends in a win for player 1. 
                 * */
                foreach (var x in player1History)
                {
                    if (x.SequenceEqual(cardsPlayer1))
                    {
                        return 1;
                    }
                }
                foreach (var x in player2History)
                {
                    if (x.SequenceEqual(cardsPlayer2))
                    {
                        return 1;
                    }
                }

                // Achtung: ToList() notwendig damit keine Refernez aufs Origianl die später verwendet wird ....
                player1History.Add(cardsPlayer1.ToList());
                player2History.Add(cardsPlayer2.ToList());

                var p1Card = cardsPlayer1.First();
                cardsPlayer1.Remove(p1Card);
                var p2Card = cardsPlayer2.First();
                cardsPlayer2.Remove(p2Card);

                /* If both players have at least as many cards remaining in their deck as the value of the card they just drew, 
                 * the winner of the round is determined by playing a new game of Recursive Combat
                 * */
                if (p1Card <= cardsPlayer1.Count && p2Card <= cardsPlayer2.Count)
                {
                    var winner = playRecursive(cardsPlayer1.Take(p1Card).ToList(), cardsPlayer2.Take(p2Card).ToList());
                    if (winner == 1)
                    {
                        cardsPlayer1.Add(p1Card);
                        cardsPlayer1.Add(p2Card);
                    }
                    else
                    {
                        cardsPlayer2.Add(p2Card);
                        cardsPlayer2.Add(p1Card);
                    }
                }
                // gleiche Regeln wie im combat Mode
                else if (p1Card > p2Card)
                {
                    cardsPlayer1.Add(p1Card);
                    cardsPlayer1.Add(p2Card);
                }
                else if (p1Card < p2Card)
                {
                    cardsPlayer2.Add(p2Card);
                    cardsPlayer2.Add(p1Card);
                }
            }

            // gewonne hat wer Karten hat
            if (cardsPlayer1.Count > 0)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
    }
}