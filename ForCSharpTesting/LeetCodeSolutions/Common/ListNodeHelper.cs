
namespace ForCSharpTesting.LeetCodeSolutions.Common
{
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }

    public static class ListNodeHelper
    {
        public static ListNode FillListNodeFromArray(int[] arr)
        {
            var result = new ListNode();
            var currentNode = result;
            ListNode lastNode = null;

            foreach (var item in arr)
            {
                currentNode.val = item;
                currentNode.next = new ListNode();
                lastNode = currentNode;
                currentNode = currentNode.next;
            }

            if (lastNode is not null)
            {
                lastNode.next = null;
            }


            return result;
        }

        public static string PrintListNode(ListNode head)
        {
            if (head.next is null)
            {
                return head.val.ToString();
            }

            return $"{head.val}, {PrintListNode(head.next)}";
        }
    }
}
