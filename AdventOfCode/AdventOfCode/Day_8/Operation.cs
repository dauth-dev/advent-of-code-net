using System;

namespace AdventOfCode.Day_8
{
	public abstract class Operation
	{
		public bool Processed { get; private set; }

		public Tuple<int, int> ProcessOperation(int currentValue, int index)
		{
			Processed = true;
			return Process(currentValue, index);
		}

		protected abstract Tuple<int, int> Process(int currentValue, int index);

		public static Operation CreateFromLine(string s)
		{
			var spaceSplittet = s.Split(" ");
			var operation = spaceSplittet[0];
			var arg = int.Parse(spaceSplittet[1]);

			Operation result = operation switch
			{
				"acc" => new AccOperation(arg),
				"jmp" => new JmpOperation(arg),
				"nop" => new NopOperation(),
				_ => new NopOperation()
			};

			return result;
		}
	}


	public class AccOperation : Operation
	{
		private readonly int _accWith;

		public AccOperation(int accWith)
		{
			_accWith = accWith;
		}

		protected override Tuple<int, int> Process(int currentValue, int index)
		{
			return Tuple.Create(currentValue + _accWith, index + 1);
		}
	}

	public class JmpOperation : Operation
	{
		private readonly int _jmpTo;

		public JmpOperation(int jmpTo)
		{
			_jmpTo = jmpTo;
		}

		protected override Tuple<int, int> Process(int currentValue, int index)
		{
			return Tuple.Create(currentValue, index + _jmpTo);
		}
	}

	public class NopOperation : Operation
	{
		protected override Tuple<int, int> Process(int currentValue, int index)
		{
			return Tuple.Create(currentValue, index+1);
		}
	}
}
