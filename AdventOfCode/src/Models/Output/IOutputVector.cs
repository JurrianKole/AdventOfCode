namespace AdventOfCode.Models.Output;

public interface IOutputVector
{
    int Row { get; }
    
    int Column { get; }
    
    string Value { get; }
}
