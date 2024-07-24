using ForCSharpTesting.LeetCodeSolutions;
using ForCSharpTesting.LeetCodeSolutions.Common;
using Xunit;
using Xunit.Abstractions;

namespace ForCSharpTesting_Tests;

public class LeetCodeSolutionsTests
{
    private readonly ITestOutputHelper _output;

    public LeetCodeSolutionsTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Theory]
    [InlineData("[0,1,2,3,4,3,4]", "dba")]
    [InlineData("[25,1,3,1,3,0,2]", "adz")]
    [InlineData("[2,2,1,null,1,0,null,0]", "abc")]
    [InlineData("[4,0,1,1]", "bae")]
    [InlineData("[25,1,null,0,0,1,null,null,null,0]", "ababz")]
    public void SmallestFromLeafTest(string treeInput, string result)
    {
        var factory = new TreeNodeFactoryStruct<int>(treeInput, int.TryParse);
        var root = factory.Create();

        var codeResult = LeetCodeSolutions.SmallestFromLeaf(root);
        _output.WriteLine(codeResult);

        Assert.Equal(result, codeResult);
    }

    [Theory]
    [InlineData(7)]
    public void LargestLocalTest(int a)
    {
        _output.WriteLine(a.ToString());
    }
}