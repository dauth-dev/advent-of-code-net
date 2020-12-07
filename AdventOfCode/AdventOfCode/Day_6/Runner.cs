using System;
using System.Collections.Generic;
using AdventOfCode.Utils;

namespace AdventOfCode.Day_06
{
    public class Runner : AbstractRunner
    {
        public Runner() : base(6)
        {
        }

        protected override void Process()
        {
            var input = InputLoader.Instance.LoadInputAsEnumerableOfStrings(Day);

            var getGroups = GetGroups(input);
            var clearedData = GetClearedData(getGroups);
            Console.WriteLine($"Result: {GetResult(clearedData)}");

            getGroups = GetSecondGroups(input);
            clearedData = GetClearedData(getGroups);
            Console.WriteLine($"Result: {GetResult(clearedData)}");
            Console.ReadLine();
        }

        public Dictionary<int, List<char>> GetGroups(IEnumerable<string> input)
        {
            var dictionary = new Dictionary<int, List<char>>();

            var newItem = new List<char>();
            var count = 0;
            foreach (var line in input)
            {
                foreach (var item in line)
                {
                    if (!newItem.Contains(item))
                    {
                        newItem.Add(item);
                    }
                }
                if (string.IsNullOrWhiteSpace(line))
                {
                    count++;
                    dictionary.Add(count, newItem);
                    newItem = new List<char>();
                }
            }
            count++;
            dictionary.Add(count, newItem);
            return dictionary;
        }

        public Dictionary<int, List<char>> GetSecondGroups(IEnumerable<string> input)
        {
            var dictionary = new Dictionary<int, List<char>>();

            var rows = new List<List<char>>();
            var count = 0;
            foreach (var line in input)
            {
                var currentList = new List<char>();
                foreach (var item in line)
                {
                    currentList.Add(item);
                }

                if (string.IsNullOrWhiteSpace(line))
                {
                    Remove(dictionary, ref rows, ref count);
                }
                else
                {
                    rows.Add(currentList);
                }
            }
            Remove(dictionary, ref rows, ref count);
            return dictionary;
        }

        private static void Remove(Dictionary<int, List<char>> dictionary, ref List<List<char>> rows, ref int count)
        {
            count++;

            var items = rows[0];

            for (int i = 1; i < rows.Count; i++)
            {
                var row = rows[i];
                var remove = new List<char>();
                foreach (var rowItem in items)
                {
                    if (!row.Contains(rowItem))
                    {
                        remove.Add(rowItem);
                    }
                }
                foreach (var ritem in remove)
                {
                    items.Remove(ritem);
                }
            }
            rows = new List<List<char>>();
            dictionary.Add(count, items);
        }

        public int GetResult(Dictionary<int, int> clearedData)
        {
            var result = 0;
            foreach (var item in clearedData)
            {
                result += item.Value;
            }

            return result;
        }

        public Dictionary<int, int> GetClearedData(Dictionary<int, List<char>> groups)
        {
            var dictionary = new Dictionary<int, int>();
            foreach (var item in groups)
            {
                dictionary.Add(item.Key, item.Value.Count);
            }
            return dictionary;
        }
    }
}