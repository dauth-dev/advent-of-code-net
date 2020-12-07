using System.Collections.Generic;

namespace AdventOfCode
{
    public class AppSettings
    {
        public readonly Dictionary<string, string> UserInputFileNameMappingOverride = new Dictionary<string, string>()
        {
            {"Markus.Lind", "input_ml"},
            {"daniel.auth", "input_da"},
            {"daniel auth", "input_da"}
        };

        public int Year => 2020;
    }
}
