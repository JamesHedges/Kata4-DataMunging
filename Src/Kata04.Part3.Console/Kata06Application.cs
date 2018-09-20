using System;
using System.IO;
using System.Threading.Tasks;
using Kata04.Part3.Football;
using Kata04.Part3.LoadTestData;
using Kata04.Part3.Weather;
using McMaster.Extensions.CommandLineUtils;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Kata04.Part3
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
            _logger.LogInformation("Running Kata 6 - Part 3: Data Munging");

            switch (Command)
            {
                case "LoadTestData":
                    var loadTestDataCommand = new LoadTestDataCommand { FilePath = FilePath, Source = Source };
                    await _mediator.Send(loadTestDataCommand);
                    break;
                case "MinSpread":
                    var minSpreadCommand = new MinSpreadCommand { };
                    var minSpreadresponse = await _mediator.Send(minSpreadCommand);
                    _logger.LogInformation($"Min Departure Date: {minSpreadresponse.MinRangeDayNumber}");
                    break;
                case "MinGoalDifferential":
                    var minGoalDifferentialCommand = new MinGoalDifferentialCommand { };
                    var minGoalResponse = await _mediator.Send(minGoalDifferentialCommand);
                    _logger.LogInformation($"Min goal differential team: {minGoalResponse.TeamName}");
                    break;
                default:
                    Console.WriteLine($"Invalid Command: {Command}");
                    break;
            }

            Console.ReadKey();
        }

        [Option("-S|--Source")]
        public string Source { get; set; }

        [Option("-F|--FileName")]
        public string FileName { get; set; }

        [Argument(0, "Command to Exec", "Command")]
        public string Command { get; set; }

        public string FilePath => $@"{Directory.GetCurrentDirectory()}\{FileName}";
    }

}
