namespace AdventOfCode.Helpers;

public class InputProvider : IInputProvider
{
    public string[] GetInputForDay(int day) => File.ReadAllLines($"input/inputday{day}.txt");
}
