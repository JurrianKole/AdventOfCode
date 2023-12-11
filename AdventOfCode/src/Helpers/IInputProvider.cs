namespace AdventOfCode.Helpers;

public interface IInputProvider
{
    string[] GetInputForDay(int day);

    string GetRawInputForDay(int day);
}