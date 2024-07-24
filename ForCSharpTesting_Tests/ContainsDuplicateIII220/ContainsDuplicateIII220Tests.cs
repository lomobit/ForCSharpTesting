using Xunit;
using Xunit.Abstractions;

namespace ForCSharpTesting_Tests.ContainsDuplicateIII220;

public class ContainsDuplicateIII220Tests
{
    private readonly ForCSharpTesting.LeetCodeSolutions.ContainsDuplicateIII220.ContainsDuplicateIII220 _testObject =
        new();

    private readonly ITestOutputHelper _output;

    public ContainsDuplicateIII220Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Theory]
    [InlineData(new int[] { 1, 2, 3, 1 }, 3, 0, true)]
    [InlineData(new int[] { 1, 5, 9, 1, 5, 9 }, 2, 3, false)]
    [InlineData(new int[] { 1, 2, 2, 3, 4, 5 }, 3, 0, true)]
    [InlineData(new int[] { -3, 3 }, 2, 4, false)]
    [InlineData(new int[] { 0, 1, 2, 3, 4, 1 }, 5, 0, true)]
    [InlineData(new int[] { 0, 4, 2, 3, 1, 1 }, 5, 0, true)]
    public void CommonTest(int[] nums, int indexDiff, int valueDiff, bool expectedResult)
    {
        var result = _testObject.ContainsNearbyAlmostDuplicate(nums, indexDiff, valueDiff, out var output);
        _output.WriteLine(output);

        Assert.Equal(expectedResult, result);
    }
}