namespace AdventOfCode.Models.Output;

public sealed record Output : IOutputVector
{
    public Output(int row, int column, string value)
    {
        this.Row = row;
        this.Column = column;
        this.Value = value;
    }

    public int Row { get; }

    public int Column { get; }

    public string Value { get; }
}
