using ForCSharpTesting.LeetCodeSolutions.Common;

namespace ForCSharpTesting.LeetCodeSolutions.Interview._2026;

public class MaximumDepthOfBinaryTree : BaseLeetCodeTask<TreeNode<int>, int>
{
    public int MaxDepth(TreeNode<int> root)
    {
        var result = GetMaxDepth(root, 0);

        return result;
    }

    private int GetMaxDepth(TreeNode<int> node, int currentDepth)
    {
        if (node is null) return currentDepth;

        int left = GetMaxDepth(node.left, currentDepth + 1);
        int right = GetMaxDepth(node.right, currentDepth + 1);

        return Math.Max(left, right);
    }

    public override IEnumerable<(TreeNode<int> Input, int Result)> GetTests()
    {
        var root1 = new TreeNodeFactoryStruct<int>("[3,9,20,null,null,15,7]", int.TryParse).Create();
        yield return (root1, 3);

        var root2 = new TreeNodeFactoryStruct<int>("[1,null,2]", int.TryParse).Create();
        yield return (root2, 2);
    }

    public override (bool Equity, int FactResult) RunTest((TreeNode<int> Input, int Result) test)
    {
        var methodResult = MaxDepth(test.Input);
        return (methodResult == test.Result, methodResult);
    }
}
