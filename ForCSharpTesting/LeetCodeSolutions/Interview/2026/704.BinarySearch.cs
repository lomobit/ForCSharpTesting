using ForCSharpTesting.LeetCodeSolutions.Common;

namespace ForCSharpTesting.LeetCodeSolutions.Interview._2026;

public class BinarySearch : BaseLeetCodeTask<BinarySerachTestInput, int>
{
    public int Search(int[] nums, int target)
    {
        int fi = 0;
        int si = nums.Length - 1;
        
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

    public override IEnumerable<(BinarySerachTestInput Input, int Result)> GetTests()
    {
        yield return (new BinarySerachTestInput([2, 5], 5), 1);
        yield return (new BinarySerachTestInput([5], 5), 0);
        yield return (new BinarySerachTestInput([-1, 0, 3, 5, 9, 12], 2), -1);
        yield return (new BinarySerachTestInput([-1, 0, 3, 5, 9, 12], 9), 4);
    }

    public override (bool Equity, int FactResult) RunTest((BinarySerachTestInput Input, int Result) test)
    {
        var methodResult = Search(test.Input.Nums, test.Input.Target);

        return (methodResult == test.Result, methodResult);
    }
}

public record BinarySerachTestInput(int[] Nums, int Target);