namespace AdventOfCode.Solutions.Day3.Models;

public interface IInputVector
{
    bool IsDigit { get; }
        
    bool IsSymbol { get; }
    
    int Row { get; }
        
    int Column { get; }
}
