using System;
using System.IO;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using McMaster.Extensions;
using McMaster.Extensions.CommandLineUtils;
using MediatR;
using Microsoft.Extensions.Configuration;
using Kata04.Weather;

namespace Kata04
{
    //LoadTestData -S football.dat -F Football.dat
    //MinSpread
    public class Kata06Application
    {
        private readonly ILogger _logger;
        readonly IMediator _mediator;
        readonly IKata04Config _configuration;

        public Kata06Application(ILogger logger, IMediator mediator, IKata04Config configuration)
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
                    var response = await _mediator.Send(minSpreadCommand);
                    _logger.LogInformation($"Min Departure Date: {response.MinRangeDayNumber}");
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
