using System.Text;

using AdventOfCode.Base;
using AdventOfCode.Helpers;

namespace AdventOfCode.Solutions;

public class Day3Part1 : SolutionBase
{
    /// <summary>
    /// Should produce 546312
    /// </summary>
    /// <returns></returns>
    public static int Solve()
    {
        var input = GetInput("inputday3");

        var symbols = ExtractSymbols(input);

        var potentialPartNumbers = input
            .Select(ExtractPotentialPartNumberMetadata)
            .SelectMany(p => p)
            .ToArray();

        return potentialPartNumbers
            .Where(partNumber => IsPartNumber(partNumber, input, symbols))
            .Select(p => p.Value)
            .Select(int.Parse)
            .Sum();
    }
    
    private static readonly Func<char, bool> SymbolFilter = c => !char.IsDigit(c) && c != '.';

    private static bool IsPartNumber(
        PotentialPartNumberMetaData potentialPartNumberMetaData, 
        string[] input, 
        HashSet<char> partNumberIdentifiers)
    {
        for (var i = 0; i < potentialPartNumberMetaData.Value.Length; i++)
        {
            var distinctNeighbours = StringHelper.GetDistinctNeighbours(
                input, 
                potentialPartNumberMetaData.ColumnStartIndex + i, 
                potentialPartNumberMetaData.RowIndex,
                SymbolFilter,
                '.');

            if (distinctNeighbours.Any(partNumberIdentifiers.Contains))
            {
                return true;
            }
        }

        return false;
    }
    
    private static HashSet<char> ExtractSymbols(string[] input)
    {
        return input
            .Select(line => line.ToCharArray().Where(SymbolFilter))
            .SelectMany(c => c)
            .ToHashSet();
    }

    private static PotentialPartNumberMetaData[] ExtractPotentialPartNumberMetadata(string input, int row)
    {
        var partNumbers = new List<PotentialPartNumberMetaData>();
        
        for (var i = 0; i < input.Length; i++)
        {
            if (char.IsDigit(input[i]))
            {
                var partNumber = GetPartNumber(input, i, row);

                partNumbers.Add(partNumber);

                i += partNumber.Value.Length;
            }
        }

        return partNumbers.ToArray();
    }

    private static PotentialPartNumberMetaData GetPartNumber(string input, int offSet, int row)
    {
        var stringBuilder = new StringBuilder();
        
        for (var i = offSet; i < input.Length; i++)
        {
            if (char.IsDigit(input[i]))
            {
                stringBuilder.Append(char.GetNumericValue(input[i]));
            }
            else
            {
                break;
            }
        }
        
        return new PotentialPartNumberMetaData(stringBuilder.ToString(), row, offSet);
    }

    private sealed record PotentialPartNumberMetaData(string Value, int RowIndex, int ColumnStartIndex);
}
