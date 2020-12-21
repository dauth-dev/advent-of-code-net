using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utils;

namespace AdventOfCode.Day_21
{
	public class Runner : AbstractRunner
	{
		List<string> input;
        Dictionary<string, int> ingredientCount = new Dictionary<string, int>();
        Dictionary<string, HashSet<string>> allergenMapping = new Dictionary<string, HashSet<string>>();

        public Runner() : base(21)
		{
		}

		protected override void Process()
		{
			input = InputLoader.Instance.LoadInputAsEnumerableOfStrings(Day).ToList();
            LoadData();
			Part1();
			Part2();
		}

        private void Part1()
        {
            /*
			 * 
			 * Each allergen is found in exactly one ingredient. Each ingredient contains zero or one allergen. 
			 * Allergens aren't always marked; when they're listed (as in (contains nuts, shellfish) after an ingredients
			 * list), the ingredient that contains each listed allergen will be somewhere in the corresponding ingredients
			 * list. However, even if an allergen isn't listed, the ingredient that contains that allergen could still be present: 
			 * maybe they forgot to label it, or maybe it was labeled in a language you don't know.

For example, consider the following list of foods:

mxmxvkd kfcds sqjhc nhms (contains dairy, fish)
trh fvjkl sbzzf mxmxvkd (contains dairy)
sqjhc fvjkl (contains soy)
sqjhc mxmxvkd sbzzf (contains fish)

The first food in the list has four ingredients (written in a language you don't understand): mxmxvkd, kfcds, sqjhc, and nhms
While the food might contain other allergens, a few allergens the food definitely contains are listed afterward: dairy and fish.

The first step is to determine which ingredients can't possibly contain any of the allergens in any food in your list. 
			In the above example, none of the ingredients kfcds, nhms, sbzzf, or trh can contain an allergen. 
			Counting the number of times any of these ingredients appear in any ingredients list produces 5: 
			they all appear once each except sbzzf, which appears twice.
			 */



            var alList = allergenMapping.Values.SelectMany(x => x).ToList();
            
            int allergenCount = 0;
            int ingCount = 0;
            // foreach(var singleAllergen in allergenMapping.Values) // 8 Stück
            // {
                foreach (var x in ingredientCount.Keys)
                {
                    ingCount++;
                    if (!alList.Contains(x))
                    {
                        allergenCount += ingredientCount[x];
                        // ingredientCount.Remove(x);
                    }
                }
            // }


            Logger.Log($"First Part: {allergenCount}");

        }
        private void Part2()
		{
            /*
             * 
             * Arrange the ingredients alphabetically by their allergen and separate them by commas to produce
             * your canonical dangerous ingredient list. (There should not be any spaces in your canonical dangerous 
             * ingredient list.) In the above example, this would be mxmxvkd,sqjhc,fvjkl.    
             * 
             * */
        }

        private void LoadData()
        {
            foreach (var line in input)
            {
                var ingredient = line.Substring(0, line.IndexOf("(")).Trim(new char[] { '(', ' ' });
                var allergen = line.Substring(line.IndexOf("(") + 1, line.Length - (line.IndexOf("(") + 1)).Replace(")", string.Empty).Trim();
                allergen = allergen.Replace("contains ", string.Empty);

                var singleIngredients = ingredient.Split(" ").ToList();
                var allergenList = allergen.Split(", ").ToList();


                foreach (var ing in singleIngredients)
                {
                    if (ingredientCount.ContainsKey(ing))
                    {
                        ingredientCount[ing] += 1;
                    }
                    else
                    {
                        ingredientCount[ing] = 1;
                    }
                }

                foreach (var alg in allergenList)
                {
                    if (allergenMapping.ContainsKey(alg))
                    {
                        allergenMapping[alg].IntersectWith(singleIngredients);
                    }
                    else
                    {
                        allergenMapping[alg] = new HashSet<string>(singleIngredients);
                    }
                }

            }
        }
    }
}