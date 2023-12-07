using AdventOfCode.Base;
using AdventOfCode.Helpers;

namespace AdventOfCode.Solutions;

public class Day4Part2 : ISolution
{
    /// <summary>
    /// Should produce 5489600
    /// </summary>
    /// <returns></returns>
    public int Solve()
    {
        var input = GetInput(4);
        
        var originalSetOfScratchCards = input
            .Select(ParseScratchCard)
            .ToArray();

        var winningCardsPerScratchCard = originalSetOfScratchCards
            .ToDictionary(card => card.CardNumber, _ => 1);
        
        foreach (var card in originalSetOfScratchCards)
        {
            if (IsWinningScratchCard(card))
            {
                var numberOfCopiesOfCurrentCard = winningCardsPerScratchCard[card.CardNumber];
                
                var numberOfCopiesToReceive = CalculateNumberOfCopiesToReceive(card);

                var copies = GetCopiesOfScratchCards(card, numberOfCopiesToReceive, originalSetOfScratchCards);

                foreach (var copy in copies)
                {
                    winningCardsPerScratchCard[copy.CardNumber] += 1 * numberOfCopiesOfCurrentCard;
                }
            }
        }

        return winningCardsPerScratchCard.Sum(w => w.Value);
    }

    public string[] GetInput(int day)
    {
        return InputHelper.GetInputForDay(day);
    }

    private static int CalculateNumberOfCopiesToReceive(ScratchCard scratchCard)
    {
        return scratchCard.NumbersOnScratchCard.Count(scratchCard.WinningNumbers.Contains);
    }

    private static bool IsWinningScratchCard(ScratchCard scratchCard)
    {
        return Array.Exists(scratchCard.NumbersOnScratchCard, n => scratchCard.WinningNumbers.Contains(n));
    }

    private static ScratchCard[] GetCopiesOfScratchCards(
        ScratchCard currentScratchCard,
        int amountOfCopies,
        ScratchCard[] originalSetOfScratchCards)
    {
        return originalSetOfScratchCards.Skip(currentScratchCard.CardNumber).Take(amountOfCopies).ToArray();
    }
    
    private static ScratchCard ParseScratchCard(string input)
    {
        var colonIndex = input.IndexOf(':');

        var cardNumber = int.Parse(input.Substring(0, colonIndex).Replace("Card ", string.Empty));

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

        return new ScratchCard(cardNumber, winningNumbers, numbersOnScratchCard);
    }

    private sealed record ScratchCard(int CardNumber, int[] WinningNumbers, int[] NumbersOnScratchCard);
}
