using ForCSharpTesting.LeetCodeSolutions.Common;
using Microsoft.Diagnostics.Runtime;

namespace ForCSharpTesting.LeetCodeSolutions.Interview._2026;

public class LongestSubstringWithoutRepeatingCharacters : BaseLeetCodeTask<string, int>
{
    public int LengthOfLongestSubstring(string str)
    {
        int maxSymb = 0;

        int fi = 0;
        int si = 0;

        Span<int> lastSeenAt = stackalloc int[128];

        while (si < str.Length)
        {
            var currentChar = str[si];
            var currentWindowSize = si - fi;

            var currentCharLastTimeSeenAtIndex = lastSeenAt[currentChar];

            lastSeenAt[currentChar] = si + 1;

            var wasSeenLastTimeInsideWindow = currentCharLastTimeSeenAtIndex > 0 && si - currentWindowSize <= currentCharLastTimeSeenAtIndex;
            if (wasSeenLastTimeInsideWindow)
            {
                fi = currentCharLastTimeSeenAtIndex;
            }

            si++;
            
            if (si - fi > maxSymb) maxSymb = si - fi;
        }

        return maxSymb;
    }

    public override IEnumerable<(string Input, int Result)> GetTests()
    {
        yield return ("jbpnbwwd", 4);


        yield return ("abcabcbb", 3);

        yield return ("dvdf", 3);
        yield return ("au", 2);
        
        yield return ("bbbbb", 1);
        yield return ("pwwkew", 3);
        yield return ("", 0);
    }

    public override (bool Equity, int FactResult) RunTest((string Input, int Result) test)
    {
        var methodResult = LengthOfLongestSubstring(test.Input);

        return (methodResult == test.Result, methodResult);
    }
}
