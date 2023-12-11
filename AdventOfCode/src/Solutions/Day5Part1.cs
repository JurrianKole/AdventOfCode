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
        var puzzleInput = ParseInput();

        var seeds = puzzleInput.Seeds;
        var puzzleInputMaps = puzzleInput.Maps;

        var something = seeds
            .Select(
                seed => puzzleInputMaps
                    .Aggregate(
                        seed,
                        (identifier, maps) =>
                        {
                            var offSet = maps.FirstOrDefault(map => identifier >= map[1] && identifier < map[1] + map[2], new[] { identifier, identifier, 1 });

                            return identifier - offSet[1] + offSet[0];
                        }))
            .Min();

        return something;
    }

    private PuzzleInput ParseInput()
    {
        var input = this.inputProvider.GetRawInputForDay(5);

        var segments = input.Split("\n\n");

        var seeds = segments[0].Split(':')[1].TrimStart(' ').Split(' ').Select(long.Parse);

        var maps = segments.Skip(1).Select(block => block.Split('\n').Skip(1).Select(line => line.Split(' ').Select(long.Parse).ToArray()));

        return new PuzzleInput(seeds, maps);
    }

    private sealed record PuzzleInput(IEnumerable<long> Seeds, IEnumerable<IEnumerable<long[]>> Maps);
}
