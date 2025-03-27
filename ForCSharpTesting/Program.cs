using System.Net.Http.Headers;

namespace ForCSharpTesting;

public class Program
{
    static async Task Main(string[] args)
    {
        await HighLoadTesting.HighLoadTesting.HighLoadFilesMultiThread();
    }
}

