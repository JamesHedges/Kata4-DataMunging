using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MediatR;

namespace Kata04.Weather
{
    public class MinSpreadCommand : IRequest<MinSpreadResponse>
    {
    }

    public class MinSpreadResponse
    {
        public int MinRangeDayNumber { get; set; }
    }

    /// <summary>
    /// output the day number (column one) with the smallest temperature spread 
    /// (the maximum temperature is the second column, the minimum the third column
    /// </summary>
    public class MinSpreadHandler : IRequestHandler<MinSpreadCommand, MinSpreadResponse>
    {
        private readonly ILogger _logger;

        public MinSpreadHandler(ILogger logger)
        {
            _logger = logger;
        }

        public Task<MinSpreadResponse> Handle(MinSpreadCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}