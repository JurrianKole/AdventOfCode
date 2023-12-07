using AdventOfCode.Base;
using AdventOfCode.Helpers;

namespace AdventOfCode.Solutions;

public class Day4Part1 : ISolution
{
    public int Solve()
    {
        var input = GetInput(4);

        var points = input
            .Select(ParseScratchCard)
            .Select(CalculateScore)
            .Sum();
        
        return points;
    }

    public string[] GetInput(int day)
    {
        return InputHelper.GetInputForDay(day);
    }

    private static int CalculateScore(ScratchCard scratchCard)
    {
        var yourWinningNumbers = scratchCard
            .NumbersOnScratchCard.Where(scratchCard.WinningNumbers.Contains)
            .ToArray();

        if (!yourWinningNumbers.Any())
        {
            return 0;
        }

        return yourWinningNumbers.Aggregate(1, (acc, _) => acc * 2) / 2;
    }

    private static ScratchCard ParseScratchCard(string input)
    {
        var colonIndex = input.IndexOf(':');

        var winningNumbersStartIndex = colonIndex + 2;

        var cardInput = input.Substring(winningNumbersStartIndex);

        var numbers = cardInput.Split('|');

        var winningNumbers = numbers
            .First()
            .Split(' ')
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(int.Parse)
            .ToArray();

        var numbersOnScratchCard = numbers
            .Last()
            .Split(' ')
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(int.Parse)
            .ToArray();

        return new ScratchCard(winningNumbers, numbersOnScratchCard);
    }

    private sealed record ScratchCard(int[] WinningNumbers, int[] NumbersOnScratchCard);
}
