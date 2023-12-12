namespace AdventOfCode.Abstractions;

public interface ISolution
{
    int Day { get; }

    long Solve();
}
