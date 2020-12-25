using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utils;

namespace AdventOfCode.Day_24
{
	/*
	 * 
	 * https://en.wikipedia.org/wiki/Hex_map
	 * wir haben hier von den Anweisungen ein Grid in der Form
	 *   /\
	 *   ||
	 *   \/
	 *   d.h. 6 Richtungen die wir auf Koordinaten mappen
	 * 
	 * */
	public enum HEXGRID_DIRECTION
    {
		NORTHEAST,		// x + 0, y - 1
		EAST,			// x + 1, y + 0
		SOUTHEAST,		// x + 1. y + 1
		SOUTHWEST,		// x + 0, y + 1
		WEST,			// x - 1, y + 0
		NORTHWEST		// x - 1, y - 1
    }

	public class Runner : AbstractRunner
	{
		List<string> input;
		// alle grids starten weiß (false)
		Dictionary<(int, int), bool> grid = new Dictionary<(int, int), bool>();          // alle grids starten weiß (false)
		
		public Runner() : base(24)
		{
		}

		protected override void Process()
		{
			input = InputLoader.Instance.LoadInputAsEnumerableOfStrings(Day).ToList();
			Part1();
			Part2();
		}

		private void PrepareGrid()
        {
			grid = new Dictionary<(int, int), bool>();
			// alle grids starten weiß (false)
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
							position = (position.Item1 + 1, position.Item2 + 1);
							break;
						case HEXGRID_DIRECTION.SOUTHWEST:
							position = (position.Item1, position.Item2 + 1);
							break;
						case HEXGRID_DIRECTION.WEST:
							position = (position.Item1 - 1, position.Item2);
							break;
						case HEXGRID_DIRECTION.NORTHWEST:
							position = (position.Item1 - 1, position.Item2 - 1);
							break;
						case HEXGRID_DIRECTION.NORTHEAST:
							position = (position.Item1, position.Item2 - 1);
							break;
					}
				} // foreach instruction
				flipTile(position);
			} // foreach
		}

		private void Part1()
		{
			PrepareGrid();
			int counter = grid.Count(x => x.Value == true);

			Logger.Log($"First Part: {counter}");
		}

		private void Part2()
		{
			PrepareGrid();

			for (int i = 0; i < 100; i++)
			{
				var flipTiles = CalculateFlips(grid.Keys.ToHashSet());

				foreach (var tile in flipTiles)
				{
					if (tile.Value)
					{
						flipTile(tile.Key);
					}
				}
			}

			int counter = grid.Count(x => x.Value == true);

			Logger.Log($"Second Part: {counter}");
		}

		private Dictionary<(int, int), bool> CalculateFlips(HashSet<(int, int)> tiles)
		{
			var neighbors = new HashSet<(int, int)>();
			foreach (var tile in tiles)
			{
				// Ost-West
				neighbors.Add((tile.Item1 + 1, tile.Item2));
				neighbors.Add((tile.Item1 - 1, tile.Item2));
				// Südost
				neighbors.Add((tile.Item1 + 1, tile.Item2 + 1));
				// Südwest
				neighbors.Add((tile.Item1, tile.Item2 + 1));
				// Nordost
				neighbors.Add((tile.Item1, tile.Item2 - 1));
				// Nordwest
				neighbors.Add((tile.Item1 - 1, tile.Item2 - 1));
			}

			return tiles.Concat(neighbors).ToHashSet().ToDictionary(x => x, needsFlip);
		}


		private void flipTile((int, int) position)
        {
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
		}

		private bool needsFlip((int, int) position)
        {
            int neighborsCount = 0;
            // Ost-West
            if (checkState((position.Item1 + 1, position.Item2))) { neighborsCount++; }
			if (checkState((position.Item1 - 1, position.Item2))) { neighborsCount++; }
			// Südost, Nordost
			if (checkState((position.Item1 + 1, position.Item2 + 1))) { neighborsCount++; }
			if (checkState((position.Item1, position.Item2 - 1))) { neighborsCount++; }
			// Nordwest, Südwest
			if (checkState((position.Item1 - 1, position.Item2 - 1))) { neighborsCount++; }
			if (checkState((position.Item1, position.Item2 + 1))) { neighborsCount++; }

            bool retValue;
            if (checkState(position))
            {
                // schwarz
                retValue = neighborsCount > 2 || neighborsCount == 0;
            }
            else
            {
                // weiß
                retValue = neighborsCount == 2;
            }
            return retValue;
        }

		private bool checkState((int, int) position)
        {
			if (grid.ContainsKey(position))
            {
				return grid[position];
            }
			else
            {
				return false;
            }
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