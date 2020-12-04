using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Utils
{
    public class BitArrayHelper
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
    }
}
