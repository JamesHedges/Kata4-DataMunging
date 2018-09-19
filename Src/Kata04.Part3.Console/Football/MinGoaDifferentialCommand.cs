using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
            var fileDesc = new FootballFileDescription(kata04Config.FootballFilePath);
            FootballDataReader reader = new FootballDataReader(fileDesc);
            using (var stream = new StreamReader(kata04Config.FootballFilePath))
            {
                var result = await reader.ProcessFile(stream);

                return new MinGoalDifferentialResponse
                {
                    TeamName = result.FootballData
                        .Select(f => new { Team = f.Team, Differntial = f.For - f.Against })
                        .OrderBy(m => m.Differntial)
                        .First().Team
                };
            }

        }
    }
}