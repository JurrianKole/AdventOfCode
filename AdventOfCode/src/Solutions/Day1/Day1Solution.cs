using AdventOfCode.Abstractions;

namespace AdventOfCode.Solutions.Day1;

public class Day1Solution : ISolution
{
    private static readonly IDictionary<string, string> DigitsAsWords = new Dictionary<string, string>
    {
        { "one", "one1one" },
        { "two", "two2two" },
        { "three", "three3three" },
        { "four", "four4four" },
        { "five", "five5five" },
        { "six", "six6six" },
        { "seven", "seven7seven" },
        { "eight", "eight8eight" },
        { "nine", "nine9nine" }
    };
    
    private readonly IInputProvider inputProvider;

    public Day1Solution(IInputProvider inputProvider)
    {
        this.inputProvider = inputProvider;
    }

    public int Day => 1;

    public long Solve()
    {
        var puzzleInput = this.inputProvider.GetPuzzleInput(this);

        var lines = puzzleInput.Split('\n');

        var result = lines
            .Select(GetFirstAndLastDigit)
            .Select(int.Parse)
            .Sum();
        
        return result;
    }

    private static string GetFirstAndLastDigit(string input)
    {
        input = DigitsAsWords.Aggregate(input, (current, digitsAsWord) => current.Replace(digitsAsWord.Key, digitsAsWord.Value));

        return $"{(int)char.GetNumericValue(input.First(char.IsDigit))}{(int)char.GetNumericValue(input.Last(char.IsDigit))}";
    }
}
