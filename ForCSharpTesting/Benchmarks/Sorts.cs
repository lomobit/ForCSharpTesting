using BenchmarkDotNet.Attributes;

namespace ForCSharpTesting.Benchmarks;

public class Sorts
{
    //[Params(10_000, 100_000, 1_000_000)]
    public int ItemsLength;

    private int[] Data;

    //[GlobalSetup]
    public void GlobalSetup()
    {
        Data = new int[ItemsLength];
        
        var j = ItemsLength;
        for (int i = 0; i < ItemsLength; i++)
        {
            Data[i] = j--;
        }
    }

    //[Benchmark]
    public void ArraySort()
    {
        Array.Sort(Data);
    }


    //[Benchmark]
    public void SpanSort()
    {
        var span = Data.AsSpan();
        span.Sort();
    }
}