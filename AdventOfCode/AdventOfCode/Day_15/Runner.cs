using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utils;

namespace AdventOfCode.Day_15
{
	public class Runner : AbstractRunner
    {
        private int[] inputs;

		public Runner() : base(15)
		{
		}

		protected override void Process()
		{
            // Achtung: input heute fest in der Aufgabe - überschreiben wenn euer input abweicht
            inputs = new int[] {1,17,0,10,18,11,6};   // input_ml.txt

			Part1();
			Part2();
		}

        private void Part1()
        {
            Logger.Log($"First Part: {getLastNumber(2020)}");
        }

		private void Part2()
		{
            Logger.Log($"Second Part: {getLastNumber(30000000)}");
        }

        private int getLastNumber(long iterations)
        {
            Dictionary<int, List<int>> spokenNum = new Dictionary<int, List<int>>();
            // initiiere die Liste der input Nummern
            for (int i = 0; i < inputs.Length; i++)
            {
                spokenNum.Add(inputs[i], new List<int> {i + 1});
            }

            /*
             *
             * In this game, the players take turns saying numbers. They begin by taking turns reading from a list of starting numbers (your puzzle input). Then, each turn consists of considering the most recently spoken number:
               If that was the first time the number has been spoken, the current player says 0.
               Otherwise, the number had been spoken before; the current player announces how many turns apart the number is from when it was previously spoken.
               So, after the starting numbers, each turn results in that player speaking aloud either 0 (if the last number is new) or an age (if the last number is a repeat).
             *
             */

            // die 2020. gesagte Zahl wurd gesucht
            var lastNumber = spokenNum.Last().Key;
            for (int i = spokenNum.Count; i < iterations; i++)
            {
                lastNumber = spokenNum[lastNumber].Count > 1 ? spokenNum[lastNumber].Last() - spokenNum[lastNumber][spokenNum[lastNumber].Count - 2] : 0;
                if (!spokenNum.ContainsKey(lastNumber))
                {
                    {
                        spokenNum.Add(lastNumber, new List<int>());
                    }
                }
                spokenNum[lastNumber].Add(i + 1);
            }

            return lastNumber;
        }
    }
}