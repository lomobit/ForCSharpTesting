using System.ComponentModel.DataAnnotations;

namespace ForCSharpTesting.LeetCodeSolutions.Common;

/// <summary>
/// Фабрика для создания деревьев
/// </summary>
/// <typeparam name="TStruct">Тип значения (структура) для листьев дерева.</typeparam>
public class TreeNodeFactoryStruct<TStruct> : ITreeNodeFactory<TStruct>
    where TStruct : struct
{
    TStruct?[] NodeValues { get; set; }

    public TreeNodeFactoryStruct(TStruct?[] nodeValues)
    {
        NodeValues = nodeValues;
    }

    public TreeNodeFactoryStruct(string nodeValuesInString, TryParse<TStruct> parser)
    {
        // Ожидается, что nodeValuesInString будет содержать в себе массив элементов через запятую, обрамленных квадратными скобками
        //   Примеры для int: "[1,2,3,null,4,null,5]", "[1,2,3]"

        TStruct? NodeValuesParser(string s)
        {
            var trimmedString = s.Trim();
            return trimmedString == "null"
                ? null
                : parser(trimmedString, out var result)
                    ? result
                    : throw new ValidationException(
                        $"TreeNode value '{trimmedString}' is not valid for type {typeof(TStruct).FullName}");
        }

        NodeValues = nodeValuesInString
            .Replace("[", string.Empty)
            .Replace("]", string.Empty)
            .Split(',')
            .Select(NodeValuesParser)
            .ToArray();
    }

    public TreeNode<TStruct> Create()
    {
        if (!NodeValues.Any() || !NodeValues[0].HasValue)
        {
            throw new ValidationException($"There is an empty array for TreeNode values");
        }

        var root = new TreeNode<TStruct>(NodeValues[0]!.Value);

        var nodeQueue = new Queue<TreeNode<TStruct>>();
        nodeQueue.Enqueue(root);

        for (int i = 1; i < NodeValues.Length; i++)
        {
            var currentNode = nodeQueue.Dequeue();

            if (NodeValues[i].HasValue)
            {
                currentNode.left = new TreeNode<TStruct>(NodeValues[i]!.Value);
                nodeQueue.Enqueue(currentNode.left);
            }

            i++;
            if (i < NodeValues.Length && NodeValues[i].HasValue)
            {
                currentNode.right = new TreeNode<TStruct>(NodeValues[i]!.Value);
                nodeQueue.Enqueue(currentNode.right);
            }
        }

        nodeQueue.Clear();

        return root;
    }
}
