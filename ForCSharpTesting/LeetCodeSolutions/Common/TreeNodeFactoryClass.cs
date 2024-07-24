using System.ComponentModel.DataAnnotations;

namespace ForCSharpTesting.LeetCodeSolutions.Common;

/// <summary>
/// Фабрика для создания деревьев
/// </summary>
/// <typeparam name="TClass">Тип значения (класс) для листьев дерева.</typeparam>
public class TreeNodeFactoryClass<TClass> : ITreeNodeFactory<TClass>
    where TClass : class
{
    TClass?[] NodeValues { get; set; }

    public TreeNodeFactoryClass(TClass?[] nodeValues)
    {
        NodeValues = nodeValues;
    }

    public TreeNodeFactoryClass(string nodeValuesInString, TryParse<TClass> parser)
    {
        // Ожидается, что nodeValuesInString будет содержать в себе массив элементов через запятую, обрамленных квадратными скобками
        //   Примеры для int: "[1,2,3,null,4,null,5]", "[1,2,3]"

        TClass? NodeValuesParser(string s)
        {
            var trimmedString = s.Trim();
            return trimmedString == "null"
                ? null
                : parser(trimmedString, out var result)
                    ? result
                    : throw new ValidationException(
                        $"TreeNode value '{trimmedString}' is not valid for type {typeof(TClass).FullName}");
        }

        NodeValues = nodeValuesInString
            .Replace("[", string.Empty)
            .Replace("]", string.Empty)
            .Split(',')
            .Select(NodeValuesParser)
            .ToArray();
    }

    public TreeNode<TClass> Create()
    {
        if (!NodeValues.Any() || NodeValues[0] is null)
        {
            throw new ValidationException($"There is an empty array for TreeNode values");
        }

        var root = new TreeNode<TClass>(NodeValues[0]!);

        var nodeQueue = new Queue<TreeNode<TClass>>();
        nodeQueue.Enqueue(root);

        for (int i = 1; i < NodeValues.Length; i++)
        {
            var currentNode = nodeQueue.Dequeue();

            if (NodeValues[i] is not null)
            {
                currentNode.left = new TreeNode<TClass>(NodeValues[i]!);
                nodeQueue.Enqueue(currentNode.left);
            }

            i++;
            if (i < NodeValues.Length && NodeValues[i] is not null)
            {
                currentNode.right = new TreeNode<TClass>(NodeValues[i]!);
                nodeQueue.Enqueue(currentNode.right);
            }
        }

        nodeQueue.Clear();

        return root;
    }
}
