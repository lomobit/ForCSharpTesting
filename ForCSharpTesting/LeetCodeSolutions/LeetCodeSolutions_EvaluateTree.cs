using ForCSharpTesting.LeetCodeSolutions.Common;

namespace ForCSharpTesting.LeetCodeSolutions;

public static partial class LeetCodeSolutions
{
    public static bool EvaluateTree(TreeNode<int> root)
    {
        if (root.val == 0) return false;
        if (root.val == 1) return true;

        // or
        if (root.val == 2)
        {
            return EvaluateTree(root.left) || EvaluateTree(root.right);
        }

        // and
        if (root.val == 3)
        {
            return EvaluateTree(root.left) && EvaluateTree(root.right);
        }

        throw new Exception("Are you shure about your test???");
    }
}