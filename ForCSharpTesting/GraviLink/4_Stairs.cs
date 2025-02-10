using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForCSharpTesting.GraviLink
{
    public static class _4_Stairs
    {
        static void RunTestsFromFiles()
        {
            string[] filesPathes = [
                "C:\\Users\\vghslnk\\OneDrive\\Рабочий стол\\4_1.txt",
                "C:\\Users\\vghslnk\\OneDrive\\Рабочий стол\\4_2.txt"
            ];

            foreach (var path in filesPathes)
            {
                var input = GetInputForTest(path);
                var result = Stairs(input.nums, input.A);
                var badResult = StairsWithBug(input.nums, input.A);

                Console.WriteLine($"result: {result} | badResult: {badResult} | path: {path}");
            }
        }

        static (List<int> nums, int A) GetInputForTest(string filePath)
        {
            var lines = File.ReadAllLines(filePath);

            var A = int.Parse(lines[1].Trim());
            var nums = lines[2].Trim().Split(" ").Select(int.Parse).ToList();

            return (nums, A);
        }

        static void RunTests()
        {
            StairsTest([1, 2], 2, 1);
            StairsTest([1, 2, 1, 2], 2, 2);
            StairsTest([0, 1, 2, 0, 1, 0, 1, 2], 2, 2);
            StairsTest([1, 1, 2, 1, 1, 2, 3, 2], 2, 2);
            StairsTest([1, 1, 2, 1, 1, 2, 3, 2], 3, 1);
            StairsTest([1, 2, 3, 1, 1, 2, 2, 3, 3], 3, 1);
            StairsTest([1, 2, 3, 1, 1, 2, 3, 2, 3], 3, 2);
        }

        static int StairsWithBug(List<int> nums, int A)
        {
            int answer = 0;
            int n = nums.Count;

            for (int i = 0; i < n; ++i)
            {
                for (int j = i; j < n; ++j)
                {
                    if (nums[j] != j - i + 1)
                    {
                        if (j - i == A)
                        {
                            answer++;
                        }
                        break;
                    }
                }
            }

            return answer;
        }

        // A >= 2
        static int Stairs(List<int> nums, int A)
        {
            int answer = 0;

            for (int i = 0; i < nums.Count;)
            {
                bool done = false;
                int subIndex = 0;
                for (int currentNumber = 1; currentNumber <= A; currentNumber++)
                {
                    if (nums[i + subIndex] != currentNumber)
                    {
                        if (currentNumber > 1)
                        {
                            subIndex--;
                        }

                        break;
                    }

                    if (nums[i + subIndex] == A)
                    {
                        done = true;
                        break;
                    }

                    subIndex++;
                }

                if (done)
                {
                    answer++;
                }

                i += subIndex + 1;
            }

            return answer;
        }

        static bool StairsTest(List<int> nums, int A, int expectedResult)
        {
            var result = Stairs(nums, A);
            var badResult = StairsWithBug(nums, A);
            Console.WriteLine($"[{(result == expectedResult).ToString().ToUpper()}] {result} - {expectedResult} | [{(badResult == expectedResult).ToString().ToUpper()}] {badResult} - {expectedResult}");

            return result == expectedResult;
        }
    }
}
