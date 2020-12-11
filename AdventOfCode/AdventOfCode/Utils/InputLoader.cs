using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AdventOfCode.Utils
{
    public class InputLoader : IInputLoader
    {


        const string BaseFolder = ".\\";
        private readonly AppSettings appSettings = new AppSettings();
        public static readonly IInputLoader Instance = new InputLoader();

        private string getFileName(int day, string file = null)
        {
            file ??= $"input";

            if (this.appSettings.UserInputFileNameMappingOverride.ContainsKey(Environment.UserName))
            {
                file = this.appSettings.UserInputFileNameMappingOverride[Environment.UserName];
            }


            return $"{BaseFolder}Day_{day}\\{file}.txt";
        }

        private IEnumerable<string> ReadAllLines(int day, string fileName = null)
        {
            var file = getFileName(day, fileName);
            if (!File.Exists(file))
            {
                throw new FileNotFoundException($"Input file '{file}' was not found in folder '{Directory.GetCurrentDirectory()}. Please try to include the file to the build with 'Copy to Output Directory' = 'Copy Always | Copy If newer'!");
            }

            return File.ReadAllLines(file);
        }

        public string LoadInputAsText(int day, string fileName = null)
        {
            Logger.Log(Directory.GetCurrentDirectory());

            var file = getFileName(day, fileName);
            var exists = File.Exists(file);
            if (exists == false)
            {
                throw new FileNotFoundException($"Input file '{file}' was not found!");
            }

            return File.ReadAllText(file);
        }

        public IEnumerable<string> LoadInputAsEnumerableOfStrings(int day, string fileName = null)
        {
            return ReadAllLines(day, fileName);
        }

        public List<BitArray> LoadInputAsBitMatrix(int day, string fileName = null, string falseChar = ".", string trueChar = "#")
        {
            Logger.Log(Directory.GetCurrentDirectory());

            var file = getFileName(day, fileName);
            var exists = File.Exists(file);
            if (exists == false)
            {
                throw new FileNotFoundException($"Input file '{file}' was not found!");
            }

            var lines = File.ReadLines(file);
            List<BitArray> bitMatrix = new List<BitArray>();

            foreach (string line in lines)
            {
                var test = line;
                test = test.Replace(falseChar, "0").Replace(trueChar, "1");
                var bitArray = new BitArray(test.Length, false);
                for (int j = 0; j < test.Length; j++)
                {
                    if (test[j].ToString() == "1")
                    {
                        bitArray[j] = true;
                    }
                }
                bitMatrix.Add(bitArray);
            }

            return bitMatrix;
        }

        public List<List<int>> LoadInputAsTriStateMatrix(int day, string fileName = null, string negativeChar = ".",
            string naChar = "L", string positiveChar = "#")
        {
            Logger.Log(Directory.GetCurrentDirectory());

            var file = getFileName(day, fileName);
            var exists = File.Exists(file);
            if (exists == false)
            {
                throw new FileNotFoundException($"Input file '{file}' was not found!");
            }

            List<List<int>> retList = new List<List<int>>();
            var lines = File.ReadLines(file);

            foreach (string line in lines)
            {
                var test = line.Trim();
                if (string.IsNullOrWhiteSpace(test))
                {
                    continue;
                }
                List<int> gridRow = new List<int>();
                for (int i = 0; i < test.Length; i++)
                {
                    var c = test[i].ToString();
                    if (c == negativeChar)
                    {
                        gridRow.Add(-1);
                    }
                    else if (c == naChar)
                    {
                        gridRow.Add(0);
                    }
                    else if (c == positiveChar)
                    {
                        gridRow.Add(1);
                    }
                    else
                    {
                        throw new ArgumentException();
                    }
                }
                retList.Add(gridRow);
            }

            return retList;

        }

        public IEnumerable<long> LoadInputAsEnumerableOfNumbers(int day, string fileNameOverride = null)
        {
            return ReadAllLines(day, fileNameOverride).Select(long.Parse);
        }
    }
}