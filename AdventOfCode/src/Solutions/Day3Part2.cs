using AdventOfCode.Base;
using AdventOfCode.Helpers;

namespace AdventOfCode.Solutions;

public class Day3Part2 : SolutionBase
{
    /// <summary>
    /// 77783407 is too low
    /// 98398841
    /// </summary>
    /// <returns></returns>
    public static int Solve()
    {
        var input = GetInput("inputday3").Select((line, index) => );
        
        return input
            .SelectMany(ExtractPotentialGearMetadata)
            .Where(partNumber => HasTwoAdjacentNumbers(partNumber, input))
            .Select(potentialGear => new GearMetaData(potentialGear.RowIndex, potentialGear.ColumnIndex))
            .Select(gear => GetAdjacentPartNumbers(gear, input))
            .Select(partNumbers => int.Parse(partNumbers[0].Value) * int.Parse(partNumbers[1].Value))
            .Sum();
    }
    
    private static readonly Func<char, bool> SymbolFilter = char.IsDigit;
    
    private static bool HasTwoAdjacentNumbers(
        PotentialGearMetaData potentialGearMetaData, 
        string[] input)
    {
        var neighbourCount = StringHelper.GetNeighbourCount(
            input,
            potentialGearMetaData.ColumnIndex,
            potentialGearMetaData.RowIndex,
            SymbolFilter,
            '.');

        return neighbourCount == 2;
    }

    private static PartNumber[] GetAdjacentPartNumbers(
        GearMetaData gearMetaData,
        string[] input)
    {
        var partNumbers = StringHelper.GetNumericNeighbourValues(
            input,
            gearMetaData.ColumnIndex,
            gearMetaData.RowIndex);

        return partNumbers
            .Select(p => new PartNumber(p))
            .ToArray();
    }

    private static PotentialGearMetaData[] ExtractPotentialGearMetadata(string input, int row)
    {
        var potentialGears = new List<PotentialGearMetaData>();
        
        for (var i = 0; i < input.Length; i++)
        {
            if (input[i] == '*')
            {
                var gear = GetPotentialGearMetaData(i, row);

                potentialGears.Add(gear);
            }
        }

        return potentialGears.ToArray();
    }

    private static PotentialGearMetaData GetPotentialGearMetaData(int offSet, int row)
    {
        return new PotentialGearMetaData(row, offSet);
    }

    private sealed record PartNumber(string Value);

    private sealed record PotentialGearMetaData(int RowIndex, int ColumnIndex);

    private sealed record GearMetaData(int RowIndex, int ColumnIndex);
}
