namespace ForCSharpTesting.LeetCodeSolutions.Common;

public abstract class BaseLeetCodeTask<TestInput, TestResult>
{
    public abstract IEnumerable<(TestInput Input, TestResult Result)> GetTests();
    
    public abstract (bool Equity, TestResult FactResult) RunTest((TestInput Input, TestResult Result) test);

    public virtual void RunTests()
    {
        var tests = GetTests().ToArray();

        for (int i = 0; i < tests.Length; i++)
        {
            var currentTest = tests[i];
            var (equity, factResult) = RunTest(currentTest);
            Console.Write($"Test {i + 1}: ");

            var colorToUse = equity ? ConsoleColor.Green : ConsoleColor.Red;
            ConsoleHelper.WriteLineColored($"[{equity.ToString().ToUpper()}]", colorToUse);
            ConsoleHelper.WriteColored($"\t {factResult}", colorToUse);

            Console.WriteLine($" : {currentTest.Result}");
        }
    }
}
