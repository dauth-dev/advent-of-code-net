using System.Linq;
using System;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day_2.Models
{
    public static class Mapper
    {
        public static Tuple<TPolicy, string> MapToPolicy<TPolicy>(string input) where TPolicy : IPolicy, new()
        {
            var regex = new Regex(@"(\d{1,2})-(\d{1,2})\s(\w):\s(\w*)");

            if (!regex.IsMatch(input))
            {
                throw new Exception($"invalid Input: {input}");
            }

            var matches = regex.Matches(input);


            var policy = new TPolicy();
            policy.ParseFromMatches(matches);

            return Tuple.Create(policy, matches.First().Groups[4].Value);
        }
    }
}
