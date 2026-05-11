using ForCSharpTesting.LeetCodeSolutions.Common;

namespace ForCSharpTesting.LeetCodeSolutions.Interview._2026;

public class LinkedListCycle : BaseLeetCodeTask<ListNode, bool>
{
    public bool HasCycle(ListNode head)
    {
        ListNode slow = head;
        ListNode fast = head;

        while (fast?.next is not null)
        {
            slow = slow.next;
            fast = fast.next.next;

            if (slow == fast)
            {
                return true;
            }
        }

        return false;
    }

    public override IEnumerable<(ListNode Input, bool Result)> GetTests()
    {
        yield break;
    }

    public override (bool Equity, bool FactResult) RunTest((ListNode Input, bool Result) test)
    {
        throw new NotImplementedException();
    }
}
