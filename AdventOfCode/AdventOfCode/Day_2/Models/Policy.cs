using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day_2.Models
{
    public class Policy
    {
        public int Minimal { get; set; }

        public Policy(int minimal, int maximal, string character)
        {
            Minimal = minimal;
            Max = maximal;
            Character = character;
        }

        public int Max { get; set; }

        public string Character { get; set; }

        public bool IsValid(string input)
        {
            var charToTest = Character.ToCharArray().First();
            var occurence = input.ToCharArray().Count(c => c == charToTest);
            var isValid = occurence >= Minimal && occurence <= Max;
            return isValid;
        }

        internal static Policy ParseFromMatches(MatchCollection matches)
        {
            var min = int.Parse(matches.First().Groups[1].Value);
            var max = int.Parse(matches.First().Groups[2].Value);
            var c = matches.First().Groups[3].Value;

            return new Policy(min, max, c);
        }
    }
}
