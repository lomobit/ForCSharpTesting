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
public class CheckReusableMetrics
{
    [Params(10000, 20000)]
    public int TestArrayLength;

    [Params(10)]
    public int TestMetricLabelsMaxLength;

    [Params(30)]
    public int Iteration;

    public static void StartBenchmark()
    {
        BenchmarkRunner.Run<CheckReusableArrays>();
    }

    [Benchmark]
    public void TryReusableArrays()
    {
        // Аллокация памяти для переиспользолвания.
        var metrics = new TestMetric[TestArrayLength];
        var labelsForMetrics = new List<TestLabel>[TestArrayLength];
        for (int i = 0; i < TestArrayLength; i++)
        {
            labelsForMetrics[i] = new List<TestLabel>(TestMetricLabelsMaxLength);
        }


        // Использование аллоцированной выше памяти.
        for (int i = 0; i < Iteration; i++)
        {
            for (int j = 0; j < TestArrayLength; j++)
            {
                var currentLabelsArray = labelsForMetrics[j];
                currentLabelsArray.Clear();

                currentLabelsArray.Add(new TestLabel("MaxDuration"));
                currentLabelsArray.Add(new TestLabel("MinDuration"));
                currentLabelsArray.Add(new TestLabel("AvgDuration"));
                currentLabelsArray.Add(new TestLabel("CountsPerDay"));

                metrics[j] = new TestMetric(j, currentLabelsArray);
            }
        }
    }

    [Benchmark]
    public void TryArrays()
    {
        var metrics = new TestMetric[TestArrayLength];
        var labelsForMetrics = new List<TestLabel>[TestArrayLength];
        for (int i = 0; i < TestArrayLength; i++)
        {
            labelsForMetrics[i] = new List<TestLabel>(TestMetricLabelsMaxLength);
        }

        for (int j = 0; j < TestArrayLength; j++)
        {
            var currentLabelsArray = labelsForMetrics[j];
            currentLabelsArray.Clear();

            currentLabelsArray.Add(new TestLabel("MaxDuration"));
            currentLabelsArray.Add(new TestLabel("MinDuration"));
            currentLabelsArray.Add(new TestLabel("AvgDuration"));
            currentLabelsArray.Add(new TestLabel("CountsPerDay"));

            metrics[j] = new TestMetric(j, currentLabelsArray);
        }
    }

    struct TestMetric
    {
        public string Name => nameof(TestMetric);
        public decimal Value { get; set; }
        public List<TestLabel> Labels { get; set; }

        public TestMetric(decimal value, List<TestLabel> labels)
        {
            Value = value;
            Labels = labels;
        }

    }

    struct TestLabel
    {
        public string Name => nameof(TestLabel);
        public string Value { get; set; }

        public TestLabel(string value)
        {
            Value = value;
        }
    }
}