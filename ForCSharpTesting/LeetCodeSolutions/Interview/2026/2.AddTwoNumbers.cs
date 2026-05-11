using ForCSharpTesting.LeetCodeSolutions.Common;

namespace ForCSharpTesting.LeetCodeSolutions.Interview._2026;

public class AddTwoNumbersClass : BaseLeetCodeTask<AddTwoNumbersTestInput, ListNode>
{

    public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
    {
        var result = new ListNode();

        var currentNode = result;
        int extraNum = 0;

        ListNode prev = null;

        while (l1 is not null || l2 is not null || extraNum > 0)
        {
            int currentResult = (l1?.val ?? 0) + (l2?.val ?? 0) + extraNum;
            if (currentResult > 9)
            {
                extraNum = 1;
                currentResult -= 10;
            }
            else
            {
                extraNum = 0;
            }

            currentNode.val = currentResult;

            l1 = l1?.next;
            l2 = l2?.next;

            currentNode.next = new ListNode();
            prev = currentNode;
            currentNode = currentNode.next;
        }

        if (prev is not null)
        {
            prev.next = null;
        }

        return result;
    }

    public override IEnumerable<(AddTwoNumbersTestInput Input, ListNode Result)> GetTests()
    {
        yield return (
            new AddTwoNumbersTestInput(
                ListNodeHelper.FillListNodeFromArray([9, 9, 9, 9, 9, 9, 9]),
                ListNodeHelper.FillListNodeFromArray([9, 9, 9, 9])),
            ListNodeHelper.FillListNodeFromArray([8, 9, 9, 9, 0, 0, 0, 1]));

        yield return (
            new AddTwoNumbersTestInput(
                ListNodeHelper.FillListNodeFromArray([2, 4, 3]),
                ListNodeHelper.FillListNodeFromArray([5, 6, 4])),
            ListNodeHelper.FillListNodeFromArray([7, 0, 8]));

        yield return (
            new AddTwoNumbersTestInput(
                ListNodeHelper.FillListNodeFromArray([0]),
                ListNodeHelper.FillListNodeFromArray([0])),
            ListNodeHelper.FillListNodeFromArray([0]));
    }

    public override (bool Equity, ListNode FactResult) RunTest((AddTwoNumbersTestInput Input, ListNode Result) test)
    {
        var methodResult = AddTwoNumbers(test.Input.l1, test.Input.l2);
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


public record AddTwoNumbersTestInput(ListNode l1, ListNode l2);