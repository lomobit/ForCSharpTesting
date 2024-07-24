using Xunit;
using Xunit.Abstractions;

namespace ForCSharpTesting_Tests.RegularExpressionMatching10;

public class RegularExpressionMatching10Tests
{
    private readonly ForCSharpTesting.LeetCodeSolutions.RegularExpressionMatching10.RegularExpressionMatching10 _testObject = new();
    private readonly ITestOutputHelper _output;

    public RegularExpressionMatching10Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Theory]
    [InlineData("aa", "a", false)]
    [InlineData("aa", "a*", true)]
    [InlineData("ab", ".*", true)]
    [InlineData("ababababababccc", "abab.*c.*c*.", true)]
    [InlineData("abababa", ".*aba", true)]
    [InlineData("mississippi", "mis*is*p*.", false)]
    [InlineData("aab", "c*a*b", true)]
    [InlineData("a", "ab*", true)]
    public void CommonTest(string @string, string pattern, bool expectedResult)
    {
        var result = _testObject.IsMatch(@string, pattern, out var output);
        _output.WriteLine(output);
        
        Assert.Equal(expectedResult, result);
    }
}