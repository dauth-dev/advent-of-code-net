using System.Linq;
using AdventOfCode.Utils;
using System;
using AdventOfCode.Day_2.Models;
using System.Collections.Generic;

using System.Linq;

using MoreLinq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day_07
{
    public class Runner : AbstractRunner
    {
        public Runner() : base(7)
        {
        }

        protected override void Process()
        {
            var input = GetBagMatrix(InputLoader.Instance.LoadInputAsEnumerableOfStrings(2020, 7));
            var result = GetResult(input, "shiny gold");
            Console.WriteLine($"First result: {result}");
            result = GetSecondResult(input, "shiny gold");
            Console.WriteLine($"Second result: {result}");
            Console.ReadLine();
        }

        public Dictionary<string, List<Bag>> GetBagMatrix(IEnumerable<string> lists)
        {
            var bagMatrix = new Dictionary<string, List<Bag>>();
            foreach (var item in lists)
            {
                var result = GetBag(item);
                bagMatrix.Add(result.name, result.bags);
            }
            return bagMatrix;
        }

        public (string name, List<Bag> bags) GetBag(string item)
        {
            var splitted = item.Split(" ");
            var name = $"{splitted[0]} {splitted[1]}";
            var bags = new List<Bag>();
            if (item.Contains("no other bags"))
            {
                return (name, bags);
            }

            var containInt = item.IndexOf("contain");
            item = item.Remove(0, containInt + 8);

            foreach (var bagString in item.Split(","))
            {
                var toOperateBagString = bagString.Replace("bags", "");
                toOperateBagString = toOperateBagString.Replace("bag", "");
                toOperateBagString = toOperateBagString.Replace(".", "");
                splitted = toOperateBagString.Trim().Split(" ");

                bags.Add(new Bag()
                {
                    Count = Convert.ToInt32(splitted[0]),
                    Name = $"{splitted[1]} {splitted[2]}"
                });
            }
            return (name, bags);
        }

        public int GetResult(Dictionary<string, List<Bag>> input, string searchForBag)
        {
            var result = 0;
            var bagsContainsToSearchForBag = new Dictionary<string, List<Bag>>();
            foreach (var item in input)
            {
                if (item.Value.Any(c => c.Name == searchForBag))
                {
                    bagsContainsToSearchForBag.Add(item.Key, item.Value.Where(c => c.Name == searchForBag).ToList());
                    foreach (var bag in item.Value)
                    {
                        if (bag.Name == searchForBag)
                        {
                            Console.WriteLine(item.Key);
                            result++;
                            break;
                        }
                    }
                    continue;
                }
            }
            var bagsSecondContainsToSearchForBag = new List<string>();
            foreach (var item in input)
            {
                if (!bagsContainsToSearchForBag.ContainsKey(item.Key))
                {
                    foreach (var bags in item.Value)
                    {
                        if (bagsContainsToSearchForBag.ContainsKey(bags.Name))
                        {
                            bagsSecondContainsToSearchForBag.Add(item.Key);
                            //Console.WriteLine(item.Key);
                            result++;
                            break;
                        }
                    }
                }
            }
            for (int i = 0; i < 2000; i++)
            {
                i++;
                foreach (var item in input)
                {
                    if (!bagsSecondContainsToSearchForBag.Contains(item.Key) && !bagsContainsToSearchForBag.ContainsKey(item.Key))
                    {
                        foreach (var bags in item.Value)
                        {
                            if (bagsSecondContainsToSearchForBag.Contains(bags.Name) && !bagsContainsToSearchForBag.ContainsKey(bags.Name))
                            {
                                bagsSecondContainsToSearchForBag.Add(item.Key);
                                //Console.WriteLine(item.Key);
                                result++;
                                break;
                            }
                        }
                    }
                }
            }

            return result;
        }

        public int GetSecondResult(Dictionary<string, List<Bag>> input, string searchForBag)
        {
            var result = 0;
            var bagsContainsToSearchForBag = new Dictionary<string, List<Bag>>();
            foreach (var item in input)
            {
                if (item.Key == searchForBag)
                {
                    foreach (var bag in item.Value)
                    {
                        result += bag.Count + GetBag(input[bag.Name], bag.Count, input);
                    }
                }
            }

            return result;
        }

        private int GetBag(List<Bag> lists, int multiply, Dictionary<string, List<Bag>> input)
        {
            var result = 0;

            foreach (var item in lists)
            {
                result += item.Count;
                result += GetBag(input[item.Name], item.Count, input);
            }

            return result * multiply;
        }
    }
}