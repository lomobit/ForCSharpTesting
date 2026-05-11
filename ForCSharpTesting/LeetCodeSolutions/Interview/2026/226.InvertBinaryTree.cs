using ForCSharpTesting.LeetCodeSolutions.Common;

namespace ForCSharpTesting.LeetCodeSolutions.Interview._2026;

public class InvertBinaryTree : BaseLeetCodeTask<TreeNode<int>, TreeNode<int>>
{
    public TreeNode<int> InvertTree(TreeNode<int> root)
    {
        InvertNode(root);

        return root;
    }

    public void InvertNode(TreeNode<int> node)
    {
        if (node is null)
        {
            return;
        }

        (node.left, node.right) = (node.right, node.left);
        InvertNode(node.left);
        InvertNode(node.right);
    }

    public override IEnumerable<(TreeNode<int> Input, TreeNode<int> Result)> GetTests()
    {
        var factory11 = new TreeNodeFactoryStruct<int>("[4,2,7,1,3,6,9]", int.TryParse);
        var root1 = factory11.Create();
        var factory12 = new TreeNodeFactoryStruct<int>("[4,7,2,9,6,3,1]", int.TryParse);
        var result1 = factory12.Create();

        yield return (root1, result1);

        var factory21 = new TreeNodeFactoryStruct<int>("[2,1,3]", int.TryParse);
        var root2 = factory21.Create();
        var factory22 = new TreeNodeFactoryStruct<int>("[2,3,1]", int.TryParse);
        var result2 = factory22.Create();

        yield return (root2, result2);

        yield return (null, null);
    }

    public override (bool Equity, TreeNode<int> FactResult) RunTest((TreeNode<int> Input, TreeNode<int> Result) test)
    {
        var methodResult = InvertTree(test.Input);
        return (TreeNodeHelper.TreeNodeToStringPresentation(methodResult) == TreeNodeHelper.TreeNodeToStringPresentation(test.Result), methodResult);
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
            ConsoleHelper.WriteColored($"\t {TreeNodeHelper.TreeNodeToStringPresentation(factResult)}", colorToUse);

            Console.WriteLine($" : {TreeNodeHelper.TreeNodeToStringPresentation(currentTest.Result)}");
        }
    }
}
