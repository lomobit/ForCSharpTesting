using BenchmarkDotNet.Running;
using ForCSharpTesting.Benchmarks;
using Serilog;

namespace ForCSharpTesting;

public class Program
{
    static void Main(string[] args)
    {
        CheckReusableArrays.StartBenchmark();
    }
}