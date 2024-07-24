namespace ForCSharpTesting.LeetCodeSolutions;

public static partial class LeetCodeSolutions
{
    public static int[][] LargestLocal(int[][] grid)
    {
        // always greater then 0
        var dimension = grid.Length - 2;

        var result = new int[grid.Length - 2][];
        for (int i = 0; i < dimension; i++)
        {
            result[i] = new int[dimension];
            for (int j = 0; j < dimension; j++)
            {
                var max = grid[i][j];
                for (int ii = 1; ii < dimension; ii++)
                {
                    for (int jj = 0; jj < dimension; jj++)
                    {
                        if (max < grid[i + ii][j + jj])
                        {
                            max = grid[i + ii][j + jj];
                        }
                    }
                }

                result[i][j] = max;
            }
        }


        return result;
    }
}