namespace ForCSharpTesting.LeetCodeSolutions;

public static partial class LeetCodeSolutions
{
    // 205. Isomorphic Strings
    // https://leetcode.com/problems/isomorphic-strings/
    
    /// <summary>
    /// Determine if they are isomorphic.
    /// </summary>
    /// <param name="str1">First string.</param>
    /// <param name="str2">Second string.</param>
    public static bool IsIsomorphic(string str1, string str2)
    {
        var dict1 = new Dictionary<char, int>();
        var dict2 = new Dictionary<char, int>();

        for (int i = 0; i < str1.Length; i++)
        {
            if (dict1.ContainsKey(str1[i]) != dict2.ContainsKey(str2[i]))
            {
                return false;
            }

            if (dict1.ContainsKey(str1[i]))
            {
                if (dict1[str1[i]] != dict2[str2[i]])
                {
                    return false;
                }
                
                dict1[str1[i]] = i;
                dict2[str2[i]] = i;
            }
            else
            {
                dict1.Add(str1[i], i);
                dict2.Add(str2[i], i);
            }
        }
        
        return true;
    }
}