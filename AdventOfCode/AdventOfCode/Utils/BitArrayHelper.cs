using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Utils
{
    public static class BitArrayHelper
    {
        public static int countHitsBySearchPattern(List<BitArray> bitMatrix, int posColumn = 0, int posLine = 0, int lineMove = 1, int columnMove = 1)
        {
            int hitCount = 0;
            while (posLine < bitMatrix.Count)
            {
                if (posColumn > bitMatrix[posLine].Length - 1)
                {
                    posColumn -= bitMatrix[posLine].Length;
                }
                if (bitMatrix[posLine][posColumn] == true)
                {
                    hitCount++;
                }
                posLine += lineMove;
                posColumn += columnMove;
            }
            return hitCount;
        }

        public static int ParseToInt(this BitArray bitArray, bool lowBitFirst = true)
        {
            if (lowBitFirst)
            {
                var tmp = new bool[bitArray.Count];
                bitArray.CopyTo(tmp, 0);
                Array.Reverse(tmp);
                bitArray = new BitArray(tmp);
            }
            var result = new int[1];
            bitArray.CopyTo(result, 0);
            return result[0];
        }

        public static BitArray[] SplitAtIndex(this BitArray bitArray, int index)
        {
            var firstPart = new bool[index];
            var secondPart = new bool[bitArray.Count - index];

            for (int i = 0; i < firstPart.Length; i++)
            {
                firstPart[i] = bitArray[i];
            }

            for (int i = 0; i < secondPart.Length; i++)
            {
                secondPart[i] = bitArray[index + i];
            }

            return new[] { new BitArray(firstPart), new BitArray(secondPart) };
        }

        public static BitArray ToBitArray(this bool[] boolArray)
        {
            return new BitArray(boolArray);
        }

        public static BitArray FindMissing(this BitArray[] bitArray)
        {
            var lenght = bitArray.First().Count;
            var tmp = new BitArray(lenght, false);

            return bitArray.Aggregate(tmp, (current, array) => array.Xor(current));
        }

        public static BitArray ToBitArray(this string value, char trueChar = '1', char falseChar = '0')
        {
            var arrayHelper = new ArrayHelper();
            return arrayHelper.ParseToBitArray(value, trueChar, falseChar);
        }

        public static BitArray[] ToBitArrayMatrix(this IEnumerable<string> values, char trueChar = '1', char falseChar = '0')
        {
            return values.Select(s => ToBitArray(s, trueChar, falseChar)).ToArray();
        }
    }
}
