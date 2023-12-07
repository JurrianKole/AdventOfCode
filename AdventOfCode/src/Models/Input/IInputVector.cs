namespace AdventOfCode.Models.Input;

public interface IInputVector
{
    bool IsDigit { get; }
        
    bool IsSymbol { get; }
    
    int Row { get; }
        
    int Column { get; }
}
