using ForCSharpTesting.LeetCodeSolutions.Common;

namespace ForCSharpTesting.LeetCodeSolutions.Interview._2026;

public class NumberOfIslands : BaseLeetCodeTask<char[][], int>
{
    public void KillTheIsland(char[][] grid, (int x, int y) pointOfIsland)
    {
        var queueForCurrentIsland = new Queue<(int, int)>();

        queueForCurrentIsland.Enqueue((pointOfIsland.x, pointOfIsland.y));
        grid[pointOfIsland.x][pointOfIsland.y] = '0';

        while (queueForCurrentIsland.Count > 0)
        {
            var (curX, curY) = queueForCurrentIsland.Dequeue();

            if (curX - 1 >= 0 && grid[curX - 1][curY] == '1')
            {
                grid[curX - 1][curY] = '0';
                queueForCurrentIsland.Enqueue((curX - 1, curY));
            }

            if (curY - 1 >= 0 && grid[curX][curY - 1] == '1')
            {
                grid[curX][curY - 1] = '0';
                queueForCurrentIsland.Enqueue((curX, curY - 1));
            }

            if (curX + 1 < grid.Length && grid[curX + 1][curY] == '1')
            {
                grid[curX + 1][curY] = '0';
                queueForCurrentIsland.Enqueue((curX + 1, curY));
            }

            if (curY + 1 < grid[curX].Length && grid[curX][curY + 1] == '1')
            {
                grid[curX][curY + 1] = '0';
                queueForCurrentIsland.Enqueue((curX, curY + 1));
            }
        }
    }

    public int NumIslands(char[][] grid)
    {
        int countOfIslands = 0;
        
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                var currentItem = grid[i][j];
                if (currentItem == '1')
                {
                    countOfIslands++;
                    KillTheIsland(grid, (i, j));
                }
            }
        }

        return countOfIslands;
    }

    public override IEnumerable<(char[][] Input, int Result)> GetTests()
    {
        yield return (
            [
              ['1','1','1','1','0'],
              ['1','1','0','1','0'],
              ['1','1','0','0','0'],
              ['0','0','0','0','0']
            ],
            1);

        yield return (
            [
                ['1', '1', '1', '1', '1', '0', '1', '1', '1', '1', '1', '1', '1', '1', '1', '0', '1', '0', '1', '1'],
                ['0', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '0', '1', '1', '1', '1', '1', '0'],
                ['1', '0', '1', '1', '1', '0', '0', '1', '1', '0', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1'],
                ['1', '1', '1', '1', '0', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1'],
                ['1', '0', '0', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1'],
                ['1', '0', '1', '1', '1', '1', '1', '1', '0', '1', '1', '1', '0', '1', '1', '1', '0', '1', '1', '1'],
                ['0', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '0', '1', '1', '0', '1', '1', '1', '1'],
                ['1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '0', '1', '1', '1', '1', '0', '1', '1'],
                ['1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '0', '1', '1', '1', '1', '1', '1', '1', '1', '1'],
                ['1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1'],
                ['0', '1', '1', '1', '1', '1', '1', '1', '0', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1'],
                ['1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1'],
                ['1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1'],
                ['1', '1', '1', '1', '1', '0', '1', '1', '1', '1', '1', '1', '1', '0', '1', '1', '1', '1', '1', '1'],
                ['1', '0', '1', '1', '1', '1', '1', '0', '1', '1', '1', '0', '1', '1', '1', '1', '0', '1', '1', '1'],
                ['1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '0', '1', '1', '1', '1', '1', '1', '0'],
                ['1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '0', '1', '1', '1', '1', '0', '0'],
                ['1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1'],
                ['1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1'],
                ['1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1']
            ],
            1);

        

        yield return (
            [
              ['1','1','0','0','0'],
              ['1','1','0','0','0'],
              ['0','0','1','0','0'],
              ['0','0','0','1','1']
            ],
            3);

        
    }

    public override (bool Equity, int FactResult) RunTest((char[][] Input, int Result) test)
    {
        var methodResult = NumIslands(test.Input);

        return (methodResult == test.Result, methodResult);
    }
}
