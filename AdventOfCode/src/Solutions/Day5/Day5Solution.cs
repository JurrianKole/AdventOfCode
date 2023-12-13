using AdventOfCode.Abstractions;
using AdventOfCode.Solutions.Day5.Models;

namespace AdventOfCode.Solutions.Day5;

public class Day5Solution : ISolution
{
    private readonly IInputProvider inputProvider;

    public Day5Solution(IInputProvider inputProvider)
    {
        this.inputProvider = inputProvider;
    }

    public int Day => 5;

    public long Solve()
    {
        var almanac = ParseInput();

        // only part 1 :(
        return almanac
            .Seeds
            .Select(seed => GetFinalDestination(seed, almanac.Maps))
            .Min();
    }

    // unexpected movie crossover 💀💀💀
    private static long GetFinalDestination(Seed seed, IEnumerable<Map[]> maps)
    {
        return maps
            .Aggregate(
                seed.Value,
                (value, map) =>
                {
                    var mapForCurrentValue = map.FirstOrDefault(
                        m => value >= m.Source && value < m.Source + m.Range, 
                        new Map(value, value, 1));
                    
                    return value - mapForCurrentValue.Source + mapForCurrentValue.Destination;
                });
    }

    private Almanac ParseInput()
    {
        var input = this.inputProvider.GetPuzzleInput(this);

        var seeds = input.Split('\n')[0]
            .Split(':')[1]
            .Trim()
            .Split(' ')
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(value => new Seed(long.Parse(value)))
            .ToArray();

        var maps = input.Split("\n\n")
            .Skip(1)
            .Select(mapBlock => mapBlock.Split('\n').Skip(1).Select(map => map.Split(' ')).Select(mapValue => new Map(
                long.Parse(mapValue[0]),
                long.Parse(mapValue[1]),
                long.Parse(mapValue[2])))
                .ToArray());

        return new Almanac(seeds, maps);
    }
}
