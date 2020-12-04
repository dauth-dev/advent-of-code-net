using System.Collections;
using System.Collections.Generic;

namespace AdventOfCode.Utils
{
    public interface IInputLoader
    {
        IEnumerable<long> LoadInputAsEnumerableOfNumbers(string day, string fileNameOverride = null);
        IEnumerable<string> LoadInputAsEnumerableOfStrings(string day, string fileName = null);

        List<BitArray> LoadInputAsBitMatrix(string day, string fileName = null, string falseChar = ".", string trueChar = "#");
        string LoadInputAsText(string day, string fileName = null);
    }
}