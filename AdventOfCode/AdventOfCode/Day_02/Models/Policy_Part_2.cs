using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day_2.Models
{
    public class Policy_Part_2 : IPolicy
    {
        public string Character { get; set; }
        public int Max { get; set; }
        public int Minimal { get; set; }

        public bool IsValid(string input)
        {
            var chars = new List<char>() {
                input.ElementAt(Minimal-1),
                input.ElementAt(Max-1),
            };

            var occurence = chars.Count(c => c == Character.First());

            return occurence == 1;
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
