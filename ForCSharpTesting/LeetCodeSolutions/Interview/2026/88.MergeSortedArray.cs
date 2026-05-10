using ForCSharpTesting.LeetCodeSolutions.Common;

namespace ForCSharpTesting.LeetCodeSolutions.Interview._2026;

public class MergeSortedArray : BaseLeetCodeTask<MergeSortedArrayTestInput, int[]>
{
    public void Merge(int[] nums1, int m, int[] nums2, int n)
    {
        int mainIndex = nums1.Length - 1;
        int nums2Index = n - 1;
        int nums1Index = m - 1;
        
        for (; mainIndex >= 0; mainIndex--)
        {
            bool nums1IndexBiggerThenNums2Index = false;
            if (nums2Index < 0)
            {
                nums1IndexBiggerThenNums2Index = true;
            }
            else if (nums1Index < 0)
            {

            }
            else if (nums1[nums1Index] > nums2[nums2Index])
            {
                nums1IndexBiggerThenNums2Index = true;
            }

            nums1[mainIndex] = nums1IndexBiggerThenNums2Index ? nums1[nums1Index] : nums2[nums2Index];

            if (nums1IndexBiggerThenNums2Index)
            {
                nums1Index--;
            }
            else
            {
                nums2Index--;
            }
        }
    }

    public override IEnumerable<(MergeSortedArrayTestInput Input, int[] Result)> GetTests()
    {
        yield return (new MergeSortedArrayTestInput([1], 1, [], 0), [1]);
        yield return (new MergeSortedArrayTestInput([1, 2, 3, 0, 0, 0], 3, [2, 5, 6], 3), [1, 2, 2, 3, 5, 6]);
    }

    public override (bool Equity, int[] FactResult) RunTest((MergeSortedArrayTestInput Input, int[] Result) test)
    {
        Merge(test.Input.nums1, test.Input.m, test.Input.nums2, test.Input.n);

        return (test.Input.nums1 == test.Result, test.Input.nums1);
    }
}

public record MergeSortedArrayTestInput(int[] nums1, int m, int[] nums2, int n);