using Microsoft.Diagnostics.Runtime.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForCSharpTesting.GraviLink
{
    public static class _5_IsFunctionEven
    {
        public static void CheckX()
        {
            List<string> files = [
                "C:\\Users\\vghslnk\\OneDrive\\Рабочий стол\\5_test.txt",
                "C:\\Users\\vghslnk\\OneDrive\\Рабочий стол\\5_1.txt",
                "C:\\Users\\vghslnk\\OneDrive\\Рабочий стол\\5_2.txt"
            ];

            foreach (string file in files)
            {
                var lines = File.ReadAllLines(file);
                var dict = new Dictionary<string, int>();
                for (int i = 1; i < lines.Length; i++)
                {
                    var x = lines[i].Trim().Split(" ")[0];
                    if (!dict.TryAdd(x, 1))
                    {
                        dict[x]++;
                    }
                }

                var result = false;
                foreach (var value in dict.Values)
                {
                    if (value > 1)
                    {
                        result = true;
                        break;
                    }
                }

                Console.WriteLine(result);
            }
        }

        private static void PrintPointsList(List<(int x, int y)> points)
        {
            foreach (var coord in points)
            {
                Console.Write($"{coord.x}:{coord.y} ");
            }
            Console.WriteLine();
        }

        private static (int k, int b) GetStraightLineFormula((int x, int y) firstPoint, (int x, int y) secondPoint)
        {
            var k = (secondPoint.y - firstPoint.y) / (secondPoint.x - firstPoint.x);
            var b = firstPoint.y - firstPoint.x * k;

            //Console.WriteLine($"line from points {firstPoint.x}:{firstPoint.y} and {secondPoint.x}:{secondPoint.y} => y = {k}x + {b}");
            return (k, b);
        }

        private static bool IsFunctionEven(List<(int x, int y)> resultPoints)
        {
            var cx = -(resultPoints[0].x + (double)(resultPoints[^1].x - resultPoints[0].x) / 2);
            //Console.WriteLine($"C(x) = {cx}");

            var midIndex = (resultPoints.Count - 1) / 2;
            var isCountEven = resultPoints.Count % 2 == 0;

            //Console.WriteLine($"Count: {resultPoints.Count} | midIndex: {midIndex} | isEven: {isEven}");

            var result = true;
            var indexLeft = midIndex - 1;
            var indexRight = midIndex + 1;
            if (isCountEven)
            {
                indexLeft = midIndex;
            }

            while (indexLeft >= 0 && indexRight < resultPoints.Count)
            {
                if (resultPoints[indexLeft].y != resultPoints[indexRight].y)
                {
                    result = false;
                    break;
                }

                indexLeft--;
                indexRight++;
            }

            return result;
        }

        public static bool Solve(List<(int x, int y)> points)
        {
            var sortedByXPoints = points.OrderBy(p => p.x).ToList();
            //PrintPointsList(sortedByXPoints);

            var coefs = new List<(int k, int b)>(sortedByXPoints.Count);
            for (int i = 1; i < sortedByXPoints.Count; i++)
            {
                coefs.Add(GetStraightLineFormula(sortedByXPoints[i - 1], sortedByXPoints[i]));
            }

            var resultPoints = new List<(int x, int y)>(sortedByXPoints.Count)
            {
                sortedByXPoints[0]
            };

            (int? k, int? b) lastCoefs = (null, null);
            int lastUniqueIndex = 0;

            for (int i = 0; i < coefs.Count; i++)
            {
                if (coefs[i].k == lastCoefs.k && coefs[i].b == lastCoefs.b)
                {
                    resultPoints[lastUniqueIndex] = sortedByXPoints[i + 1];

                    continue;
                }

                lastCoefs.k = coefs[i].k;
                lastCoefs.b = coefs[i].b;

                resultPoints.Add(sortedByXPoints[i + 1]);
                lastUniqueIndex = resultPoints.Count - 1;
            }

            //PrintPointsList(resultPoints);


            return IsFunctionEven(resultPoints);
        }

        public static void RunInternalTests()
        {
            (List<(int, int)> points, bool expectedResult, int testNumber)[] tests =
            {
                ([(1, 1), (2, 2), (3, 3)], false, 0_001),

                ([(-5, 4), (-3, 2), (3, 2), (5, 4)], true, 1_001),
                ([(-5, 4), (5, 4), (-3, 2), (3, 2)], true, 1_002),
                ([(-3, 2), (-5, 4), (5, 4), (3, 2)], true, 1_003),

                ([(0, 4), (2, 2), (8, 2), (10, 4)], true, 2_001),
                ([(0, 4), (10, 4), (2, 2), (8, 2)], true, 2_002),
                ([(8, 2), (0, 4), (10, 4), (2, 2)], true, 2_003),

                ([(-5, 2), (-3, 4), (3, 2), (5, 4)], false, 3_001),
                ([(-5, 2), (5, 4), (-3, 4), (3, 2)], false, 3_002),
                ([(3, 2), (5, 4), (-5, 2), (-3, 4)], false, 3_003),

                ([(-10, 2), (-8, 4), (-2, 2), (0, 4)], false, 4_001),
                ([(-10, 2), (0, 4), (-8, 4), (-2, 2)], false, 4_002),
                ([(-2, 2), (0, 4), (-10, 2), (-8, 4)], false, 4_003),

                ([(-5, 4), (-3, 2), (2, 2), (3, 2), (4, 3), (5, 4)], true, 5_001),
                ([(3, 2), (4, 3), (-5, 4), (-3, 2), (2, 2), (5, 4)], true, 5_002),
                ([(3, 2), (4, 3), (2, 2), (5, 4), (-5, 4), (-3, 2)], true, 5_003),

                ([(-5, 4), (-3, 2), (-1, 2), (3, 2), (5, 4), (7, 4)], false, 6_001),
                ([(5, 4), (7, 4), (-5, 4), (-3, 2), (-1, 2), (3, 2)], false, 6_002),
                ([(-1, 2), (3, 2), (5, 4), (7, 4), (-5, 4), (-3, 2)], false, 6_003),

                ([(0, 6), (2, 3), (3, 5), (4, 4), (6, 4), (7, 5), (8, 3), (10, 6)], true, 7_001),
                ([(8, 3), (10, 6), (0, 6), (2, 3), (3, 5), (4, 4), (6, 4), (7, 5)], true, 7_002),
                ([(4, 4), (6, 4), (7, 5), (8, 3), (10, 6), (0, 6), (2, 3), (3, 5)], true, 7_003),

                ([(-10, 6), (-8, 3), (-7, 5), (-6, 4), (-4, 4), (-3, 5), (-2, 3), (0, 6)], true, 8_001),
                ([(-3, 5), (-2, 3), (0, 6), (-10, 6), (-8, 3), (-7, 5), (-6, 4), (-4, 4)], true, 8_002),
                ([(-7, 5), (-6, 4), (-4, 4), (-3, 5), (-2, 3), (0, 6), (-10, 6), (-8, 3)], true, 8_003),

                ([(2, 10), (4, 8), (7, 8), (10, 8), (11, 9), (12, 10)], true, 9_001),
                ([(11, 9), (12, 10), (2, 10), (4, 8), (7, 8), (10, 8)], true, 9_002),
                ([(7, 8), (10, 8), (11, 9), (12, 10), (2, 10), (4, 8)], true, 9_003),

                ([(-9, 12), (-7, 10), (-5, 10), (-1, 10), (0, 11), (1, 12)], true, 10_001),
                ([(0, 11), (1, 12), (-9, 12), (-7, 10), (-5, 10), (-1, 10)], true, 10_002),
                ([(-5, 10), (-1, 10), (0, 11), (1, 12), (-9, 12), (-7, 10)], true, 10_003),

                ([(-8, 7), (-7, 8), (-6, 9), (-1, 9), (1, 7), (2, 6)], false, 11_001),
                ([(1, 7), (2, 6), (-8, 7), (-7, 8), (-6, 9), (-1, 9)], false, 11_002),
                ([(-6, 9), (-1, 9), (1, 7), (2, 6), (-8, 7), (-7, 8)], false, 11_003),

                ([(-1, 7), (0, 8), (2, 10), (6, 10), (7, 10), (9, 8)], false, 12_001),
                ([(7, 10), (9, 8), (-1, 7), (0, 8), (2, 10), (6, 10)], false, 12_002),
                ([(2, 10), (6, 10), (7, 10), (9, 8), (-1, 7), (0, 8)], false, 12_003),

                ([(2, 6), (4, 8), (9, 8), (11, 6)], true, 13_001),
                ([(2, 6), (11, 6), (4, 8), (9, 8)], true, 13_002),
                ([(9, 8), (2, 6), (11, 6), (4, 8)], true, 13_003),

                ([(2, -2), (4, 0), (6, 0), (9, 0), (10, -1), (11, -2)], true, 14_001),
                ([(10, -1), (11, -2), (2, -2), (4, 0), (6, 0), (9, 0)], true, 14_002),
                ([(6, 0), (9, 0), (10, -1), (11, -2), (2, -2), (4, 0)], true, 14_003),

                ([(-4, -3), (-3, 3), (3, -3), (4, -3), (10, 3), (11, -3)], true, 15_001),
                ([(10, 3), (11, -3), (-4, -3), (-3, 3), (3, -3), (4, -3)], true, 15_002),
                ([(3, -3), (4, -3), (10, 3), (11, -3), (-4, -3), (-3, 3)], true, 15_003),

                ([(-2, -2), (-1, -1), (1, 1), (4, -2)], true, 16_001),
                ([(4, -2), (-2, -2), (-1, -1), (1, 1)], true, 16_002),
                ([(1, 1), (4, -2), (-2, -2), (-1, -1)], true, 16_003),

                ([(-12, 3), (-10, -3), (-7, 0), (-5, 2), (-4, 3), (-3, 3), (1, -1), (3, -3), (4, 0), (5, 3)], true, 17_001),
                ([(3, -3), (4, 0), (5, 3), (-12, 3), (-10, -3), (-7, 0), (-5, 2), (-4, 3), (-3, 3), (1, -1)], true, 17_002),
                ([(-5, 2), (-4, 3), (-3, 3), (1, -1), (3, -3), (4, 0), (5, 3), (-12, 3), (-10, -3), (-7, 0)], true, 17_003),

                ([(-4, 2), (-3, 0), (-2, -2), (0, 0), (1, 1), (3, -3)], false, 18_001),
                ([(1, 1), (3, -3), (-4, 2), (-3, 0), (-2, -2), (0, 0)], false, 18_002),
                ([(-2, -2), (0, 0), (1, 1), (3, -3), (-4, 2), (-3, 0)], false, 18_003),

                // ([(), (), (), (), (), (), (), ()], true, ),
            };

            var total = 0;
            var correct = 0;
            var initialColor = Console.ForegroundColor;
            foreach (var test in tests)
            {
                var result = Solve(test.points);

                Console.Write($"[{test.testNumber / 1000}_00{test.testNumber % 1000}]  \t");
                Console.ForegroundColor = result == test.expectedResult
                    ? ConsoleColor.Green
                    : ConsoleColor.Red;
                Console.WriteLine($"[{(result == test.expectedResult).ToString().ToUpper()}]\t\t{result}:{test.expectedResult}");
                Console.ForegroundColor = initialColor;

                total++;
                if (result == test.expectedResult)
                {
                    correct++;
                }
            }

            Console.WriteLine();

            Console.Write($"Correct: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{correct}");
            Console.ForegroundColor = initialColor;
            Console.Write($"/{total}");

            Console.Write($" | Wrong: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{total - correct}");
            Console.ForegroundColor = initialColor;
            Console.Write($"/{total}");
        }
    }
}
