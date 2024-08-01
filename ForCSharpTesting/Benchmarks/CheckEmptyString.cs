using BenchmarkDotNet.Attributes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ForCSharpTesting.Benchmarks;

public class CheckEmptyString
{
    [Params(null, "", " ", "Turbocharged: Writing High-Performance C# and .NET Code - Steve Gordon - NDC Oslo 2024")]
    public string? StringForCheck;

    [Benchmark]
    public void StringIsNullOrWhiteSpace()
    {
        var result = string.IsNullOrWhiteSpace(StringForCheck);
    }


    [Benchmark]
    public void MemoryExtensionsIsWhiteSpace()
    {
        var length = StringForCheck?.Length ?? 0;
        var result = length == 0 || MemoryExtensions.IsWhiteSpace(StringForCheck);
    }
}