using System.Linq;
using System;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day_2.Models
{
    public static class Mapper
    {
        public static Tuple<Policy, string> MapToPolicy(string input)
        {
            var regex = new Regex(@"(\d{1,2})-(\d{1,2})\s(\w):\s(\w*)");

            if (!regex.IsMatch(input))
            {
                throw new Exception($"invalid Input: {input}");
            }

            var matches = regex.Matches(input);

            var policy = Policy.ParseFromMatches(matches);

            return Tuple.Create(policy, matches.First().Groups[4].Value);
        }
    }
}
