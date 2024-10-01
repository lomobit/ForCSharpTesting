using ForCSharpTesting.LeetCodeSolutions.Common;

namespace ForCSharpTesting;

public class Program
{
    static void Main(string[] args)
    {
        var k = 2;

        MyCircularDeque obj = new MyCircularDeque(k);
        var param_1 = obj.InsertFront(7);
        var param_2 = obj.DeleteLast();
        var param_3 = obj.GetFront();
        var param_4 = obj.InsertLast(5);
        var param_5 = obj.InsertFront(0);
        var param_6 = obj.GetFront();
        var param_7 = obj.GetRear();
        var param_8 = obj.GetFront();
        var param_9 = obj.GetFront();
        var param_10 = obj.GetRear();
        var param_11 = obj.InsertLast(0);


    }
}