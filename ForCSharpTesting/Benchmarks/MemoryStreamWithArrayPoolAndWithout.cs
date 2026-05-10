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
public class MemoryStreamWithArrayPoolAndWithout
{
    public int BufferLength = 2 * 1024;

    public string FileDirectoryPath = "D:\\Projects\\Adve\\Code\\adve-community-ui\\public\\files";
    public string FilePrefixName = "9MB_";
    public string FileExtension = ".dat";

    [Params(1, 20)]
    public int ThreadCount;

    [Params(100, 1000)]
    public int IterationCount;

    public static void StartBenchmark()
    {
        BenchmarkRunner.Run<MemoryStreamWithArrayPoolAndWithout>();
    }

    [Benchmark]
    public void MemoryStreamWithoutArrayPool()
    {
        for (int i = 0; i < IterationCount; i++)
        {
            var currentFilePath = Path.Combine(FileDirectoryPath, $"{FilePrefixName}{i}{FileExtension}");
            using var readerStream = new FileStream(currentFilePath, FileMode.Open, FileAccess.Read);

            using var memoryStream = new MemoryStream(BufferLength);

            readerStream.CopyTo(memoryStream);
        }
    }
}