namespace AdventOfCode.Helpers;

public class InputProvider : IInputProvider
{
    public string[] GetInputForDay(int day) => File.ReadAllLines($"input/inputday{day}.txt");

    public string GetRawInputForDay(int day) => File.ReadAllText($"input/inputday{day}.txt");
}
