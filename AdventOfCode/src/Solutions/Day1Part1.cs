using AdventOfCode.Base;
using AdventOfCode.Helpers;

namespace AdventOfCode.Solutions;

public class Day1Part1 : ISolution
{
    /// <summary>
    /// Should produce 53080
    /// </summary>
    /// <returns></returns>
    public int Solve()
    {
        var input = GetInput(1);
        
        return input
            .Select(ExtractAppendedFirstAndLastDigits)
            .Select(int.Parse)
            .Sum();
    }

    public string[] GetInput(int day)
    {
        return InputHelper.GetInputForDay(day);
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
