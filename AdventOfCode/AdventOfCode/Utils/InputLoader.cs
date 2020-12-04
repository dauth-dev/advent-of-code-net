using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AdventOfCode.Utils
{
    public class InputLoader : IInputLoader
    {
        public InputLoader(IOptions<AppSettings> appSettings, ILogger<InputLoader> logger)
        {
            this.appSettings = appSettings?.Value ?? throw new ArgumentNullException(nameof(appSettings));
            this.logger = logger;
        }

        const string BaseFolder = "..\\..\\..\\";
        private readonly AppSettings appSettings;
        private readonly ILogger<InputLoader> logger;

        private string getFileName(string day, string file = null)
        {
            file ??= $"input";

            if (this.appSettings.UserInputFileNameMappingOverride.ContainsKey(Environment.UserName))
            {
                file = this.appSettings.UserInputFileNameMappingOverride[Environment.UserName];
            }


            return $"{BaseFolder}Day_{day}\\{file}.txt";
        }

        private IEnumerable<string> ReadAllLines(string day, string fileName = null)
        {
            var file = getFileName(day, fileName);
            if (!File.Exists(file))
            {
                throw new FileNotFoundException($"Input file '{file}' was not found in folder '{Directory.GetCurrentDirectory()}'!");
            }

            return File.ReadAllLines(file);
        }

        public string LoadInputAsText(string day, string fileName = null)
        {
            logger.LogInformation(Directory.GetCurrentDirectory());

            var file = getFileName(day, fileName);
            var exists = File.Exists(file);
            if (exists == false)
            {
                throw new FileNotFoundException($"Input file '{file}' was not found!");
            }

            return File.ReadAllText(file);
        }

        public IEnumerable<string> LoadInputAsEnumerableOfStrings(string day, string fileName = null)
        {
            return ReadAllLines(day, fileName);
        }

        public List<BitArray> LoadInputAsBitMatrix(string day, string fileName = null, string falseChar = ".", string trueChar = "#")
        {
            logger.LogInformation(Directory.GetCurrentDirectory());

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

        public IEnumerable<long> LoadInputAsEnumerableOfNumbers(string day, string fileNameOverride = null)
        {
            return ReadAllLines(day, fileNameOverride).Select(l => long.Parse(l));
        }
    }
}