using System;
using System.Collections.Generic;
using AdventOfCode.Utils;

namespace AdventOfCode.Day_8
{
    public class Accumulator
    {
        private readonly IList<Operation> _operations;

        public Accumulator(IList<Operation> operations)
        {
            _operations = operations;
        }

        //public int Process(bool skipProcessed = true)
        //{
        //	var index = 0;
        //	int prevIndex = index;

        //	var currentValue = 0;
        //	var operation = _operations[index];
        //	var finished = false;

        //	var firstOperationWasProcessed = false;

        //	do
        //	{
        //		if (skipProcessed)
        //		{
        //			finished = operation.Processed;
        //		}


        //		if (operation.Processed)
        //		{
        //			Logger.Log($"First Operation was processed at index {index}: {operation}");
        //			if (firstOperationWasProcessed == false)
        //			{
        //				Console.ReadKey();
        //				firstOperationWasProcessed = true;
        //			}


        //		}

        //		if (!finished)
        //		{
        //			var next = operation.ProcessOperation(currentValue, index);

        //			currentValue = next.Item1;
        //			prevIndex = index;
        //			index = next.Item2;

        //			if (index >= _operations.Count)
        //			{
        //				Logger.Log($"index : {index}; lastIndex: {prevIndex}");
        //				finished = true;
        //			}
        //			else
        //			{
        //				operation = _operations[index];
        //			}

        //		}

        //	} while (finished == false);

        //	Logger.Log($"index : {index}; lastIndex: {prevIndex}");
        //	operation.Print();

        //	return currentValue;
        //}
        public int Process(bool skipProcessed = true)
        {
            var index = 0;
            int prevIndex = index;
            var currentValue = 0;
            var operation = _operations[index];
            bool finished;
            var firstOperationWasProcessed = false;
            do
            {
                if (skipProcessed)
                {
                    finished = operation.Processed;
                }


                if (operation.Processed)
                {
                    Logger.Log($"First Operation was processed at index {index}: {operation}");
                    if (firstOperationWasProcessed == false)
                    {
                        //Console.Read();
                        firstOperationWasProcessed = true;
                    }


                }
                finished = operation.Processed;
                if (!finished)
                {
                    var next = operation.ProcessOperation(currentValue, index);

                    currentValue = next.Item1;
                    index = next.Item2;
                    prevIndex = index;

                    if (index >= _operations.Count)
                    {
                        Logger.Log($"index : {index}; lastIndex: {prevIndex}");
                        finished = true;
                    }
                    else
                    {
                        operation = _operations[index];
                    }

                }

            } while (finished == false);

            Logger.Log($"index : {index}; lastIndex: {prevIndex}");
            operation.Print();

            return currentValue;
        }
    }
}
