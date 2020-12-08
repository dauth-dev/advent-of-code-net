using System;
using AdventOfCode.Day_8;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests.Day_8
{
	[TestClass]
	public class AccumulatorTest
	{
		[TestMethod]
		public void AccTest()
		{
			var o1 = new AccOperation(1);
			o1.ProcessOperation(0, 0).Should().Be(Tuple.Create(1, 1));
			o1.Processed.Should().BeTrue();

			var o2 = new AccOperation(4);
			o2.ProcessOperation(1, 4).Should().Be(Tuple.Create(5, 5));
			o2.Processed.Should().BeTrue();
		}

		[TestMethod]
		public void JmpTest()
		{
			var o1 = new JmpOperation(1);
			o1.ProcessOperation(0, 0).Should().Be(Tuple.Create(0, 1));
			o1.Processed.Should().BeTrue();

			var o2 = new JmpOperation(-2);
			o2.ProcessOperation(2, 4).Should().Be(Tuple.Create(2, 2));
			o2.Processed.Should().BeTrue();
		}

		[TestMethod]
		public void Accumulator_processes_operations_simple()
		{
			var operations = new Operation[]
			{
				new NopOperation(),
				new AccOperation(1),
				new JmpOperation(2),
				new NopOperation(),
				new NopOperation(),
				new AccOperation(1),
			};
			var accmulator = new Accumulator(operations);
			accmulator.Process().Should().Be(2);
		}

		[TestMethod]
		public void Accumulator_processes_operations()
		{
			var operations = new Operation[]
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
			var accmulator = new Accumulator(operations);
			accmulator.Process().Should().Be(5);
		}
	}
}
