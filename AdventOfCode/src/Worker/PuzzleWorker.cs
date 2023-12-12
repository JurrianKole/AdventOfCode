using AdventOfCode.Abstractions;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AdventOfCode.Worker;

public class PuzzleWorker : IHostedService
{
    private readonly ILogger<PuzzleWorker> logger;

    private readonly IEnumerable<ISolution> solutions;

    public PuzzleWorker(
        ILogger<PuzzleWorker> logger,
        IEnumerable<ISolution> solutions)
    {
        this.logger = logger;
        this.solutions = solutions;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Starting puzzle worker service");

        foreach (var solution in this.solutions)
        {
            this.logger.LogInformation("Answer for day {Day}: {Answer}", solution.Day, solution.Solve());
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Stopping puzzle worker service");
    }
}
