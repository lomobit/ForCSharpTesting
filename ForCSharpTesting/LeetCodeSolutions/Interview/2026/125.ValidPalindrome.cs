using Microsoft.Diagnostics.Runtime;

namespace ForCSharpTesting.LeetCodeSolutions.Interview._2026;

public static class ValidPalindrome
{
    public static bool IsPalindrome(string str)
    {
        var input = str.Trim().Where(char.IsLetterOrDigit).Select(char.ToLower).ToArray();

        if (input.Length == 0)
        {
            return true;
        }

        int fi = 0;
        int si = input.Length - 1;

        while (fi < si)
        {
            if (input[fi] != input[si])
            {
                return false;
            }


            fi++;
            si--;
        }

        return true;
    }
}
