using AdventOfCode.Abstractions;
using AdventOfCode.Solutions.Day6.Models;

namespace AdventOfCode.Solutions.Day6;

public class Day6Solution : ISolution
{
    private readonly IInputProvider inputProvider;

    public Day6Solution(IInputProvider inputProvider)
    {
        this.inputProvider = inputProvider;
    }

    public int Day => 6;
    
    public long Solve()
    {
        var input = ParseInput().ToArray();

        var part1 = input
            .Select(CalculateNumberOfWaysToWin)
            .Aggregate((long)0, (current, next) => current * next);

        var raceDuration = long.Parse(input
            .Aggregate("", (current, next) => current + next.RaceDuration));
        var raceRecord = long.Parse(input
            .Aggregate("", (current, next) => current + next.RecordInMillimeters));

        var part2Input = new [] { new Race(raceDuration, raceRecord) };

        return part2Input
            .Select(CalculateNumberOfWaysToWin)
            .Sum();
    }

    // 🧠 yea, this is big brain time
    private static long CalculateNumberOfWaysToWin(Race race)
    {
        var waitTimeUntilWeWin = 0;
        
        for (var i = 1; i < race.RaceDuration / 2; i++)
        {
            if ((race.RaceDuration - i) * i > race.RecordInMillimeters)
            {
                waitTimeUntilWeWin = i;
                
                break;
            }
        }

        return race.RaceDuration - (waitTimeUntilWeWin * 2 - 1);
    }

    private Race[] ParseInput()
    {
        var input = this.inputProvider.GetPuzzleInput(this);

        var lines = input
            .Split('\n')
            .SelectMany(line => line.Split(':').Skip(1).Select(s => s.Trim()))
            .Select(line => 
                line
                    .Split(' ')
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .Select(long.Parse))
            .ToArray();

        var time = lines[0];
        var distance = lines[1];

        return time
            .Zip(distance).Select(f => new Race(f.First, f.Second))
            .ToArray();
    }
}
