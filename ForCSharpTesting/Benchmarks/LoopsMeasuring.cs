using BenchmarkDotNet.Attributes;

namespace ForCSharpTesting.Benchmarks;

public class LoopsMeasuring
{
    public int CountLoop { get; set; } = 100000;

    // [Benchmark]
    public void LoopFor()
    {
        var nums = new int[CountLoop];
        for (int i = 0; i < CountLoop; i++)
        {
            nums[i] = i;
        }
    }


    // [Benchmark]
    public void LoopWhile()
    {
        var nums = new int[CountLoop];
        int i = 0;
        while (i < CountLoop)
        {
            nums[i] = i;
            i++;
        }
    }

    // [Benchmark]
    public void LoopDoWhile()
    {
        var nums = new int[CountLoop];
        int i = 0;
        do
        {
            nums[i] = i;
            i++;
        } while (i < CountLoop);
    }
}
