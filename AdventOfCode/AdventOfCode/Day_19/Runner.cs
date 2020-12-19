using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Utils;

namespace AdventOfCode.Day_19
{
	public class Runner : AbstractRunner
	{
		List<string> input;
		Dictionary<int, string> rules = new Dictionary<int, string>();
		List<string> messages = new List<string>();

		public Runner() : base(19)
		{
		}

		protected override void Process()
		{
			input = InputLoader.Instance.LoadInputAsEnumerableOfStrings(Day).ToList();
			var isRule = true;
			foreach (var line in input)
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


			// Your goal is to determine the number of messages that completely match rule 0.
			
			// ==> 
			// Regeln die aus a-z bestehen sind direkte Regex (es gibt im Input nur eine Regel für a und eine für b ...
			// Zahlen verweisen auf andere Regex. Diese müssen als Gruppe übersetzt werden
			// die | in den Regelen können 1:1 in den RegEx übernommen werden als Gruppe (min 1 muss gematcht werdeb)

			Logger.Log($"First Part: {countMatches()}");
		}

		private void Part2()
		{
			/*
			 * As you look over the list of messages, you realize your matching rules aren't quite right. 
			 * To fix them, completely replace rules 8: 42 and 11: 42 31 with the following:
8: 42 | 42 8
11: 42 31 | 42 11 31
This small change has a big impact: now, the rules do contain loops, and the list of messages they could hypothetically 
			match is infinite. You'll need to determine how these changes affect which messages are valid.
			*/

			rules[8] = "42 | 42 8";
			rules[11] = "42 31 | 42 11 31";

			/*
			 *
			 * Fortunately, many of the rules are unaffected by this change; it might help to start by looking at which rules
			 * always match the same set of values and how those rules (especially rules 42 and 31) 
			 * are used by the new versions of rules 8 and 11
			 * 
			 * 
			 * => 42 und 31 kommen nur in den beiden Regeln vor
			 * die neue R8 besagt dann  dass R42 1 16 | 47 26 rekursiv wird
			 * aus R11: 42 31 wird 42 11 31 was bedeutet 42 42 31 31
			 * wird zu ( 1 16 | 47 26 ) (1 16 |47 26) (83 26 | 90 16) 
			 * wird zu ( 1 b | 47 26) ( 1 b | 47 26) ( 83 26 | 90 16)
			 * ...
			 * */

			// die längste Nachricht waren 97 Zeichen
			// mit den Testdaten hat es gereicht das auf 4 fach aufzublährn statt rekursiv. Später ggf. generalisieren
			rules[8] = "42 | 42 (42 | 42 (42 | 42 (42 | 42 (42))))";
			rules[11] = "42 31 | 42 (42 31 | 42 (42 31 | 42 (42 31 | 42 31) 31) 31) 31";

			
			Logger.Log($"Second Part: {countMatches()}");
		}

		private int countMatches()
        {
			var test = rules[0];
			while (Regex.IsMatch(test, @"\d+"))
			{
				test = Regex.Replace(test, @"\d+", matchEval => "(" + rules[Convert.ToInt32(matchEval.Value)] + ")");
			}
			test = test.Replace(" ", string.Empty).Trim();

			// Ergibt (in Part 1) ein Regex-Monster mit 4302 Zeichen ... 
			var regexForTesting = new Regex("^" + test + "$");
			var counter = 0;
			foreach (var msg in messages)
			{
				if (regexForTesting.IsMatch(msg))
				{
					counter++;
				}
			}
			return counter;
		}
    }
}