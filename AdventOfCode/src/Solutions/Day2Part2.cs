using AdventOfCode.Base;
using AdventOfCode.Helpers;

namespace AdventOfCode.Solutions;

public class Day2Part2 : ISolution
{
    private readonly IInputProvider inputProvider;

    public Day2Part2(IInputProvider inputProvider)
    {
        this.inputProvider = inputProvider;
    }

    /// <summary>
    /// Should produce 84538
    /// </summary>
    /// <returns></returns>
    public long Solve()
    {
        var input = this.inputProvider.GetInputForDay(2);
        
        return input
            .Select(ParseGameData)
            .Select(gameData => new MaximumCubeCountPerGame(gameData.GameRounds.Max(r => r.RedCount), gameData.GameRounds.Max(r => r.GreenCount), gameData.GameRounds.Max(r => r.BlueCount)))
            .Select(round => round.RedCount * round.GreenCount * round.BlueCount)
            .Sum();
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

    private sealed record MaximumCubeCountPerGame(int RedCount, int GreenCount, int BlueCount);
}
