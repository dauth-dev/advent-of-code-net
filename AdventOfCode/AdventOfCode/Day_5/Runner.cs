using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utils;
using MoreLinq;

namespace AdventOfCode.Day_05
{
    public class Runner : AbstractRunner
    {
        public Runner() : base(5)
        {
        }

        protected override void Process()
        {
            var input = InputLoader.Instance.LoadInputAsEnumerableOfStrings(Day);
            var highestId = 0;
            foreach (var item in input)
            {
                var id = GetId(item);
                if (id > highestId)
                {
                    highestId = id;
                }
            }
            Console.WriteLine($"Highest id:{highestId}");
            Console.WriteLine($"Missing id:{GetMissingId(input.ToList())}");
            Console.ReadLine();
        }

        public int GetMissingId(List<string> list)
        {
            var ids = new List<int>();
            foreach (var item in list)
            {
                var id = GetId(item);
                ids.Add(id);
            }
            var next = ids.OrderBy(c => c).First();
            var missing = 0;
            foreach (var item in ids.OrderBy(c => c))
            {
                if (item != next)
                {
                    missing = next;
                    next = item;
                }
                next++;
            }
            return missing;
        }

        public int GetId(string value)
        {
            var row = GetRow(value);
            var column = GetColumn(value);

            return row * 8 + column;
        }

        public int GetRow(string v)
        {
            var range = new Range(0, 127);
            var inputString = v.Take(7);
            foreach (var item in inputString)
            {
                if (item == 'F')
                {
                    var result = (range.End.Value + range.Start.Value) / 2;
                    range = new Range(range.Start, result);
                }
                else
                {
                    var result = (range.End.Value + range.Start.Value) / 2;
                    result++;
                    range = new Range(result, range.End);
                }
                //Console.WriteLine(range.Start + "-" + range.End);
            }
            if (inputString.Last() == 'F')
            {
                return range.Start.Value;
            }
            return range.End.Value;
        }

        public int GetColumn(string v)
        {
            var range = new Range(0, 7);
            var inputString = v.Substring(7, 3);
            foreach (var item in inputString)
            {
                if (item == 'L')
                {
                    var result = (range.End.Value + range.Start.Value) / 2;
                    range = new Range(range.Start, result);
                }
                else
                {
                    var result = (range.End.Value + range.Start.Value) / 2;
                    result++;
                    range = new Range(result, range.End);
                }
                //Console.WriteLine(range.Start + "-" + range.End);
            }
            if (inputString.Last() == 'L')
            {
                return range.Start.Value;
            }
            return range.End.Value;
        }
    }
}