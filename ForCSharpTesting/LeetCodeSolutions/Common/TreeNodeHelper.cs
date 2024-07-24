namespace ForCSharpTesting.LeetCodeSolutions.Common;

public static class TreeNodeHelper
{
    private const string NullAlias = "null";

    public static string TreeNodeToStringPresentation<T>(TreeNode<T> root)
    {
        var nodeList = new List<string>();
        var nullList = new List<string>();

        var nodeQueue = new Queue<TreeNode<T>>();
        nodeQueue.Enqueue(root);

        nodeList.Add(root.val?.ToString() ?? NullAlias);

        while (nodeQueue.Count > 0)
        {
            var currentNode = nodeQueue.Dequeue();

            if (currentNode.left is not null)
            {
                if (nullList.Any())
                {
                    nodeList.AddRange(nullList);
                    nullList.Clear();
                }

                nodeList.Add(currentNode.left.val?.ToString() ?? NullAlias);
                nodeQueue.Enqueue(currentNode.left);
            }
            else
            {
                nullList.Add(NullAlias);
            }

            if (currentNode.right is not null)
            {
                if (nullList.Any())
                {
                    nodeList.AddRange(nullList);
                    nullList.Clear();
                }

                nodeList.Add(currentNode.right.val?.ToString() ?? NullAlias);
                nodeQueue.Enqueue(currentNode.right);
            }
            else
            {
                nullList.Add(NullAlias);
            }
        }

        return $"[{string.Join(',', nodeList)}]";
    }
}