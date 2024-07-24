using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForCSharpTesting.LeetCodeSolutions.Common
{
    public static class ArrayHelper
    {
        public static int[][] GetDoubleArrayFromString(string input)
        {
            var tt = input
                .Replace(" ", string.Empty)
                .Substring(1);
            var subArrays = tt
                .Substring(0, tt.Length - 1)
                .Split("],[");

            int[][] result = new int[subArrays.Length][];
            for (int i = 0; i < subArrays.Length; i++)
            {
                var subSubArray = subArrays[i]
                    .Replace("]", string.Empty)
                    .Replace("[", string.Empty)
                    .Split(',');

                result[i] = new int[subSubArray.Length];
                for (int j = 0; j < subSubArray.Length; j++)
                {
                    if (!int.TryParse(subSubArray[j], out var parseResult))
                    {
                        throw new Exception($"Parsing error: '{subSubArray[j]}' is not a number.");
                    }

                    result[i][j] = parseResult;
                }
            }

            return result;
        }

        public static string GetPrintedArray(int[][] array, bool needSpaceBetweenNumbers)
        {
            string space = needSpaceBetweenNumbers ? " " : string.Empty;

            return $"[[{string.Join($"],{space}[", array.Select(subArray => string.Join($",{space}", subArray)))}]]";
        }
    }
}
