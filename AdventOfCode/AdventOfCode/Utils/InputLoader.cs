using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Utils
{
    public class InputLoader : IInputLoader, IInputLoader1
    {
        private const string BaseFolder = "..\\..\\..\\";

        private string getFileName(int day, string file = null)
        {
            file ??= $"{Environment.UserName}-input";
            if (Environment.UserName == "Markus.Lind")
            {
                file = "input_ml";
            }

            return $"{BaseFolder}Day_{day}\\{file}.txt";
        }

        private IEnumerable<string> ReadAllLines(int day, string fileName = null)
        {
            Logger.Log(Directory.GetCurrentDirectory());

            var file = getFileName(day, fileName);
            if (!File.Exists(file))
            {
                throw new FileNotFoundException($"Input file '{file}' was not found!");
            }

            return File.ReadAllLines(file);
        }

        public string LoadInputAsText(int year, int day, string fileName = null)
        {
            Logger.Log(Directory.GetCurrentDirectory());

            var file = getFileName(year, day, fileName);
            var exists = File.Exists(file);
            if (exists == false)
            {
                throw new FileNotFoundException($"Input file '{file}' was not found!");
            }

            return File.ReadAllText(file);
        }

        public IEnumerable<string> LoadInputAsEnumerableOfStrings(int year, int day, string fileName = null)
        {
            Logger.Log(Directory.GetCurrentDirectory());

            var file = getFileName(year, day, fileName);
            var exists = File.Exists(file);
            if (exists == false)
            {
                throw new FileNotFoundException($"Input file '{file}' was not found!");
            }


            return lines;
        }

        public List<BitArray> LoadInputAsBitMatrix(int year, int day, string fileName = null, string falseChar = ".", string trueChar = "#")
        {
            Logger.Log(Directory.GetCurrentDirectory());

            var file = getFileName(year, day, fileName);
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
    }
}