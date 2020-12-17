using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utils;

namespace AdventOfCode.Day_17
{
	public class Runner : AbstractRunner
	{
		public Runner() : base(17)
		{
		}

		protected override void Process()
		{
			Part1();
			Part2();
		}

        private void Part1()
        {
			// aktiv = #, inaktiv = .
			var input = InputLoader.Instance.LoadInputAsBitMatrix(Day);

			// Würfel aufbauen x/y/z. Das bitARray ist die unterste Scheibe
			var conwayCube = new Dictionary<(int x, int y, int z), bool>();
			for (int i = 0; i < input.Count; i++)
            {
				for (int j = 0; j < input[0].Count; j++)
                {
					conwayCube[(i, j, 0)] = input[i][j];
				}
			}

            var cubeModified = new Dictionary<(int x, int y, int z), bool>();
            for (int i=0; i<6;i++)
            {
                foreach (var cubeItem in conwayCube)
                {
                    var itemNeighborStates = Neightbors3D(cubeItem.Key, conwayCube);

                    foreach (var neighbor in itemNeighborStates)
                    {
                        // Achtung: die Nachbarn der Nachbarn beeinflussen das Ergebnis
                        // If a cube is inactive but exactly 3 of its neighbors are active, the cube becomes active
                        var neighborsOfNeighbors = Neightbors3D(neighbor.Key, conwayCube).Where(e => conwayCube.ContainsKey(e.Key));

                        if (neighborsOfNeighbors.Count(e => e.Value == true) == 3)
                        {
                            cubeModified[neighbor.Key] = true;
                        }
                    }

                    // If a cube is active and exactly 2 or 3 of its neighbors are also active, the cube remains active. Otherwise, the cube becomes inactive.
                    // If a cube is inactive but exactly 3 of its neighbors are active, the cube becomes active. Otherwise, the cube remains inactive
                    if (cubeItem.Value == true && (itemNeighborStates.Count(e => e.Value == true) == 2 || itemNeighborStates.Count(e => e.Value == true) == 3))
                    {
                        cubeModified[cubeItem.Key] = cubeItem.Value;
                    }
                    else if (cubeItem.Value == false && itemNeighborStates.Count(e => e.Value == true) == 3)
                    {
                        cubeModified[cubeItem.Key] = true;
                    }
                    else
                    {
                        cubeModified[cubeItem.Key] = false;
                    }
                }
                // Originalwürfel verändern für den nächsten Durchlauf
                conwayCube = cubeModified.ToDictionary(e => e.Key, e => e.Value);
            }
            int countActiveCubes = conwayCube.Count(e => e.Value == true);
            Logger.Log($"First Part: {countActiveCubes}");
        }

        protected static Dictionary<(int x, int y, int z), bool> Neightbors3D((int x, int y, int z) gridCoord, Dictionary<(int x, int y, int z), bool> currentCube)
        {
            var retList = new Dictionary<(int x, int y, int z), bool>();

            // Nachbarkoordinaten 3 * 3 * 3 - 1 (Originalwürfel in der Mitte) = 26 Stati
            for (int i = gridCoord.x - 1; i <= gridCoord.x + 1; i++)
            {
                for (int j = gridCoord.y - 1; j <= gridCoord.y + 1; j++)
                {
                    for (int k = gridCoord.z - 1; k <= gridCoord.z + 1; k++)
                    {
                        if (i == gridCoord.x && j == gridCoord.y && k == gridCoord.z)
                        {
                            // zu prüfende Koordinate ignorieren
                            continue;
                        }
                        else
                        {
                            if (currentCube.ContainsKey((i, j, k)))
                            {
                                retList.Add((i, j, k), currentCube[(i, j, k)]);
                            }
                            else
                            {
                                // Koordinate außerhalb des Würfels => automatisch false
                                retList.Add((i, j, k), false);
                            }
                        }
                    }
                }
            }

            return retList;
        }

        private void Part2()
		{

        }
    }
}