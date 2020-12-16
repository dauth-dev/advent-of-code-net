using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using AdventOfCode.Utils;

namespace AdventOfCode.Day_16
{
	public class Runner : AbstractRunner
	{
        inputTickets dataInput = new inputTickets();

		public Runner() : base(16)
        {
            loadDataStructure();
        }

		protected override void Process()
		{
			Part1();
			Part2();
		}

        private void loadDataStructure()
        {
            var input = InputLoader.Instance.LoadInputAsEnumerableOfStrings(Day).ToList();
            dataInput = new inputTickets();

            input.Clear();
            input.Add("class: 1-3 or 5-7");
            input.Add("row: 6-11 or 33-44");
            input.Add("seat: 13-40 or 45-50");
            input.Add("");
            input.Add("your ticket:");
            input.Add("7,1,14");
            input.Add("");
            input.Add("nearby tickets:");
            input.Add("7,3,47");
            input.Add("40,4,50");
            input.Add("55,2,20");
            input.Add("38,6,12");
            
            var section = "prop";

            for (int i=0; i<input.Count; i++)
            {
                var line = input[i];
                if (string.IsNullOrEmpty(line))
                {
                    i++;
                    line = input[i];
                    if (line == "your ticket:")
                    {
                        section = "your";
                    }
                    else if (line == "nearby tickets:")
                    {
                        section = "nearby";
                    }
                }
                else
                {
                    switch (section)
                    {
                        case "prop":
                            string[] temp = line.Split(new char[] {':'});
                            dataInput.properties.Add(temp[0], temp[1]);
                            break;
                        case "your":
                            string[] temp2 = line.Split(new char[] {','});
                            foreach (var x in temp2)
                            {
                                dataInput.ticket.Add(Convert.ToInt32(x));
                            }
                            break;
                        case "nearby":
                            string[] temp3 = line.Split(new char[] {','});
                            foreach (var x in temp3)
                            {
                                dataInput.nearbyTickes.Add(Convert.ToInt32(x));
                            }
                            break;
                    }
                }
            }
        }

        private void Part1()
        {
            // prüfe nearby auf invalid (ohne eigenes Ticket)
            foreach(var x in dataInput.properties.Keys)
            {
                var tempVal = dataInput.properties[x].Trim().Split();
                foreach (var y in tempVal)
                {
                    if (y == "or")
                    {
                        continue;
                    }

                    var limits = y.Split(new char[] {'-'});
                    for (int j = Convert.ToInt32(limits[0]); j <= Convert.ToInt32(limits[1]); j++)
                    {
                        if (!dataInput.validPropNumbers.Contains(j))
                        {
                            dataInput.validPropNumbers.Add(j);
                        }
                    }
                }

            }

            int errCount = 0;
            int errRate = 0;
            foreach (var x in dataInput.nearbyTickes)
            {
                if (!dataInput.validPropNumbers.Contains(x))
                {
                    errCount++;
                    errRate += x;
                    dataInput.validNearbyTickes.Add(x);
                }
            }

            Logger.Log($"First Part: {errRate}");
        }

		private void Part2()
		{

        }
    }

    public class inputTickets
    {
        public Dictionary<string,string> properties = new Dictionary<string, string>();
        public List<int> ticket = new List<int>();
        public List<int> nearbyTickes = new List<int>();
        public List<int> validNearbyTickes = new List<int>();
        public List<int> validPropNumbers = new List<int>();
    }
}