using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utils;

namespace AdventOfCode.Day_24
{
	public enum HEXGRID_DIRECTION
    {
		NORTHEAST,
		EAST,
		SOUTHEAST,
		SOUTHWEST,
		WEST,
		NORTHWEST
    }

	public class Runner : AbstractRunner
	{
		List<string> input;
		
		public Runner() : base(24)
		{
		}

		protected override void Process()
		{
			input = InputLoader.Instance.LoadInputAsEnumerableOfStrings(Day).ToList();
			Part1();
			Part2();
		}

		private void Part1()
		{
			// alle grids starten weiß (false)
			var grid = new Dictionary<(int, int), bool>();
			foreach (var line in input)
			{
				var instructions = getDirections(line);
				var position = (0, 0);

				foreach (var move in instructions)
				{
					switch (move)
					{
						case HEXGRID_DIRECTION.EAST:
							position = (position.Item1 + 1, position.Item2);
							break;
						case HEXGRID_DIRECTION.SOUTHEAST:
							position = (position.Item1 + 1, position.Item2 - 1);
							break;
						case HEXGRID_DIRECTION.SOUTHWEST:
							position = (position.Item1, position.Item2 - 1);
							break;
						case HEXGRID_DIRECTION.WEST:
							position = (position.Item1 - 1, position.Item2);
							break;
						case HEXGRID_DIRECTION.NORTHWEST:
							position = (position.Item1 - 1, position.Item2 + 1);
							break;
						case HEXGRID_DIRECTION.NORTHEAST:
							position = (position.Item1, position.Item2 + 1);
							break;
					}
				} // foreach instruction
				if (grid.ContainsKey(position))
				{
					if (grid[position] == true)
					{
						grid[position] = false;
					}
					else
					{
						grid[position] = true;
					}
				}
				else
				{
					// draufgetreten wird zu schwarz
					grid.Add(position, true);
				}
			} // foreach
			int counter = grid.Count(x => x.Value == true);

			Logger.Log($"First Part: {counter}");
		}

		private void Part2()
		{

        }

		private List<HEXGRID_DIRECTION> getDirections(string line)
        {
			List<HEXGRID_DIRECTION> retList = new List<HEXGRID_DIRECTION>();

			while(!string.IsNullOrEmpty(line))
            {
				if (line.StartsWith("ne"))
                {
					retList.Add(HEXGRID_DIRECTION.NORTHEAST);
					line = line[2..];
                }
				else if (line.StartsWith("nw"))
				{
					retList.Add(HEXGRID_DIRECTION.NORTHWEST);
					line = line[2..];
				}
				else if (line.StartsWith("sw"))
				{
					retList.Add(HEXGRID_DIRECTION.SOUTHWEST);
					line = line[2..];
				}
				else if (line.StartsWith("se"))
				{
					retList.Add(HEXGRID_DIRECTION.SOUTHEAST);
					line = line[2..];
				}
				else if (line.StartsWith("e"))
				{
					retList.Add(HEXGRID_DIRECTION.EAST);
					line = line[1..];
				}
				else if (line.StartsWith("w"))
				{
					retList.Add(HEXGRID_DIRECTION.WEST);
					line = line[1..];
				}
			}

			return retList;
        }
    }
}