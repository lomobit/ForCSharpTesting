using ForCSharpTesting.LeetCodeSolutions.Common;

namespace ForCSharpTesting.LeetCodeSolutions.Interview._2026;

public class BinaryTreeLevelOrderTraversal : BaseLeetCodeTask<TreeNode<int>, IList<IList<int>>>
{
    public IList<IList<int>> LevelOrder(TreeNode<int> root)
    {
        if (root is null)
        {
            return [];
        }

        var result = new List<IList<int>>();
        var queue = new Queue<TreeNode<int>>();

        queue.Enqueue(root);
        int currentLevelNumber = 0;
        
        while (queue.Count > 0)
        {
            int currentLevelCount = queue.Count;
            result.Add(new List<int>());

            for (; currentLevelCount > 0; currentLevelCount--)
            {
                var currentNode = queue.Dequeue();
                result[currentLevelNumber].Add(currentNode.val);

                if (currentNode.left is not null) queue.Enqueue(currentNode.left);
                if (currentNode.right is not null) queue.Enqueue(currentNode.right);
            }

            currentLevelNumber++;
        }

        return result;
    }

    public override IEnumerable<(TreeNode<int> Input, IList<IList<int>> Result)> GetTests()
    {
        var root = new TreeNodeFactoryStruct<int>("[3,9,20,null,null,15,7]", int.TryParse).Create();
        yield return (root, [[3], [9, 20], [15, 7]]);

        root = new TreeNodeFactoryStruct<int>("[1]", int.TryParse).Create();
        yield return (root, [[1]]);

        root = new TreeNodeFactoryStruct<int>("[1,2,3,4,5]", int.TryParse).Create();
        yield return (root, [[1], [2, 3], [4, 5]]);

        yield return (null, []);
    }

    public override (bool Equity, IList<IList<int>> FactResult) RunTest((TreeNode<int> Input, IList<IList<int>> Result) test)
    {
        var methodResult = LevelOrder(test.Input);

        return (ArrayHelper.GetPrintedList(methodResult) == ArrayHelper.GetPrintedList(test.Result), methodResult);
    }

    public override void RunTests()
    {
        var tests = GetTests().ToArray();

        for (int i = 0; i < tests.Length; i++)
        {
            var currentTest = tests[i];
            var (equity, factResult) = RunTest(currentTest);
            Console.Write($"Test {i + 1}: ");

            var colorToUse = equity ? ConsoleColor.Green : ConsoleColor.Red;
            ConsoleHelper.WriteLineColored($"[{equity.ToString().ToUpper()}]", colorToUse);
            ConsoleHelper.WriteColored($"\t {ArrayHelper.GetPrintedList(factResult)}", colorToUse);

            Console.WriteLine($" : {ArrayHelper.GetPrintedList(currentTest.Result)}");
        }
    }
}
