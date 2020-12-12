using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utils;

namespace AdventOfCode.Day_12
{
	public class Runner : AbstractRunner
	{
		public Runner() : base(12)
		{
		}

		protected override void Process()
		{
			Part1();
			Part2();
		}

        private void Part1()
        {
			var input = InputLoader.Instance.LoadInputAsEnumerableOfStrings(Day);
			var East = 0;
			var North = 0;
			var South = 0;
			var West = 0;
			var Angle = 90;	// Start nach Osten
			foreach(var ins in input)
            {
				var command = ins[0].ToString();
				var movement = Convert.ToInt32(ins.Substring(1));
				switch(command)
                {
					case "N":
						North += movement;
						break;
					case "S":
						South += movement;
						break;
					case "E":
						East += movement;
						break;
					case "W":
						West += movement;
						break;
					case "R":
						Angle += movement;
						if (Angle >= 360)
                        {
							Angle -= 360;
                        }
						break;  
					case "L":
						Angle -= movement;
						if (Angle < 0)
                        {
							Angle += 360;
                        }
						break;
					case "F":
						switch(Angle)
                        {
							case 0:
								North += movement;
								break;
							case 90:
								East += movement;
								break;
							case 180:
								South += movement;
								break;
							case 270:
								West += movement;
								break;
                        }
						break;
				}
            }

			var northing = North - South;
			var easting = East - West;
			Logger.Log($"First Part: {Math.Abs(easting) + Math.Abs(northing)}");
		}

		private void Part2()
		{

        }
    }
}