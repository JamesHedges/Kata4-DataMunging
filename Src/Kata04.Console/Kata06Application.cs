using System;
using System.IO;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using McMaster.Extensions;
using McMaster.Extensions.CommandLineUtils;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Kata04
{
    public class Kata06Application
    {
        private readonly ILogger _logger;
        readonly IMediator _mediator;
        readonly IConfiguration _configuration;

        public Kata06Application(ILogger logger, IMediator mediator, IConfiguration configuration)
        {
            _configuration = configuration;
            _mediator = mediator;
            _logger = logger;
            _logger.LogInformation("Logger injected");
        }

        public async Task OnExecute()
        {
            _logger.LogInformation("Running Kata 6 - Data Munging");

            switch (Command)
            {
                case "LoadTestData":
                    var loadTestDataCommand = new LoadTestDataCommand { FilePath = FilePath, Source = Source };
                    await _mediator.Send(loadTestDataCommand);
                    break;
                case "MinSpread":
                    var minSpreadCommand = new MinSpreadCommand { };
                    await _mediator.Send(minSpreadCommand);
                    break;
                default:
                    Console.WriteLine($"Invalid Command: {Command}");
                    break;
            }

            Console.ReadKey();
        }

        public void TestCommand(CommandOption commandOption)
        { }

        [Option("-S|--Source")]
        public string Source { get; set; }

        [Option("-F|--FileName")]
        public string FileName { get; set; }

        [Argument(0, "Command to Exec", "Command")]
        public string Command { get; set; }

        public string FilePath => $@"{Directory.GetCurrentDirectory()}\{FileName}";
    }

}
