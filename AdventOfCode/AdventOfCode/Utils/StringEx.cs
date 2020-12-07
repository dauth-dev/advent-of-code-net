using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Utils
{
    public static class StringEx
    {
        public static string JoinWith(this IEnumerable<string> items, string joinWith = null)
        {
            joinWith ??= string.Empty;
            return items.Aggregate((current, next) => $"{current}{joinWith}{next}");
        }
    }
}
