namespace AdventOfCode.Models.Input;

public sealed class Dot : IInputVector
{
    public bool IsDigit => false;

    public bool IsSymbol => false;
    
    public int Row { get; }

    public int Column { get; }
}
