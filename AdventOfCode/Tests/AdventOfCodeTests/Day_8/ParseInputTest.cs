using AdventOfCode.Day_8;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests.Day_8
{
	[TestClass]
	public class ParseInputTest
	{
		[TestMethod]
		public void Lines_can_be_parsed_to_operations()
		{
			var sut = new AdventOfCode.Day_08.Runner();

			var lines = new[]
			{
				"nop +0",
				"acc +1",
				"jmp +4",
				"acc +3",
				"jmp -3",
				"acc -99",
				"acc +1",
				"jmp -4",
				"acc +6"
			};

			var exptected = new Operation[]
			{
				new NopOperation(),
				new AccOperation(1),
				new JmpOperation(4),
				new AccOperation(3),
				new JmpOperation(-3),
				new AccOperation(-99),
				new AccOperation(1),
				new JmpOperation(-4),
				new AccOperation(6),
			};

			var actual = sut.ParseInput(lines);
			actual.Should().BeEquivalentTo(exptected, o => o.WithStrictOrdering());

		}

		[TestMethod]
		public void Operation_can_be_created_by_string()
		{
			Operation.CreateFromLine("nop +0").Should().BeEquivalentTo(new NopOperation());
			Operation.CreateFromLine("acc +1").Should().BeEquivalentTo(new AccOperation(1));
			Operation.CreateFromLine("acc -10").Should().BeEquivalentTo(new AccOperation(-10));
			Operation.CreateFromLine("jmp +1").Should().BeEquivalentTo(new JmpOperation(1));
		}
	}
}
