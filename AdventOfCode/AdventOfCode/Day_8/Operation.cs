using System;
using AdventOfCode.Utils;

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
				"nop" => new NopOperation(arg),
				_ => new NopOperation(arg)
			};

			return result;
		}

		public void Print()
		{
			Logger.Log(this.ToString());
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

		public override string ToString() => $"acc {_accWith}";
	}

	public class JmpOperation : Operation
	{
		private readonly int maxOperations = 647;
		private readonly int _jmpTo;

		public JmpOperation(int jmpTo)
		{
			_jmpTo = jmpTo;
		}

		protected override Tuple<int, int> Process(int currentValue, int index)
		{
			var newIndex = index + _jmpTo;
			if (newIndex < 0 || newIndex > maxOperations)
			{
				Logger.Log($"invalid jmp operation at index {index}: {ToString()}");
			}
			Logger.Log($"index: {index} => {ToString()}");

			return Tuple.Create(currentValue, index + _jmpTo);
		}

		public override string ToString() => $"jmp {_jmpTo}";
	}

	public class NopOperation : Operation
	{
		private readonly int? _arg;

		public NopOperation(int? arg = null)
		{
			_arg = arg;
		}

		protected override Tuple<int, int> Process(int currentValue, int index)
		{
			return Tuple.Create(currentValue, index + 1);
		}

		public override string ToString() => $"nop {_arg}";
	}
}
