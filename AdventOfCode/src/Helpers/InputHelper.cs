namespace AdventOfCode.Helpers;

public static class InputHelper
{
    public static string[] GetInputForDay(int day) => File.ReadAllLines($"input/inputday{day}.txt");
}
