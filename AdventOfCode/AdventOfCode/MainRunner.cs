using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
	internal class MainRunner
	{
		private IList<IRunner> runners = new List<IRunner>() {
			new Day_01.Runner(),
			new Day_02.Runner(),
			new Day_03.Runner(),
			 new Day_04.Runner(),
			 new Day_05.Runner(),
			 new Day_06.Runner(),
			 new Day_07.Runner(),
			 new  Day_08.Runner(),
             new  Day_09.Runner(),
			  new  Day_10.Runner(),
			  new  Day_11.Runner(),
			  new  Day_12.Runner(),
			  new  Day_13.Runner(),
			  new  Day_14.Runner(),
			  new  Day_15.Runner(),
			  new  Day_16.Runner() /*,
			  new  Day_17.Runner(),
			  new  Day_18.Runner(),
			  new  Day_19.Runner(),
			  new  Day_20.Runner(),
			  new  Day_21.Runner(),
			  new  Day_22.Runner(),
			  new  Day_23.Runner(),
			  new  Day_24.Runner(),
			  new  Day_25.Runner()
			  */
		};

		public static void Main()
		{
			var runner = new MainRunner();
			runner.runners.Last().Run();
		}
	}
}