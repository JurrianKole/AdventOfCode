using AdventOfCode.Abstractions;

namespace AdventOfCode.Solutions.Day2;

public class Day2Solution : ISolution
{
    private readonly IInputProvider inputProvider;

    public Day2Solution(IInputProvider inputProvider)
    {
        this.inputProvider = inputProvider;
    }

    public int Day => 2;

    public long Solve()
    {
        // Should produce 84538
        var input = this.inputProvider.GetPuzzleInput(this).Split('\n');
        
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
