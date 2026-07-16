using ForCSharpTesting.MongoDBExploration;

namespace ForCSharpTesting;

public class Program
{
    static async Task Main(string[] args)
    {
        MongoTest.Configure();



        await MongoTest.Test();
    }
}

