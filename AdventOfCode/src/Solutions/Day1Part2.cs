using AdventOfCode.Base;
using AdventOfCode.Helpers;

namespace AdventOfCode.Solutions;

public class Day1Part2 : ISolution
{
    private static readonly IReadOnlyDictionary<string, int> DigitsAsWords = new Dictionary<string, int>
    {
        { "one", 1 },
        { "two", 2 },
        { "three", 3 },
        { "four", 4 },
        { "five", 5 },
        { "six", 6 },
        { "seven", 7 },
        { "eight", 8 },
        { "nine", 9 }
    };
    
    /// <summary>
    /// Should produce 53268
    /// </summary>
    /// <returns></returns>
    public int Solve()
    {
        var input = GetInput(1);

        var alternative = input
            .Select(GetFirstAndLastDigitSlowButLessDirty)
            .Select(int.Parse)
            .Sum();
        
        return input
            .Select(GetFirstAndLastDigitFastAndDirty)
            .Select(int.Parse)
            .Sum();
    }

    public string[] GetInput(int day)
    {
        return InputHelper.GetInputForDay(day);
    }

    private static string GetFirstAndLastDigitFastAndDirty(string input)
    {
        foreach (var digit in DigitsAsWords)
        {
            input = input.Replace(digit.Key, $"{digit.Key}{digit.Value.ToString()}{digit.Key}");
        }

        var firstDigit = (int)char.GetNumericValue(input.First(char.IsDigit));
        var lastDigit = (int)char.GetNumericValue(input.Last(char.IsDigit));
        
        return $"{firstDigit}{lastDigit}";
    }

    private static string GetFirstAndLastDigitSlowButLessDirty(string input)
    {
        int? firstDigit = null;
        int? lastDigit = null;

        for (var i = 0; i < input.Length && (firstDigit == null || lastDigit == null); i++)
        {
            firstDigit ??= ForwardSeekFirstDigit(input, i);

            lastDigit ??= BackwardSeekFirstDigit(input, i);
        }

        return $"{firstDigit!.Value}{lastDigit!.Value}";
    }

    private static int? BackwardSeekFirstDigit(string input, int i)
    {
        if (char.IsDigit(input[input.Length - 1 - i]))
        {
            return (int)char.GetNumericValue(input[input.Length - 1 - i]);
        }

        if (DigitsAsWords.Any(s => input.Substring(input.Length - 1 - i, i + 1).Contains(s.Key)))
        {
            return DigitsAsWords
                .First(s => input.Substring(input.Length - 1 - i, i + 1).Contains(s.Key))
                .Value;
        }

        return null;
    }

    private static int? ForwardSeekFirstDigit(string input, int i)
    {
        if (char.IsDigit(input[i]))
        {
            return (int)char.GetNumericValue(input[i]);
        }

        if (DigitsAsWords.Any(s => input.Substring(0, i + 1).Contains(s.Key)))
        {
            return DigitsAsWords
                .First(s => input.Substring(0, i + 1).Contains(s.Key))
                .Value;
        }

        return null;
    }
}
