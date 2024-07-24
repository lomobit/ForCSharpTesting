using System.Text;
using ForCSharpTesting.LeetCodeSolutions.Common;

namespace ForCSharpTesting.LeetCodeSolutions;

public static partial class LeetCodeSolutions
{
    // 988. Smallest String Starting From Leaf
    // https://leetcode.com/problems/smallest-string-starting-from-leaf/


    public static string SmallestFromLeaf(TreeNode<int> node)
    {
        return SmallestFromChildNodes(node, string.Empty);
    }

    public static string SmallestFromChildNodes(TreeNode<int> node, string parentPrefix)
    {
        const int firstAlphabetIndex = 'a';

        string currentPrefix = $"{(char)(firstAlphabetIndex + node.val)}{parentPrefix}";

        // Нода на последнем уровне
        if (node.left is null && node.right is null)
        {
            return currentPrefix;
        }

        string leftResult = string.Empty;
        if (node.left is not null)
        {
            leftResult = SmallestFromChildNodes(node.left, currentPrefix);
        }

        string rightResult = string.Empty;
        if (node.right is not null)
        {
            rightResult = SmallestFromChildNodes(node.right, currentPrefix);
        }

        if (node.left is not null && node.right is not null)
        {
            var compareResult = string.Compare(leftResult, rightResult, StringComparison.CurrentCulture);
            if (compareResult < 0)
            {
                return leftResult;
            }

            return rightResult;
        }

        if (node.left is not null)
        {
            return leftResult;
        }

        return rightResult;
    }
}