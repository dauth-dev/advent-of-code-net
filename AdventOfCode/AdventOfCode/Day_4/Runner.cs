using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AdventOfCode.Utils;

namespace AdventOfCode.Day_04
{
    public class Runner : AbstractRunner
    {
        public Runner() : base(4)
        {
        }

        protected override void Process()
        {
            var input = InputLoader.Instance.LoadInputAsEnumerableOfStrings(Day);

            var resultFirst = GetValidPassports(input);
            Console.WriteLine($"Valid passports: {resultFirst.Count}");

            Console.WriteLine($"Valid passports: {ExtendedChecks(resultFirst).Count}");
        }

        public List<Dictionary<string, string>> ExtendedChecks(List<Dictionary<string, string>> passports)
        {
            var validPassports = new List<Dictionary<string, string>>();

            foreach (var item in passports)
            {
                var valid = true;

                //(Birth Year)
                //iyr(Issue Year)
                //eyr(Expiration Year)
                //hgt(Height)
                //hcl(Hair Color)
                //ecl(Eye Color)
                //pid(Passport ID)
                //cid(Country ID)

                if (!CheckYear(item["byr"], 1920, 2002))
                {
                    continue;
                }
                if (!CheckYear(item["iyr"], 2010, 2020))
                {
                    continue;
                }
                if (!CheckYear(item["eyr"], 2020, 2030))
                {
                    continue;
                }
                if (!CheckHeight(item["hgt"]))
                {
                    continue;
                }
                if (!CheckHairColor(item["hcl"]))
                {
                    continue;
                }
                if (!CheckEyeColor(item["ecl"]))
                {
                    continue;
                }
                if (!CheckPid(item["pid"]))
                {
                    continue;
                }

                if (valid)
                {
                    validPassports.Add(item);
                }
            }
            return validPassports;
        }

        public bool CheckPid(string v)
        {
            return Regex.IsMatch(v.Replace("#", "").Trim(), @"^[0-9]+$") && v.Length == 9;
        }

        public bool CheckEyeColor(string v)
        {
            var validEyeColors = new List<string>()
            {
                "amb", "blu", "brn", "gry", "grn", "hzl", "oth"
            };

            return validEyeColors.Contains(v.ToLower());
        }

        public bool CheckHairColor(string v)
        {
            if (v.StartsWith("#") && v.Length == 7)
            {
                return Regex.IsMatch(v.Replace("#", "").Trim(), @"^[a-f0-9]+$");
            }
            return false;
        }

        public bool CheckHeight(string v)
        {
            if (v.Contains("cm"))
            {
                var number = Convert.ToInt32(v.Replace("cm", ""));
                return number >= 150 && number <= 193;
            }
            if (v.Contains("in"))
            {
                var number = Convert.ToInt32(v.Replace("in", ""));
                return number >= 59 && number <= 76;
            }
            return false;
        }

        public bool CheckYear(string v, int min, int max)
        {
            if (v.Length != 4)
            {
                return false;
            }
            var year = Convert.ToInt32(v);
            return year >= min && year <= max;
        }

        public List<Dictionary<string, string>> GetValidPassports(IEnumerable<string> input)
        {
            var exceptedFields = new List<string>()
            {
                "byr",
                 "iyr",
                 "eyr",
                "hgt",
                 "hcl",
                 "ecl",
                 "pid",
            };
            var countValid = 0;

            var passports = GetPassportsFromFile(input);
            var currentValid = true;
            var validPassports = new List<Dictionary<string, string>>();
            foreach (var passport in passports)
            {
                currentValid = true;
                foreach (var requiredField in exceptedFields)
                {
                    if (!passport.ContainsKey(requiredField))
                    {
                        currentValid = false;
                    }
                }
                if (currentValid)
                {
                    validPassports.Add(passport);
                    countValid++;
                }
            }

            return validPassports;
        }

        public List<Dictionary<string, string>> GetPassportsFromFile(IEnumerable<string> input)
        {
            var passports = new List<Dictionary<string, string>>();
            var currentPassport = new Dictionary<string, string>();
            foreach (var item in input)
            {
                if (string.IsNullOrWhiteSpace(item))
                {
                    passports.Add(currentPassport);
                    currentPassport = new Dictionary<string, string>();
                }
                else
                {
                    foreach (var keyVal in ExtractInformations(item))
                    {
                        currentPassport.Add(keyVal.Key, keyVal.Value);
                    }
                }
            }
            passports.Add(currentPassport);
            return passports;
        }

        public Dictionary<string, string> ExtractInformations(string line)
        {
            var dictionary = new Dictionary<string, string>();
            foreach (var item in line.Split(" "))
            {
                var splitted = item.Split(":");
                dictionary.Add(splitted[0], splitted[1]);
            }
            return dictionary;
        }
    }
}