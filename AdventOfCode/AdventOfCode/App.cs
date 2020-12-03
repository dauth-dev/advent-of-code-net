using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace AdventOfCode
{
    public class App
    {
        private readonly AppSettings appSettings;
        private readonly RunnerStarter runnerContainer;
        private readonly ILogger logger;

        public App(AppSettings appSettings, ILogger<App> logger, RunnerStarter runnerContainer)
        {
            this.appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
            this.runnerContainer = runnerContainer;
            this.logger = logger ?? throw new ArgumentNullException(nameof(appSettings));
        }

        public void Run()
        {
            this.logger.LogInformation("Starting...");

            //runnerContainer.StartLatestRunner();

            // den Runner für eine andere/alte Aufgabe starten
            runnerContainer.StartRunnerForDay(2);

            this.logger.LogInformation($"Finished...");
        }

    }
}
