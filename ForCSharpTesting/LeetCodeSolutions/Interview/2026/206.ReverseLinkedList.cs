using ForCSharpTesting.LeetCodeSolutions.Common;
using static System.Net.Mime.MediaTypeNames;

namespace ForCSharpTesting.LeetCodeSolutions.Interview._2026;

public class ReverseLinkedList : BaseLeetCodeTask<ListNode, ListNode>
{
    public ListNode ReverseList(ListNode head)
    {
        if (head?.next == null)
        {
            return head;
        }

        var prevNode = head;
        var currentNode = head.next;
        var nextNode = currentNode.next;

        prevNode.next = null;

        while (currentNode is not null)
        {
            nextNode = currentNode.next;

            currentNode.next = prevNode;

            prevNode = currentNode;
            currentNode = nextNode;
        } 

        return prevNode;
    }

    public override IEnumerable<(ListNode Input, ListNode Result)> GetTests()
    {
        yield return (
            ListNodeHelper.FillListNodeFromArray([1, 2, 3, 4, 5]),
            ListNodeHelper.FillListNodeFromArray([5, 4, 3, 2, 1]));

        yield return (
            ListNodeHelper.FillListNodeFromArray([1, 2]),
            ListNodeHelper.FillListNodeFromArray([2, 1]));

        yield return (
            ListNodeHelper.FillListNodeFromArray([1]),
            ListNodeHelper.FillListNodeFromArray([1]));

        yield return (
            ListNodeHelper.FillListNodeFromArray([]),
            ListNodeHelper.FillListNodeFromArray([]));
    }

    public override (bool Equity, ListNode FactResult) RunTest((ListNode Input, ListNode Result) test)
    {
        var methodResult = ReverseList(test.Input);
        return (ListNodeHelper.PrintListNode(methodResult) == ListNodeHelper.PrintListNode(test.Result), methodResult);
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
            ConsoleHelper.WriteColored($"\t [{ListNodeHelper.PrintListNode(factResult)}]", colorToUse);

            Console.WriteLine($" : [{ListNodeHelper.PrintListNode(currentTest.Result)}]");
        }
    }
}
