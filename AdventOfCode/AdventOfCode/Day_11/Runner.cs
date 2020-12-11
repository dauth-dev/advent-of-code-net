using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using AdventOfCode.Utils;

namespace AdventOfCode.Day_11
{
	public class Runner : AbstractRunner
    {
        private List<List<int>> input;
        private List<List<int>> clonedMatrix;
		public Runner() : base(11)
		{
		}

		protected override void Process()
		{
			// Part1();
			Part2();
		}

        private void Part1()
        {
            input = InputLoader.Instance.LoadInputAsTriStateMatrix(Day);
            clonedMatrix = InputLoader.Instance.LoadInputAsTriStateMatrix(Day);
            bool stateChange = true;

            do
            {
                stateChange = false;
                // Original wird zum update
                input = cloneMatrix(clonedMatrix);
                //printMatrix();

                for (int i = 0; i < input.Count; i++)
                {
                    for (int j = 0; j < input[i].Count; j++)
                    {
                        // -1 = Floor
                        // 0 = nicht besetzt
                        // 1 = besetzt
                        if (input[i][j] == -1)
                        {
                            continue;
                        }
                        if (input[i][j] == 0)
                        {
                            if (neighborsOccupied(i, j) == 0) // Nachbarn = 0 oder erster Durchlauf
                            {
                                clonedMatrix[i][j] = 1;
                                stateChange = true;
                            }
                        }
                        else if (input[i][j] == 1)
                        {
                            if (neighborsOccupied(i, j) >= 4) // Nachbarn >= 4
                            {
                                clonedMatrix[i][j] = 0;
                                stateChange = true;
                            }
                        }
                    }
                }
            } while (stateChange);

            Logger.Log($"First Part: {countSeats()}");
        }

        private void printMatrix()
        {
            Debug.WriteLine("-------------------");
            foreach (List<int> line in input)
            {
                foreach (int elem in line)
                {
                    switch (elem)
                    {
                        case -1:
                            Debug.Write(".");
                            break;
                        case 0:
                            Debug.Write("L");
                            break;
                        case 1:
                            Debug.Write("#");
                            break;
                    }
                }

                Debug.WriteLine(string.Empty);
            }
        }

        private int countSeats()
        {
            int counter = 0;
            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[i].Count; j++)
                {
                    if (input[i][j] == 1)
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }

        private List<List<int>> cloneMatrix(List<List<int>> inputMatrix)
        {
            List<List<int>> newMatrix = new List<List<int>>();
            foreach (var listElem in inputMatrix)
            {
                List<int> line = new List<int>();
                foreach (var y in listElem)
                {
                    line.Add(y);
                }
                newMatrix.Add(line);
            }
            return newMatrix;
        }

        private int neighborsOccupied(int xPos, int yPos)
        {
            /*
             * alle x um den Punkt prüfen
             *    x    x    x
             *    x    .    x
             *    x    x    x
             */

            int countNeighbors = 0;
            for (int dx = -1; dx < 2; dx++)
            {
                for (int dy = -1; dy < 2; dy++)
                {
                    if (dx == 0 && dy == 0)
                    {
                        continue;   // der Platz
                    }

                    int testX = xPos + dx;
                    int testY = yPos + dy;
                    if ((testX < 0 || testX >= input.Count) || (testY < 0 || testY >= input[0].Count))
                    {
                        // außerhalb der Plätze
                        continue;
                    }

                    if (input[testX][testY] == 1)
                    {
                        countNeighbors++;
                    }
                }
            }

            return countNeighbors;
        }

		private void Part2()
		{
            input = InputLoader.Instance.LoadInputAsTriStateMatrix(Day);
            clonedMatrix = InputLoader.Instance.LoadInputAsTriStateMatrix(Day);
            bool stateChange = true;

            do
            {
                stateChange = false;
                input = cloneMatrix(clonedMatrix);
                printMatrix();

                for (int i = 0; i < input.Count; i++)
                {
                    for (int j = 0; j < input[i].Count; j++)
                    {
                        // -1 = Floor
                        // 0 = nicht besetzt
                        // 1 = besetzt
                        if (input[i][j] == -1)
                        {
                            continue;
                        }
                        if (input[i][j] == 0)
                        {
                            if (neighborsOccupiedInSight(i, j) < 5) 
                            {
                                clonedMatrix[i][j] = 1;
                                stateChange = true;
                            }
                        }
                        else if (input[i][j] == 1)
                        {
                            if (neighborsOccupiedInSight(i, j) >= 5) // Nachbarn in Sicht >=5
                            {
                                clonedMatrix[i][j] = 0;
                                stateChange = true;
                            }
                            else
                            {
                                clonedMatrix[i][j] = 1;
                                stateChange = true;
                            }
                        }
                    }
                }
            } while (stateChange);

            Logger.Log($"Second Part: {countSeats()}");
        }

        private int neighborsOccupiedInSight(int xPos, int yPos)
        {
            /*
             * alle x um den Punkt prüfen Sichtlinie
             * x   x    x    x   x
             * x   x    x    x   x
             * x   x    .    x   x
             * x   x    x    x   x
             * etc
             */

            int countNeighbors = 0;

            for (int dx = -1; dx < 2; dx++)
            {
                for (int dy = -1; dy < 2; dy++)
                {
                    for (int multiplier = 1; multiplier < Math.Max(input.Count, input[0].Count); multiplier++)
                    {
                        if (dx == 0 && dy == 0)
                        {
                            continue; // der Platz
                        }

                        int testX = xPos + multiplier * dx;
                        int testY = yPos + multiplier * dy;
                        if ((testX < 0 || testX >= input.Count) || (testY < 0 || testY >= input[0].Count))
                        {
                            // außerhalb der Plätze
                            break;
                        }

                        if (input[testX][testY] == 1)
                        {
                            countNeighbors++;
                            break;
                        }
                    }
                }
            }

            return countNeighbors;
        }
    }
}