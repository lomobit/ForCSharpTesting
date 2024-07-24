using System.Diagnostics.CodeAnalysis;

namespace ForCSharpTesting.LeetCodeSolutions.Common;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public class TreeNode<T>
{
    public T val;
    public TreeNode<T>? left;
    public TreeNode<T>? right;

    public TreeNode(T val, TreeNode<T>? left = default, TreeNode<T>? right = default)
    {
        this.val = val;
        this.left = left;
        this.right = right;
    }
}
