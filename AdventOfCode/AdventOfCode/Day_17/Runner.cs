using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utils;

namespace AdventOfCode.Day_17
{
	public class Runner : AbstractRunner
	{

        Dictionary<(int x, int y, int z), bool> conwayCube3D = new Dictionary<(int x, int y, int z), bool>();
        Dictionary<(int x, int y, int z, int w), bool> conwayCube4D = new Dictionary<(int x, int y, int z, int w), bool>();
        const int loops = 6;

        public Runner() : base(17)
		{
		}

		protected override void Process()
		{
            // some Background for the rules see https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life

            // aktiv = #, inaktiv = .
            var input = InputLoader.Instance.LoadInputAsBitMatrix(Day);
            // 3D-Würfel aufbauen x/y/z. Das bitARray ist die unterste Scheibe
            // 4D-Würfel aufbauen x/y/z/w. Der 3D-Würfel ist die unterste "Scheibe"
            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[0].Count; j++)
                {
                    conwayCube3D[(i, j, 0)] = input[i][j];
                    conwayCube4D[(i, j, 0, 0)] = input[i][j];
                }
            }

            Part1();
			Part2();
		}

        private void Part1()
        {
            // 3D Fall
            var cubeModified = new Dictionary<(int x, int y, int z), bool>();
            for (int i=0; i < loops; i++)
            {
                foreach (var cubeItem in conwayCube3D)      // alle 3D - Koordinaten aka 8 * 8 * 1 = 64 Stück
                {
                    var itemNeighborStates = Neighbors3D(cubeItem.Key, conwayCube3D);

                    foreach (var neighbor in itemNeighborStates)
                    {
                        // Achtung: die Nachbarn der Nachbarn beeinflussen das Ergebnis
                        // If a cube is inactive but exactly 3 of its neighbors are active, the cube becomes active
                        var neighborsOfNeighbors = Neighbors3D(neighbor.Key, conwayCube3D).Where(e => conwayCube3D.ContainsKey(e.Key));

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
                conwayCube3D = cubeModified.ToDictionary(e => e.Key, e => e.Value);
            }
            int countActiveCubes = conwayCube3D.Count(e => e.Value == true);
            Logger.Log($"First Part: {countActiveCubes}");
        }

        protected static Dictionary<(int x, int y, int z), bool> Neighbors3D((int x, int y, int z) gridCoord, Dictionary<(int x, int y, int z), bool> currentCube)
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
                    } // k (z-Achse)
                } // j (y-Achse))
            } // i (x-Achse)

            return retList;
        }

        protected static Dictionary<(int x, int y, int z, int w), bool> Neighbors4D((int x, int y, int z, int w) gridCoord, Dictionary<(int x, int y, int z, int w), bool> currentCube)
        {
            var retList = new Dictionary<(int x, int y, int z, int w), bool>();

            // Nachbarkoordinaten 3 * 3 * 3 * 3 - 1 (Originalwürfel in der Mitte) = 80 Stati
            // (möglicher Generalisierungsansatz auf N Dimensionen siehe https://de.mathworks.com/matlabcentral/fileexchange/29330-neighbour-points-in-a-matrix)
            for (int i = gridCoord.x - 1; i <= gridCoord.x + 1; i++)
            {
                for (int j = gridCoord.y - 1; j <= gridCoord.y + 1; j++)
                {
                    for (int k = gridCoord.z - 1; k <= gridCoord.z + 1; k++)
                    {
                        for (int l = gridCoord.w - 1; l <= gridCoord.w + 1; l++)
                        {
                            if (i == gridCoord.x && j == gridCoord.y && k == gridCoord.z && l == gridCoord.w)
                            {
                                // zu prüfende Koordinate ignorieren
                                continue;
                            }
                            else
                            {
                                if (currentCube.ContainsKey((i, j, k, l)))
                                {
                                    retList.Add((i, j, k, l), currentCube[(i, j, k, l)]);
                                }
                                else
                                {
                                    // Koordinate außerhalb des Würfels => automatisch false
                                    retList.Add((i, j, k, l), false);
                                }
                            }
                        } // l (w-Achse)
                    } // k (z-Achse)
                } // j (y-Achse)
            } // i (x-Achse)
            return retList;
        }

        private void Part2()
		{
            // 4D Fall
            var cubeModified = new Dictionary<(int x, int y, int z, int w), bool>();
            for (int i = 0; i < loops; i++)
            {
                foreach (var cubeItem in conwayCube4D) // wie im 3D Fall 8 * 8 * 1 * 1 = 64 Mini-Kubes, aber mit entsprechend mehr Nachbarn
                {
                    var itemNeighborStates = Neighbors4D(cubeItem.Key, conwayCube4D);

                    foreach (var neighbor in itemNeighborStates)
                    {
                        // Achtung: die Nachbarn der Nachbarn beeinflussen das Ergebnis
                        // If a cube is inactive but exactly 3 of its neighbors are active, the cube becomes active
                        var neighborsOfNeighbors = Neighbors4D(neighbor.Key, conwayCube4D).Where(e => conwayCube4D.ContainsKey(e.Key));

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
                conwayCube4D = cubeModified.ToDictionary(e => e.Key, e => e.Value);
            }
            int countActiveCubes = conwayCube4D.Count(e => e.Value == true);
            Logger.Log($"Second Part: {countActiveCubes}");
        }
    }
}