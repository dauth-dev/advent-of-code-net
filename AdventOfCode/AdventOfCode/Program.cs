using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace AdventOfCode
{
    internal class Program
    {
        internal static IServiceProvider ServiceProvider { get; private set; }

        public static void Main()
        {
            var services = new ServiceCollection();

            ConfigureServices(services);

            ServiceProvider = ConfigureContainer(services);
            ServiceProvider.GetService<App>().Run();
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

        private static IServiceProvider ConfigureContainer(ServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<ContainerModule>();

            builder.Populate(services);

            var container = builder.Build();
            return new AutofacServiceProvider(container);
        }
    }
}
