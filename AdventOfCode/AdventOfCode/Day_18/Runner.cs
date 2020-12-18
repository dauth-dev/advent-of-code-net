using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utils;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AdventOfCode.Day_18
{
	public class Runner : AbstractRunner
	{
		List<string> input;

		public Runner() : base(18)
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
			/*
			string test = "1 + (2 * 3) + (4 * (5 + 6))";
			/*
			 * 1 + (2 * 3) + (4 * (5 + 6))
				1 +    6    + (4 * (5 + 6))
					7      + (4 * (5 + 6))
					7      + (4 *   11   )
					7      +     44
					51
			 * */
			// test = test.Replace("*", "-");

			// wir können das * nicht von der Precedence überschreiben daher simulieren wir das mal mit dem - was die gleiche Pre hat wie +
			long sum = 0;
			foreach (var line in input)
			{
				sum += fakeMath(SyntaxFactory.ParseExpression(line.Replace("*", "-")));
			}
			Logger.Log($"First Part: {sum}");
		}

		private void Part2()
		{
			input = InputLoader.Instance.LoadInputAsEnumerableOfStrings(Day).ToList();
			long sum = 0;
			foreach (var line in input)
			{
				// das gleiche in grün nur diesmal mit Addition vor Multiplikation d.h. aus dem + müssen wir ein * machen das dann in fakeMath auf ein + gemappt wird :)
				sum += fakeMath(SyntaxFactory.ParseExpression(line.Replace("*", "-").Replace("+", "*")));
			}
			Logger.Log($"Second Part: {sum}");
		}


		private static long fakeMath(ExpressionSyntax es)
		{

			// es gibt jede Menge mehr, hier sind nur die Expressions aus der Aufgabe drin. Zur Sicherheite werfen wir NotImplementedExceptions
			// siehe https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.expressionsyntax?view=roslyn-dotnet

			// das sind einerseits string, true, false, null etc.
			// aber auch NumericLiteralExpression aka Zahlen :)
			if (es is LiteralExpressionSyntax n)
			{
				return long.Parse(n.Token.ValueText);
			}

			if (es is BinaryExpressionSyntax bs)
			{
				switch (bs.Kind())
				{
					case SyntaxKind.AddExpression:
						return fakeMath(bs.Left) + fakeMath(bs.Right);
					case SyntaxKind.MultiplyExpression:
						// das ist unser gefaktes multiply
						return fakeMath(bs.Left) + fakeMath(bs.Right);
					case SyntaxKind.SubtractExpression:
						// wir ersetzen zuvor im Ursprungsausdruck das * mit - wegen der Precedence
						return fakeMath(bs.Left) * fakeMath(bs.Right);
					default:
						throw new NotImplementedException();
				}
			}

			// Klammern
			if (es is ParenthesizedExpressionSyntax paran)
			{
				return fakeMath(paran.Expression);
			}

			throw new NotImplementedException();
		}
	}
}
