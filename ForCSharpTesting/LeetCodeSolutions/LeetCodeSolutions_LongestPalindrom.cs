namespace ForCSharpTesting.LeetCodeSolutions;

public static partial class LeetCodeSolutions
{
    // 5. Longest Palindromic Substring
    // https://leetcode.com/problems/longest-palindromic-substring/
    
    /// <summary>
    /// Get the longest palindromic substring.
    /// </summary>
    /// <param name="str">Source string.</param>
    /// <returns>The longest palindromic substring</returns>
    public static string GetLongestPalindromeSubstring(string str)
    {
        if (string.IsNullOrEmpty(str) || str.Length == 1)
        {
            return str;
        }
            
        int maxLengthPalindrome = 0;
        int maxLengthPalindromeIndex = 0;

        int currentLengthPalindrome;
            
        for (int i = 0; i < str.Length; i++)
        {
            currentLengthPalindrome = Math.Max(
                MaxPalindromeLengthFromIndexes(str, i, i),
                MaxPalindromeLengthFromIndexes(str, i, i + 1));

            if (currentLengthPalindrome > maxLengthPalindrome)
            {
                maxLengthPalindrome = currentLengthPalindrome;
                maxLengthPalindromeIndex = i - (currentLengthPalindrome - 1) / 2;
            }
        }
            
        return str.Substring(maxLengthPalindromeIndex, maxLengthPalindrome);
    }
    
    /// <summary>
    /// Get the maximal palindrome length from indexes.
    /// </summary>
    /// <param name="str">Source string.</param>
    /// <param name="leftIndex">Left index in source string.</param>
    /// <param name="rightIndex">Right index in source string.</param>
    /// <returns>The maximal palindrome length from indexes.</returns>
    private static int MaxPalindromeLengthFromIndexes(string str, int leftIndex, int rightIndex)
    {
        while (leftIndex >= 0 && rightIndex < str.Length && str[leftIndex] == str[rightIndex])
        {
            leftIndex--;
            rightIndex++;
        }

        return rightIndex - leftIndex - 1;
    }
}