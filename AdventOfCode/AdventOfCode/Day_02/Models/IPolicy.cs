using System.Text.RegularExpressions;

namespace AdventOfCode.Day_2.Models
{
    public interface IPolicy
    {
        string Character { get; set; }
        int Max { get; set; }
        int Minimal { get; set; }

        bool IsValid(string input);

        void ParseFromMatches(MatchCollection matches);
    }
}