using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day_2.Models
{
    public class Policy_Part_1 : IPolicy
    {
        public int Minimal { get; set; }

        public Policy_Part_1() { }

        public Policy_Part_1(int minimal, int maximal, string character)
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

        public void ParseFromMatches(MatchCollection matches)
        {
            var min = int.Parse(matches.First().Groups[1].Value);
            var max = int.Parse(matches.First().Groups[2].Value);
            var c = matches.First().Groups[3].Value;

            this.Minimal = min;
            this.Max = max;
            this.Character = c;
        }
    }
}
