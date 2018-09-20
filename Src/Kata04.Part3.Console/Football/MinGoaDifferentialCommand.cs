using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kata04.TabularFile;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Kata04.Part3.Football
{
    public class MinGoalDifferentialCommand : IRequest<MinGoalDifferentialResponse>
    {
    }

    public class MinGoalDifferentialResponse
    {
        public string TeamName { get; set; }
    }

    /// <summary>
    /// output the day number (column one) with the smallest temperature spread 
    /// (the maximum temperature is the second column, the minimum the third column
    /// </summary>
    public class MinGoalDifferentialHandler : IRequestHandler<MinGoalDifferentialCommand, MinGoalDifferentialResponse>
    {
        private readonly ILogger _logger;
        private readonly IKata04Config kata04Config;

        public MinGoalDifferentialHandler(ILogger logger, IKata04Config kata04Config)
        {
            _logger = logger;
            this.kata04Config = kata04Config;
        }

        public async Task<MinGoalDifferentialResponse> Handle(MinGoalDifferentialCommand request, CancellationToken cancellationToken)
        {

            var fileDesc = BuildFileDescription(kata04Config.FootballFilePath);

            TabularDataReader<FootballData> reader = new TabularDataReader<FootballData>(fileDesc);
            using (var stream = new StreamReader(kata04Config.FootballFilePath))
            {
                var result = await reader.ProcessFile(stream);

                return new MinGoalDifferentialResponse
                {
                    TeamName = result.FileData
                        .Select(f => new { Team = f.Team, Differntial = f.For - f.Against })
                        .OrderBy(m => m.Differntial)
                        .First().Team
                };
            }
        }

        private static TabularFileDescription BuildFileDescription(string filePath)
        {
            return new BuildTabularFileDescription(filePath)
                .HasHeadings(true)
                .HasSummaryLine(false)
                .IgnoreBlankLines(true)
                .IgnoreSpecialLines(false)
                .AddField("Place", typeof(int), 0, 5)
                .AddField("Team", typeof(string), 7, 15)
                .AddField("P", typeof(int), 23, 6)
                .AddField("Win", "W", typeof(int), 29, 4)
                .AddField("Loss", "L", typeof(int), 33, 4)
                .AddField("D", typeof(int), 37, 5)
                .AddField("For", "F", typeof(int), 42, 4)
                .AddField("Against", "A", typeof(int), 48, 4)
                .AddField("Points", "Pts", typeof(int), 54, 4)
                .Build();
        }
    }
}