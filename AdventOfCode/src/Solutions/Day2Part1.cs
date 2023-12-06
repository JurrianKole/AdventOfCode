using AdventOfCode.Base;

namespace AdventOfCode.Solutions;

public class Day2Part1 : SolutionBase
{
    /// <summary>
    /// Should produce 1867
    /// </summary>
    /// <returns></returns>
    public static int Solve()
    {
        var input = GetInput("inputday2");

        const int MaximumRedCubeCount = 12;
        const int MaximumGreenCubeCount = 13;
        const int MaximumBlueCubeCount = 14;

        return input
            .Select(ParseGameData)
            .Where(data => Array.TrueForAll(
                data.GameRounds, 
                round => 
                    round.RedCount <= MaximumRedCubeCount 
                    && round.GreenCount <= MaximumGreenCubeCount 
                    && round.BlueCount <= MaximumBlueCubeCount))
            .Sum(r => r.GameNumber);
    }

    private static GameData ParseGameData(string input)
    {
        input = input.Replace(" ", string.Empty);
        input = input.Replace("Game", string.Empty);
        
        var colonIndex = input.IndexOf(':');
        
        var gameNumber = int.Parse(input.Substring(0, colonIndex));

        input = input.Replace($"{gameNumber}:", string.Empty);
        
        var gameRounds = input
            .Split(';')
            .Select(
                s =>
                {
                    var cubes = s.Split(',');

                    var redCount = CalculateAmountOfCubesInRound(cubes, "red");
                    var greenCount = CalculateAmountOfCubesInRound(cubes, "green");
                    var blueCount = CalculateAmountOfCubesInRound(cubes, "blue");

                    return new GameRound(redCount, greenCount, blueCount);
                })
            .ToArray();

        return new GameData(gameNumber, gameRounds);
    }

    private static int CalculateAmountOfCubesInRound(string[] cubes, string color)
    {
        return cubes
            .Where(c => c.EndsWith(color))
            .Select(c => int.Parse(c.Substring(0, c.IndexOf(color[0]))))
            .Sum();
    }

    private sealed record GameData(int GameNumber, GameRound[] GameRounds);

    private sealed record GameRound(int RedCount, int GreenCount, int BlueCount);
}
