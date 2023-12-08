using AdventOfCode.Base;
using AdventOfCode.Helpers;
using AdventOfCode.Models.Input;
using AdventOfCode.Models.Output;

namespace AdventOfCode.Solutions;

public class Day3Part1 : ISolution
{
    private readonly IInputProvider inputProvider;

    public Day3Part1(IInputProvider inputProvider)
    {
        this.inputProvider = inputProvider;
    }

    /// <summary>
    /// Should produce 546312
    /// </summary>
    /// <returns></returns>
    public long Solve()
    {
        var input = this.inputProvider.GetInputForDay(3);

        var parsedInput = ParseInput(input);

        var allSymbols = parsedInput.Where(p => p.IsSymbol).Select(s => (Symbol)s).ToArray();
        var allDigits = parsedInput.Where(p => p.IsDigit).Select(d => (Digit)d).ToArray();

        var digitsWithNeighbouringSymbol = allDigits.Where(d => DigitIsNextToSymbol(d, allSymbols)).ToArray();

        var reconstructedNumbers = digitsWithNeighbouringSymbol
            .Select(n => ReconstructNumber(n, allDigits))
            .Distinct();

        var numbers = reconstructedNumbers
            .Select(o => int.Parse(o.Value))
            .Sum();

        return numbers;
    }

    private static IOutputVector ReconstructNumber(Digit digit, Digit[] allDigits)
    {
        var digitsInSameRow = allDigits
            .Where(d => d.Row == digit.Row)
            .OrderBy(d => d.Column)
            .ToArray();

        var leftDigit = digit;
        var rightDigit = digit;

        while (Array.Exists(digitsInSameRow, d => leftDigit.Column - d.Column == 1))
        {
            leftDigit = digitsInSameRow.First(d => leftDigit.Column - d.Column == 1);
        }

        while (Array.Exists(digitsInSameRow, d => rightDigit.Column - d.Column == -1))
        {
            rightDigit = digitsInSameRow.First(d => rightDigit.Column - d.Column == -1);
        }

        var value = digitsInSameRow
            .Where(d => d.Column >= leftDigit.Column && d.Column <= rightDigit.Column)
            .Aggregate("", (current, next) => current + next.Value);

        return new Output(leftDigit.Row, leftDigit.Column, value);
    }
    
    private static bool DigitIsNextToSymbol(Digit digit, Symbol[] symbols)
    {
        Func<Digit, Symbol, bool> isNextToSymbol = (d, s) => 
            (d.Row == s.Row || d.Row - s.Row == 1 || d.Row - s.Row == -1) 
            && (d.Column == s.Column || d.Column - s.Column == 1 || d.Column - s.Column == -1);
        
        return Array.Exists(symbols, s => isNextToSymbol(digit, s));
    }

    private static IInputVector[] ParseInput(string[] input)
    {
        return input
            .Select(
                (line, rowIndex) =>
                    line.Select(
                        (character, columnIndex) =>
                        {
                            return character switch
                            {
                                '.' => (IInputVector)new Dot(),
                                _ when char.IsDigit(character) => new Digit(rowIndex, columnIndex, (int)char.GetNumericValue(character)),
                                _ => new Symbol(rowIndex, columnIndex, character)
                            };
                        }))
            .SelectMany(r => r)
            .ToArray();
    }
}
