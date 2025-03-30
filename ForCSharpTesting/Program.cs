using System.Net.Http.Headers;

namespace ForCSharpTesting;

public class Program
{
    static async Task Main(string[] args)
    {
        //HighLoadTesting.HighLoadTesting.HighLoadFiles();
        //return;

        await HighLoadTesting.HighLoadTesting.HighLoadFilesMultiThread();
    }
}

