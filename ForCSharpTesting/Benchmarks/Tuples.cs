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
public class Tuples
{
    public static void StartBenchmark()
    {
        BenchmarkRunner.Run<Tuples>();
    }

    [Params(/*500, 1000, */10000)]
    public int TestInputNum;

    [Benchmark]
    public void AnonymousTupleReturn()
    {
        var result = InternalAnonymousTupleReturn(TestInputNum);
    }

    private (int testNum, Guid? testGuid) InternalAnonymousTupleReturn(int inputNum)
    {
        return (inputNum * 2, Guid.NewGuid());
    }

    [Benchmark]
    public void ValueTupleReturn()
    {
        var result = InternalValueTupleReturn(TestInputNum);
    }

    private ValueTuple<int, Guid?> InternalValueTupleReturn(int inputNum)
    {
        return (inputNum * 2, Guid.NewGuid());
    }

    [Benchmark]
    public void TupleReturn()
    {
        var result = InternalTupleReturn(TestInputNum);
    }

    private Tuple<int, Guid?> InternalTupleReturn(int inputNum)
    {
        return new Tuple<int, Guid?>(inputNum * 2, Guid.NewGuid());
    }

    private record ReturnDto(int Number, Guid? Guid);

    [Benchmark]
    public void DtoReturn()
    {
        var result = InternalTupleReturn(TestInputNum);
    }

    private ReturnDto InternalDtoReturn(int inputNum)
    {
        return new ReturnDto(inputNum * 2, Guid.NewGuid());
    }
}