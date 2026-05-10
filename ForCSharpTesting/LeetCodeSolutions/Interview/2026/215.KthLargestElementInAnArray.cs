using ForCSharpTesting.LeetCodeSolutions.Common;

namespace ForCSharpTesting.LeetCodeSolutions.Interview._2026;

public class KthLargestElementInAnArray : BaseLeetCodeTask<KthLargestElementInAnArrayTestInput, int>
{
    public int FindKthLargest(int[] nums, int k)
    {
        var heap = MyHeapMax.FromArray(nums);

        var result = 0;
        for (int i = 0; i < k; i++)
        {
            result = heap.Pop();
        }

        return result;
    }

    public override IEnumerable<(KthLargestElementInAnArrayTestInput Input, int Result)> GetTests()
    {
        yield return (new KthLargestElementInAnArrayTestInput([3, 2, 1, 5, 6, 4], 2), 5);
        yield return (new KthLargestElementInAnArrayTestInput([3, 2, 3, 1, 2, 4, 5, 5, 6], 4), 4);
    }

    public override (bool Equity, int FactResult) RunTest((KthLargestElementInAnArrayTestInput Input, int Result) test)
    {
        var methodResult = FindKthLargest(test.Input.nums, test.Input.k);

        return (methodResult == test.Result, methodResult);
    }
}


public record KthLargestElementInAnArrayTestInput(int[] nums, int k);
