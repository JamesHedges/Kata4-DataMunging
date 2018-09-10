using System;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using MediatR;
using Scrutor;
using Microsoft.Extensions.Configuration;

namespace Kata04
{
    // using the McMaster command line util: https://natemcmaster.github.io/CommandLineUtils/
    class Program
    {
        static void Main(string[] args)
        {
            IServiceProvider provider = ConfigureServices();
            CommandLineApplication<Kata06Application> app = ConfigureApplication(provider);

            app.Execute(args);
        }

        private static CommandLineApplication<Kata06Application> ConfigureApplication(IServiceProvider provider)
        {
            var app = new CommandLineApplication<Kata06Application>();
            app.Conventions
                .UseDefaultConventions()
                .UseConstructorInjection(provider);
            return app;
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddScoped<ServiceFactory>(p => p.GetService);

            services.AddLogging(c => c.AddConsole());
            services.AddTransient<ILogger>(p => p.GetRequiredService<ILoggerFactory>().CreateLogger("Console"));
            services.AddSingleton<IConfiguration>(LoadConfiguration());

            services.Scan(scan =>
                scan
                .FromAssembliesOf(typeof(IMediator), typeof(LoadTestDataCommand))
                .AddClasses(true)
                .AsImplementedInterfaces());

            return services.BuildServiceProvider();
        }

        private static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("applicationsettings.json", false, true);
            return builder.Build();
        }
    }
}
