using BenchmarkDotNet.Running;

namespace ForCSharpTesting;

public class Program
{
    static void Main(string[] args)
    {
        BenchmarkRunner.Run(typeof(Program).Assembly);
    }
}