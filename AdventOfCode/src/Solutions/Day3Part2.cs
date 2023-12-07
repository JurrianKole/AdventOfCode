using AdventOfCode.Base;
using AdventOfCode.Helpers;
using AdventOfCode.Models.Input;
using AdventOfCode.Models.Output;

namespace AdventOfCode.Solutions;

public class Day3Part2 : ISolution
{
    /// <summary>
    /// 87449461 is too low
    /// </summary>
    /// <returns></returns>
    public int Solve()
    {
        var input = GetInput(3);

        var parsedInput = ParseInput(input);

        var allSymbols = parsedInput.Where(p => p.IsSymbol).Select(s => (Symbol)s).ToArray();
        var allAsterisks = allSymbols.Where(s => s.Value == '*').ToArray();
        var allDigits = parsedInput.Where(p => p.IsDigit).Select(d => (Digit)d).ToArray();

        var digitsNextToSymbols = allDigits.Where(d => DigitIsNextToSymbol(d, allSymbols)).ToArray();

        var digitsNextToAsterisks = digitsNextToSymbols.Where(d => DigitIsNextToSymbol(d, allAsterisks)).ToArray();

        var asterisksNextToTwoNumbers = allAsterisks.Where(a => AsteriskIsNextToTwoDigits(a, digitsNextToAsterisks)).ToArray();

        var gearRatios = asterisksNextToTwoNumbers
            .Select(a => MultiplyGears(a, digitsNextToAsterisks, allDigits))
            .Sum();

        return gearRatios;
    }

    public string[] GetInput(int day)
    {
        return InputHelper.GetInputForDay(day);
    }

    private static int MultiplyGears(Symbol asterisk, Digit[] digitsNextToAsterisks, Digit[] allDigits)
    {
        var something = digitsNextToAsterisks
            .Where(d => DigitIsNextToSymbol(d, new[] { asterisk }))
            .ToArray();

        var reconstructedNumbers = something
            .Select(d => ReconstructNumber(d, allDigits))
            .Distinct()
            .Select(reconstructedNumber => int.Parse(reconstructedNumber.Value))
            .ToArray();

        return reconstructedNumbers[0] * reconstructedNumbers[1];
    }

    private static bool AsteriskIsNextToTwoDigits(Symbol asterisk, Digit[] digitsNextToAsterisks)
    {
        var digitsNextToCurrentAsterisk = digitsNextToAsterisks
            .Where(d => (Math.Abs(d.Row - asterisk.Row) <= 1) && (Math.Abs(d.Column - asterisk.Column) <= 1))
            .ToArray();

        var count = digitsNextToCurrentAsterisk
            .Count(digit => !digitsNextToCurrentAsterisk.Any(d => digit.Row == d.Row && digit.Column - d.Column == 1));

        return count == 2;
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

    private static bool DigitIsNextToSymbol(Digit digit, Symbol[] allSymbols)
    {
        Func<Digit, Symbol, bool> isNextToSymbol = (d, s) =>
            (d.Row == s.Row || d.Row - s.Row == 1 || d.Row - s.Row == -1)
            && (d.Column == s.Column || d.Column - s.Column == 1 || d.Column - s.Column == -1);
        
        return Array.Exists(allSymbols, s => isNextToSymbol(digit, s));
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
