using AdventOfCode;
using AdventOfCode.Core;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace AdventOfCodeTests
{
    public abstract class AbstractTestRunner
    {
        private readonly IContainer container;



        protected AbstractTestRunner()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            var builder = new ContainerBuilder();
            builder.RegisterModule<ContainerModule>();
            builder.Populate(services);

            container = builder.Build();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            var logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            services.AddLogging(builder => builder.AddSerilog(logger, dispose: true));

            IConfiguration configurationRoot = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .AddEnvironmentVariables()
                .Build();

            services.Configure<AppSettings>(configurationRoot.GetSection(key: "App"));
        }


        protected IRunner GetRunnerForDay(string day) => container.Resolve<RunnerStarter>().GetRunnerForDay(day);

        protected T GetRunner<T>() where T : IRunner => container.Resolve<T>();
    }
}
