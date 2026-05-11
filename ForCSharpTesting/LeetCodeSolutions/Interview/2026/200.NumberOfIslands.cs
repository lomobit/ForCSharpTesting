using ForCSharpTesting.LeetCodeSolutions.Common;

namespace ForCSharpTesting.LeetCodeSolutions.Interview._2026;

public class NumberOfIslands : BaseLeetCodeTask<char[][], int>
{
    public int NumIslands(char[][] grid)
    {


        return 0;
    }

    public override IEnumerable<(char[][] Input, int Result)> GetTests()
    {
        yield return (
            [
              ['1','1','1','1','0'],
              ['1','1','0','1','0'],
              ['1','1','0','0','0'],
              ['0','0','0','0','0']
            ],
            1);

        yield return (
            [
              ['1','1','0','0','0'],
              ['1','1','0','0','0'],
              ['0','0','1','0','0'],
              ['0','0','0','1','1']
            ],
            3);
    }

    public override (bool Equity, int FactResult) RunTest((char[][] Input, int Result) test)
    {
        var methodResult = NumIslands(test.Input);

        return (methodResult == test.Result, methodResult);
    }
}
