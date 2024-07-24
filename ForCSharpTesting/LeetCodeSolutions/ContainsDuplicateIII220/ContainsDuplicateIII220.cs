namespace ForCSharpTesting.LeetCodeSolutions.ContainsDuplicateIII220;

public class ContainsDuplicateIII220
{
    public bool ContainsNearbyAlmostDuplicate(int[] nums, int indexDiff, int valueDiff, out string output)
    {
        output = string.Empty;

        long currentSum = 0;
        int tempDiff = 0;
        for (int i = 0; i < Math.Min(indexDiff, nums.Length - 1); i++)
        {
            tempDiff = nums[i] - nums[i + 1];
            currentSum += tempDiff;
            if (Math.Abs(tempDiff) <= valueDiff || Math.Abs(currentSum) <= valueDiff)
            {
                return true;
            }
        }

        for (int i = indexDiff; i < nums.Length - 1; i++)
        {
            tempDiff = nums[i] - nums[i + 1];
            currentSum -= nums[i - indexDiff] - nums[i - indexDiff + 1];
            currentSum += tempDiff;
            if (Math.Abs(tempDiff) <= valueDiff || Math.Abs(currentSum) <= valueDiff)
            {
                return true;
            }
        }

        for (int i = Math.Max(0, nums.Length - indexDiff - 2); i < nums.Length - 2; i++)
        {
            tempDiff = nums[i] - nums[i + 1];
            currentSum -= tempDiff;
            if (Math.Abs(tempDiff) <= valueDiff || Math.Abs(currentSum) <= valueDiff)
            {
                return true;
            }
        }
        
        return false;
    }
}