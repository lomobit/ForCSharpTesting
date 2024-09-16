using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using BenchmarkDotNet.Running;

namespace ForCSharpTesting.Benchmarks;

[MemoryDiagnoser]
[InliningDiagnoser(true, true)]
[TailCallDiagnoser]
[EtwProfiler]
[ConcurrencyVisualizerProfiler]
[NativeMemoryProfiler]
//[ThreadingDiagnoser]
public class CheckReusableArrays
{
    [Params(10000, 20000)]
    public int TestArrayLength;

    [Params(30)]
    public int Iteration;

    public static void StartBenchmark()
    {
        BenchmarkRunner.Run<CheckReusableArrays>();
    }

    [Benchmark]
    public void TryReusableArray()
    {
        const string key = "key";
        const string value = "value";
        var array = new TestClass[TestArrayLength];

        for (int i = 0; i < Iteration; i++)
        {
            for (int j = 0; j < TestArrayLength; j++)
            {
                array[j] = new TestClass(key, j);
            }
        }
    }

    [Benchmark]
    public void TryArray()
    {
        const string key = "key";
        const string value = "value";
        var array = new TestClass[TestArrayLength];

        for (int j = 0; j < TestArrayLength; j++)
        {
            array[j] = new TestClass(key, j);
        }
    }

    struct TestClass
    {
        public string Key { get; set; }
        public decimal Value { get; set; }

        public TestClass(string key, decimal value)
        {
            Key = key;
            Value = value;
        }
    }
}