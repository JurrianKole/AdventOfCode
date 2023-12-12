namespace AdventOfCode.Solutions.Day3.Models;

public sealed class Digit : IInputVector
{
    public Digit(int row, int column, int value)
    {
        this.Row = row;
        this.Column = column;
        this.Value = value;
    }

    public bool IsDigit => true;

    public bool IsSymbol => false;
    
    public int Row { get; }

    public int Column { get; }
    
    public int Value { get; }
}