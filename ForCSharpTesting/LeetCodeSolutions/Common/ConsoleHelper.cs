namespace ForCSharpTesting.LeetCodeSolutions.Common
{
    public static class ConsoleHelper
    {
        public static void WriteLineColored(string text, ConsoleColor color)
        {
            var currentColor = Console.BackgroundColor;
            Console.BackgroundColor = color;
            Console.WriteLine(text);
            Console.BackgroundColor = currentColor;
        }
    }
}
