using AdventOfCode.Base;
using AdventOfCode.Helpers;

namespace AdventOfCode.Solutions;

public class Day1Part1 : ISolution
{
    private readonly IInputProvider inputProvider;

    public Day1Part1(IInputProvider inputProvider)
    {
        this.inputProvider = inputProvider;
    }

    /// <summary>
    /// Should produce 53080
    /// </summary>
    /// <returns></returns>
    public long Solve()
    {
        var input = this.inputProvider.GetInputForDay(1);
        
        return input
            .Select(ExtractAppendedFirstAndLastDigits)
            .Select(int.Parse)
            .Sum();
    }

    private static string ExtractAppendedFirstAndLastDigits(string input)
    {
        var inputArray = input.ToCharArray();

        var firstDigit = ForwardSeekFirstDigit(inputArray);
        var lastDigit = BackwardSeekFirstDigit(inputArray);

        return $"{firstDigit}{lastDigit}";
    }
    
    private static int BackwardSeekFirstDigit(char[] input)
    {
        var reverseInput = input.Reverse();
        
        var firstDigit = reverseInput.First(char.IsDigit);
        return (int)char.GetNumericValue(firstDigit);
    }

    private static int ForwardSeekFirstDigit(char[] input)
    {
        var firstDigit = input.First(char.IsDigit);
        return (int)char.GetNumericValue(firstDigit);
    }
}
