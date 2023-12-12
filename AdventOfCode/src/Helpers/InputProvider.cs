using AdventOfCode.Abstractions;

namespace AdventOfCode.Helpers;

public class InputProvider : IInputProvider
{
    public string GetPuzzleInput(ISolution solution) => File.ReadAllText($"input/inputday{solution.Day}.txt");
}
