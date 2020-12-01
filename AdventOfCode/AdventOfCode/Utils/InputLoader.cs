using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdventOfCode.Utils
{
	public class InputLoader
	{
		const string BaseFolder = "..\\..\\..\\";

		public static InputLoader Instance => new InputLoader();

		private string getFileName(int year, int day, string file = "input")
		{
			return $"{BaseFolder}Day_{day}\\{file}.txt";
		}

		public async Task<IEnumerable<long>> LoadInputAsEnumerableOfNumbers(int year, int day)
		{
			Logger.Log(Directory.GetCurrentDirectory());

			var file = getFileName(year, day);
			var exists = File.Exists(file);
			if (exists == false)
			{
				throw new FileNotFoundException($"Input file '{file}' was not found!");
			}

			var lines = await File.ReadAllLinesAsync(file);

			var numbers = lines.Select(long.Parse);

			return numbers;
		}
	}
}
