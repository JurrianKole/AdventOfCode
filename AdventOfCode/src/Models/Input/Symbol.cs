namespace AdventOfCode.Models.Input;

public sealed class Symbol : IInputVector
{
    public Symbol(int row, int column, char value)
    {
        this.Row = row;
        this.Column = column;
        this.Value = value;
    }

    public bool IsDigit => false;

    public bool IsSymbol => true;

    public bool IsDot => false;

    public int Row { get; }

    public int Column { get; }
    
    public char Value { get; }
}
