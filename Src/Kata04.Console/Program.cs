using System;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
//using System.IO;
//using System.Text;
//using System.Threading.Tasks;

namespace Kata04
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceProvider provider = ConfigureServices();

            var app = new CommandLineApplication<Kata06Application>();
            app.Conventions
                .UseDefaultConventions()
                .UseConstructorInjection(provider);
            app.Execute(args);
        }

        public static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddLogging(c => c.AddConsole());
            services.AddTransient<ILogger>(p => p.GetRequiredService<ILoggerFactory>().CreateLogger("Console"));

            return services.BuildServiceProvider();
        }
    }
}
