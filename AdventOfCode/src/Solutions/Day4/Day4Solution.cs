using AdventOfCode.Abstractions;
using AdventOfCode.Solutions.Day4.Models;

namespace AdventOfCode.Solutions.Day4;

public class Day4Solution : ISolution
{
    private readonly IInputProvider inputProvider;

    public Day4Solution(IInputProvider inputProvider)
    {
        this.inputProvider = inputProvider;
    }

    public int Day => 4;

    public long Solve()
    {
        var scratchCards = GetScratchCards().ToArray();

        var totalCards = scratchCards.ToDictionary(card => card.GameNumber, _ => 1);

        foreach (var scratchCard in scratchCards)
        {
            var numberOfCopiesOfCurrentCard = totalCards[scratchCard.GameNumber];
            
            var winningNumbers = scratchCard.NumbersOnCard.Where(scratchCard.WinningNumbers.Contains);

            for (var i = 0; i < winningNumbers.Count(); i++)
            {
                totalCards[scratchCard.GameNumber + 1 + i] += 1 * numberOfCopiesOfCurrentCard;
            }
        }
        
        return totalCards.Sum(card => card.Value);
    }

    private ScratchCard[] GetScratchCards()
    {
        var input = this.inputProvider.GetPuzzleInput(this);

        return input
            .Split('\n')
            .Select(
                line => new ScratchCard(
                    int.Parse(line.Split(':')[0].Replace("Card", string.Empty).Trim()),
                    line.Split(':')[1].Trim().Split('|')[0].Trim().Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).Select(int.Parse).ToArray(),
                    line.Split(':')[1].Trim().Split('|')[1].Trim().Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).Select(int.Parse).ToArray()
                ))
            .ToArray();
    }
}
