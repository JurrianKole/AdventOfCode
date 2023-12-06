namespace AdventOfCode.Base;

public class SolutionBase
{
    internal static string[] GetInput(string filename) => File.ReadAllLines($"input/{filename}.txt");
}
