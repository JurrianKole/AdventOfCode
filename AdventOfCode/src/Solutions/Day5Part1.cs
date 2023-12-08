using AdventOfCode.Base;
using AdventOfCode.Helpers;

namespace AdventOfCode.Solutions;

public class Day5Part1 : ISolution
{
    private readonly IInputProvider inputProvider;

    public Day5Part1(IInputProvider inputProvider)
    {
        this.inputProvider = inputProvider;
    }

    public long Solve()
    {
        var input = this.inputProvider.GetInputForDay(5);

        var puzzleInput = ParseInput(input);

        var answer = puzzleInput
            .Seeds
            .Select(s => GetLocationForSeed(s, puzzleInput))
            .Min();

        return answer;
    }

    private static readonly Func<long, PuzzleInput, Map>[] AlmanacMaps =
    {
        (id, puzzleInput) => GetMap(id, puzzleInput.Maps.ElementAt(0)),
        (id, puzzleInput) => GetMap(id, puzzleInput.Maps.ElementAt(1)),
        (id, puzzleInput) => GetMap(id, puzzleInput.Maps.ElementAt(2)),
        (id, puzzleInput) => GetMap(id, puzzleInput.Maps.ElementAt(3)),
        (id, puzzleInput) => GetMap(id, puzzleInput.Maps.ElementAt(4)),
        (id, puzzleInput) => GetMap(id, puzzleInput.Maps.ElementAt(5)),
        (id, puzzleInput) => GetMap(id, puzzleInput.Maps.ElementAt(6)),
    };
    
    private static long GetLocationForSeed(Seed seed, PuzzleInput puzzleInput)
    {
        return AlmanacMaps
            .Aggregate(
                seed.Identifier,
                ((identifier, mapper) =>
                {
                    var newMap = mapper(identifier, puzzleInput);

                    var offSet = identifier - newMap.SourceRangeStart;

                    identifier = newMap.DestinationRangeStart + offSet;

                    return identifier;
                }));
    }

    private static Map GetMap(long identifier, Map[] maps)
    {
        return maps
            .FirstOrDefault(
                map => identifier >= map.SourceRangeStart && identifier < map.SourceRangeStart + map.Range,
                new Map(identifier, identifier, 1));
    }

    private static PuzzleInput ParseInput(string[] input)
    {
        var seeds = input[0]
            .Substring(input[0].IndexOf(':') + 2)
            .Split(' ')
            .Select(long.Parse)
            .Select(i => new Seed(i))
            .ToArray();

        var mapNames = new[]
        {
            "seed-to-soil map:",
            "soil-to-fertilizer map:",
            "fertilizer-to-water map:",
            "water-to-light map:",
            "light-to-temperature map:",
            "temperature-to-humidity map:",
            "humidity-to-location map:"
        };

        return new PuzzleInput(
            seeds,
            mapNames.Select(mapName => GetMapFromInput(input, mapName)));
    }

    private static Map[] GetMapFromInput(string[] input, string mapName)
    {
        return input
            .SkipWhile(s => !s.Equals(mapName))
            .Skip(1)
            .TakeWhile(s => !string.IsNullOrWhiteSpace(s))
            .Select(s => s.Split(' '))
            .Select(i => new Map(
                long.Parse(i[0]), 
                long.Parse(i[1]), 
                long.Parse(i[2])))
            .ToArray();
    }

    private sealed record PuzzleInput(Seed[] Seeds, IEnumerable<Map[]> Maps);

    private sealed record Seed(long Identifier);

    private sealed record Map(long DestinationRangeStart, long SourceRangeStart, long Range);
}
