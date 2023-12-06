namespace AdventOfCode.Helpers;

public static class StringHelper
{
    public static HashSet<char> GetDistinctNeighbours(
        string[] input,
        int column,
        int row,
        Func<char, bool> symbolFilter,
        char defaultValue)
    {
        var accumulator = new HashSet<char>();

        for (var y = -1; y < 2; y++)
        {
            for (var x = -1; x < 2; x++)
            {
                var element = TryGetAtIndexOrDefault(input, column + x, row + y, defaultValue);

                if (symbolFilter(element))
                {
                    accumulator.Add(element);
                }
            }
        }

        return accumulator;
    }

    public static int GetNeighbourCount(
        string[] input,
        int columnIndex,
        int rowIndex,
        Func<char, bool> symbolFilter,
        char defaultValue)
    {
        var count = 0;
        
        for (var y = -1; y < 2; y++)
        {
            for (var x = -1; x < 2; x++)
            {
                var element = TryGetAtIndexOrDefault(input, columnIndex + x, rowIndex + y, defaultValue);

                if (symbolFilter(element))
                {
                    count++;

                    if (char.IsDigit(TryGetAtIndexOrDefault(input, columnIndex + x + 1, rowIndex + y, defaultValue)))
                    {
                        break;
                    }
                }
            }
        }

        return count;
    }

    private static char TryGetAtIndexOrDefault(string[] input, int column, int row, char defaultValue)
    {
        var inputSize = input.Length - 1;

        if (column < 0 || row < 0 || column > inputSize || row > inputSize)
        {
            return defaultValue;
        }

        return input[row].ElementAt(column);
    }

    public static string[] GetNumericNeighbourValues(string[] input, int columnIndex, int rowIndex)
    {
        Func<char, bool> symbolFilter = char.IsDigit;

        var neighbourValues = new List<string>();
        
        for (var y = -1; y < 2; y++)
        {
            for (var x = -1; x < 2; x++)
            {
                var element = TryGetAtIndexOrDefault(input, columnIndex + x, rowIndex + y, '.');

                if (symbolFilter(element))
                {
                    var neighbourValue = ExpandAtIndex(input, columnIndex + x, rowIndex + y);

                    neighbourValues.Add(neighbourValue);

                    if (x == -1)
                    {
                        x++;
                    }
                }
            }
        }

        return neighbourValues.ToArray();
    }

    private static string ExpandAtIndex(string[] input, int columnIndex, int rowIndex)
    {
        var line = input[rowIndex];

        var left = columnIndex;
        var right = columnIndex;

        while (left - 1 > 0 && char.IsDigit(line[left - 1]))
        {
            left--;
        }

        while (right + 1 <= line.Length - 1 && char.IsDigit(line[right + 1]))
        {
            right++;
        }

        return line.Substring(left, right - left + 1);
    }
}
