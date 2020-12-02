using System.Collections;
using System.Collections.Generic;

namespace AdventOfCode.Utils
{
    public interface IInputLoader
    {
        IEnumerable<long> LoadInputAsEnumerableOfNumbers(int day, string fileNameOverride = null);
        List<BitArray> LoadInputAsBitMatrix(int year, int day, string fileName = null, string falseChar = ".", string trueChar = "#");
        IEnumerable<string> LoadInputAsEnumerableOfStrings(int year, int day, string fileName = null);
        string LoadInputAsText(int year, int day, string fileName = null);
    }
}