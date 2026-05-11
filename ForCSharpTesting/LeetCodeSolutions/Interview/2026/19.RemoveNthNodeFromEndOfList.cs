using ForCSharpTesting.LeetCodeSolutions.Common;

namespace ForCSharpTesting.LeetCodeSolutions.Interview._2026;

public class RemoveNthNodeFromEndOfList : BaseLeetCodeTask<RemoveNthNodeFromEndOfListTestInput, ListNode>
{
    public ListNode RemoveNthFromEnd(ListNode head, int n)
    {
        ListNode sw = head;
        ListNode ew = head;

        for (int i = 0; i < n; i++)
        {
            ew = ew.next;
        }

        if (ew is null)
        {
            if (head?.next is not null)
            {
                return head.next;
            }

            return null;
        }

        while (ew.next is not null)
        {
            sw = sw.next;
            ew = ew.next;
        }

        sw.next = sw.next.next;

        return head;
    }

    public override IEnumerable<(RemoveNthNodeFromEndOfListTestInput Input, ListNode Result)> GetTests()
    {
        yield return (
            new RemoveNthNodeFromEndOfListTestInput(
                ListNodeHelper.FillListNodeFromArray([1, 2]),
                2),
            ListNodeHelper.FillListNodeFromArray([2]));

        yield return (
            new RemoveNthNodeFromEndOfListTestInput(
                ListNodeHelper.FillListNodeFromArray([1]),
                1),
            ListNodeHelper.FillListNodeFromArray([]));

        yield return (
            new RemoveNthNodeFromEndOfListTestInput(
                ListNodeHelper.FillListNodeFromArray([1, 2, 3, 4, 5]),
                2),
            ListNodeHelper.FillListNodeFromArray([1, 2, 3, 5]));

        yield return (
            new RemoveNthNodeFromEndOfListTestInput(
                ListNodeHelper.FillListNodeFromArray([1, 2]),
                1),
            ListNodeHelper.FillListNodeFromArray([1]));
    }

    public override (bool Equity, ListNode FactResult) RunTest((RemoveNthNodeFromEndOfListTestInput Input, ListNode Result) test)
    {
        var methodResult = RemoveNthFromEnd(test.Input.head, test.Input.n);
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

public record RemoveNthNodeFromEndOfListTestInput(ListNode head, int n);