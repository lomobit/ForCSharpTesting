using ForCSharpTesting.LeetCodeSolutions.Common;

namespace ForCSharpTesting.LeetCodeSolutions.Interview._2026;

public class SearchInRotatedSortedArray : BaseLeetCodeTask<SearchInRotatedSortedArrayTestInput, int>
{
    public (bool isLeftRotated, int fi, int si, int fi2, int si2) GetInitialIndexes(int[] nums)
    {
        if (nums.Length == 1 || nums[nums.Length - 1] > nums[0])
        {
            return (false, 0, nums.Length - 1, -1, -1);
        }

        int fi = 0;
        int si = nums.Length - 1;

        while (fi <= si)
        {
            int currentIndex = (si - fi) / 2 + fi;
            if (nums[currentIndex] > nums[currentIndex + 1])
            {
                return (true, 0, currentIndex, currentIndex + 1, nums.Length - 1);
            }
            else if (nums[currentIndex] > nums[0] &&  nums[currentIndex] < nums[currentIndex + 1])
            {
                if (fi == currentIndex) fi++;
                else fi = currentIndex;
            }
            else if (nums[currentIndex] < nums[0] && nums[currentIndex] < nums[currentIndex + 1])
            {
                if (si == currentIndex) si--;
                else si = currentIndex;
            }
        }

        return (false, 0, nums.Length - 1, -1, -1);
    }

    public int Search(int[] nums, int target)
    {
        var (isLeftRotated, prefi, presi, prefi2, presi2) = GetInitialIndexes(nums);

        int fi = 0;
        int si = 0;
        if (target >= nums[prefi])
        {
            fi = prefi;
            si = presi;
        }
        else if (presi2 > 0 && target <= nums[presi2])
        {
            fi = prefi2;
            si = presi2;
        }
        else
        {
            return -1;
        }

        while (fi <= si)
        {
            int currentIndex = (si - fi) / 2 + fi;
            if (nums[currentIndex] == target)
            {
                return currentIndex;
            }
            else if (nums[currentIndex] < target)
            {
                if (fi == currentIndex) fi++;
                else fi = currentIndex;
            }
            else
            {
                if (si == currentIndex) si--;
                else si = currentIndex;
            }
        }

        return -1;
    }

    public override IEnumerable<(SearchInRotatedSortedArrayTestInput Input, int Result)> GetTests()
    {
        yield return (new SearchInRotatedSortedArrayTestInput([1], 0), -1);
        yield return (new SearchInRotatedSortedArrayTestInput([4, 5, 6, 7, 0, 1, 2], 0), 4);
        yield return (new SearchInRotatedSortedArrayTestInput([4, 5, 6, 7, 0, 1, 2], 3), -1);
        yield return (new SearchInRotatedSortedArrayTestInput([2, 1], 1), 1);
        yield return (new SearchInRotatedSortedArrayTestInput([1, 2, 0], 0), 2);
        
    }

    public override (bool Equity, int FactResult) RunTest((SearchInRotatedSortedArrayTestInput Input, int Result) test)
    {
        var methodResult = Search(test.Input.nums, test.Input.target);

        return (methodResult == test.Result, methodResult);
    }
}


public record SearchInRotatedSortedArrayTestInput(int[] nums, int target);