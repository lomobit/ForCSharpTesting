using Microsoft.Diagnostics.Runtime;

namespace ForCSharpTesting.LeetCodeSolutions.Interview._2026;

public static class TwoSumClass
{
    public static int[] TwoSum(int[] nums, int target)
    {
        var dict = new Dictionary<int, int>();
        for (int i = 0; i < nums.Length; i++)
        {
            var num = nums[i];
            var complement = target - num;

            if (dict.ContainsKey(complement))
            {
                return [dict[complement], i];
            }

            dict.Add(num, i);
        }

        return Array.Empty<int>();
    }
}
