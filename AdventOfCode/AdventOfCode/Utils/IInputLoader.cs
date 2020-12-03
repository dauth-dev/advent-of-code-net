using System.Collections;
using System.Collections.Generic;

namespace AdventOfCode.Utils
{
    public interface IInputLoader
    {
        IEnumerable<long> LoadInputAsEnumerableOfNumbers(int day, string fileNameOverride = null);
        IEnumerable<string> LoadInputAsEnumerableOfStrings(int day, string fileName = null);

        List<BitArray> LoadInputAsBitMatrix(int day, string fileName = null, string falseChar = ".", string trueChar = "#");
        string LoadInputAsText(int day, string fileName = null);
    }
}