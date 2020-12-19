using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Utils;

namespace AdventOfCode.Day_19
{
	public class Runner : AbstractRunner
	{
		public Runner() : base(19)
		{
		}

		protected override void Process()
		{
			Part1();
			Part2();
		}

        private void Part1()
        {
			/*
			 * 
			 * 
			 *  They sent you a list of the rules valid messages should obey and a list of received messages they've collected so far (your puzzle input).
The rules for valid messages (the top part of your puzzle input) are numbered and build upon each other. For example:

0: 1 2
1: "a"
2: 1 3 | 3 1
3: "b"

Some rules, like 3: "b", simply match a single character (in this case, b).
The remaining rules list the sub-rules that must be followed; for example, the rule 0: 1 2 means that to match rule 0, 
			the text being checked must match rule 1, and the text after the part that matched rule 1 must then match rule 2.
Some of the rules have multiple lists of sub-rules separated by a pipe (|). This means that at least one list of sub-rules must match. (The ones that match might be different each time the rule is encountered.) For example, 
			the rule 2: 1 3 | 3 1 means that to match rule 2, the text being checked must match rule 1
			followed by rule 3 or it must match rule 3 followed by rule 1.            
			 * 
			 * 
			 * */

			var input = InputLoader.Instance.LoadInputAsEnumerableOfStrings(Day).ToList();
			var isRule = true;
			Dictionary<int, string> rules = new Dictionary<int, string>();
			List<string> messages = new List<string>();
			foreach(var line in input)
            {
				if (string.IsNullOrEmpty(line))
                {
					isRule = false;
					continue;
                }
				if (isRule)
                {
					rules.Add(Convert.ToInt32(line.Split(new char[] { ':' })[0]), line.Split(new char[] { ':' })[1].Replace("\"", string.Empty));
                }
				else
                {
					messages.Add(line);
                }
            }

			// Your goal is to determine the number of messages that completely match rule 0.
			
			// ==> 
			// Regeln die aus a-z bestehen sind direkte Regex (es gibt im Input nur eine Regel für a und eine für b ...
			// Zahlen verweisen auf andere Regex. Diese müssen als Gruppe übersetzt werden
			// die | in den Regelen können 1:1 in den RegEx übernommen werden als Gruppe (min 1 muss gematcht werdeb)

			var test = rules[0];
			while (Regex.IsMatch(test, @"\d+"))
			{
				test = Regex.Replace(test, @"\d+", matchEval => "(" + rules[Convert.ToInt32(matchEval.Value)] + ")");
			}
			test = test.Replace(" ", string.Empty).Trim();

			// Ergibt ein Regex-Monster mit 4302 Zeichen ... 
			var regexForTesting = new Regex("^" + test + "$");
			var counter = 0;
			foreach(var msg in messages)
            {
				if (regexForTesting.IsMatch(msg))
                {
					counter++;
                }
            }
			Logger.Log($"First Part: {counter}");
		}

		private void Part2()
		{

        }
    }
}