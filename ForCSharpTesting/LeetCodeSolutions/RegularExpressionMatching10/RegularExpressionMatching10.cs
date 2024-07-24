using System.Text;

namespace ForCSharpTesting.LeetCodeSolutions.RegularExpressionMatching10;

public class RegularExpressionMatching10
{
    private const char AnySingleSymbol = '.';
    private const char AnyAmountOfSymbols = '*';

    public bool IsMatch(string source, string pattern, out string output)
    {
        var tempOutput = new StringBuilder();
        output = string.Empty;

        var patternBricks = GetPatternBricks(pattern);

        Dictionary<int, bool> currentDictionary = new();
        Dictionary<int, bool> previousDictionary = new() { { 0, true } };
        Dictionary<int, bool> tempRef;

        foreach (var patternBrick in patternBricks)
        {
            currentDictionary.Clear();
            foreach (var index in previousDictionary.Keys)
            {
                FillNextIndexes(index, source, patternBrick, currentDictionary);
            }

            tempRef = previousDictionary;
            previousDictionary = currentDictionary;
            currentDictionary = tempRef;
        }

        return previousDictionary.ContainsKey(source.Length);
    }

    private void FillNextIndexes(int currentIndex, string source, string patternBrick, Dictionary<int, bool> dict)
    {
        if (patternBrick.Length == 2 &&  patternBrick[0] == AnySingleSymbol)
        {
            FillNextIndexesForAnyAmountOfAnySymbols(currentIndex, source, dict);
        }
        else if (patternBrick.Length == 2 && patternBrick[1] == AnyAmountOfSymbols)
        {
            FillNextIndexesForAnyAmountOfSpecificSymbol(currentIndex, patternBrick[0], source, dict);
        }
        else if (patternBrick.Length == 1 && patternBrick[0] == AnySingleSymbol)
        {
            FillNextIndexesForAnySymbol(currentIndex, source, dict);
        }
        else
        {
            FillNextIndexesForAnyString(currentIndex, source, patternBrick, dict);
        }
    }

    private void FillNextIndexesForAnyAmountOfAnySymbols(int currentIndex, string source, Dictionary<int, bool> dict)
    {
        int i = currentIndex;
        for (; i < source.Length; i++)
        {
            dict[i] = true;
        }

        dict[i] = true;
    }

    private void FillNextIndexesForAnyAmountOfSpecificSymbol(int currentIndex, char specificSymbol, string source,
        Dictionary<int, bool> dict)
    {
        dict[currentIndex] = true;

        int i = currentIndex;
        for (; i < source.Length; i++)
        {
            if (source[i] != specificSymbol)
            {
                break;
            }

            dict[i] = true;
        }

        if (currentIndex != i)
        {
            dict[i] = true;
        }
    }

    private void FillNextIndexesForAnySymbol(int currentIndex, string source, Dictionary<int, bool> dict)
    {
        if (currentIndex + 1 <= source.Length)
        {
            dict[currentIndex + 1] = true;
        }
    }

    private void FillNextIndexesForAnyString(int currentIndex, string source, string patternBrick, Dictionary<int, bool> dict)
    {
        if (currentIndex + patternBrick.Length > source.Length)
        {
            return;
        }

        var passed = true;
        for (int i = 0; i < patternBrick.Length; i++)
        {
            if (source[currentIndex + i] != patternBrick[i])
            {
                passed = false;
                break;
            }
        }

        if (passed)
        {
            dict[currentIndex + patternBrick.Length] = true;
        }
    }

    private List<string> GetPatternBricks(string pattern)
    {
        var patternBricks = new List<string>(pattern.Length);
        var tempString = new StringBuilder(pattern.Length);
        for (int i = 0; i < pattern.Length; i++)
        {
            if (i < pattern.Length - 1 && pattern[i + 1] == AnyAmountOfSymbols)
            {
                AddPatternBrickAndClearStringBuilder(tempString, patternBricks);

                patternBricks.Add($"{pattern[i++]}{AnyAmountOfSymbols}");
                continue;
            }

            if (pattern[i] == AnySingleSymbol)
            {
                AddPatternBrickAndClearStringBuilder(tempString, patternBricks);

                patternBricks.Add(AnySingleSymbol.ToString());
                continue;
            }

            tempString.Append(pattern[i]);
        }

        AddPatternBrickAndClearStringBuilder(tempString, patternBricks);

        return patternBricks;
    }

    private void AddPatternBrickAndClearStringBuilder(StringBuilder stringBuilder, List<string> patternBricksList)
    {
        if (stringBuilder.Length > 0)
        {
            patternBricksList.Add(stringBuilder.ToString());
            stringBuilder.Clear();
        }
    }
}