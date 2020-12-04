using System;
using Microsoft.Extensions.Logging;

namespace AdventOfCode
{
    public class App
    {
        private readonly RunnerStarter runnerContainer;
        private readonly ILogger logger;

        public App(ILogger<App> logger, RunnerStarter runnerContainer)
        {
            this.runnerContainer = runnerContainer;
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Run()
        {
            this.logger.LogInformation("Starting...");

            runnerContainer.StartLatestRunner();

            // den Runner für eine andere/alte Aufgabe starten
            //runnerContainer.StartRunnerForDay(2);

            this.logger.LogInformation($"Finished...");
        }

    }
}
