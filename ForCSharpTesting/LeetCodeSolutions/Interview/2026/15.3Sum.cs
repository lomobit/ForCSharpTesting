using ForCSharpTesting.LeetCodeSolutions.Common;

namespace ForCSharpTesting.LeetCodeSolutions.Interview._2026;

public class Sum3 : BaseLeetCodeTask<Sum3TestInput, Sum3TestResult>
{
    public IList<IList<int>> ThreeSum(int[] nums)
    {

        return [];
    }

    public override (bool Equity, Sum3TestResult FactResult) RunTest((Sum3TestInput Input, Sum3TestResult Result) test)
    {
        var methodResult = ThreeSum(test.Input.Nums);

        return (methodResult == test.Result.Result, new Sum3TestResult(methodResult));
    }

    public override IEnumerable<(Sum3TestInput Input, Sum3TestResult Result)> GetTests()
    {
        yield return (new Sum3TestInput([-1, 0, 1, 2, -1, -4]), new Sum3TestResult([[-1, -1, 2], [-1, 0, 1]]));
    }
}

public record Sum3TestInput(int[] Nums)
{
    public override string ToString()
    {
        return string.Join(", ", Nums);
    }
}
public record Sum3TestResult(IList<IList<int>> Result)
{
    public override string ToString()
    {
        return ArrayHelper.GetPrintedList(Result, true);
    }
}
