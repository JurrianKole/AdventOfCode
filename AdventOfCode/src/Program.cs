
using AdventOfCode.Abstractions;
using AdventOfCode.Helpers;
using AdventOfCode.Solutions.Day1;
using AdventOfCode.Solutions.Day2;
using AdventOfCode.Solutions.Day3;
using AdventOfCode.Solutions.Day4;
using AdventOfCode.Worker;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = Host
    .CreateDefaultBuilder(args)
    .ConfigureServices(
        services =>
        {
            services.AddHostedService<PuzzleWorker>();
            
            services.AddTransient<IInputProvider, InputProvider>();

            services.AddTransient<ISolution, Day1Solution>();
            services.AddTransient<ISolution, Day2Solution>();
            services.AddTransient<ISolution, Day3Solution>();
            services.AddTransient<ISolution, Day4Solution>();
        })
    .Build();
    
await host.StartAsync();
