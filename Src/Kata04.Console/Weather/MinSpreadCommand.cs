﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MediatR;
using System.IO;
using System.Linq;

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
        private readonly IKata04Config kata04Config;

        public MinSpreadHandler(ILogger logger, IKata04Config kata04Config)
        {
            _logger = logger;
            this.kata04Config = kata04Config;
        }

        public async Task<MinSpreadResponse> Handle(MinSpreadCommand request, CancellationToken cancellationToken)
        {
            var fileDesc = new WeatherFileDescription(kata04Config.WeatherFilePath);
            WeatherDataReader reader = new WeatherDataReader(fileDesc);
            using (var stream = new StreamReader(kata04Config.WeatherFilePath))
            {
                var result = await reader.ProcessFile(stream);

                return new MinSpreadResponse
                {
                    MinRangeDayNumber = result.WeatherData
                        .Select(w => new { Day = w.Day, Departure = w.MxT - w.MnT })
                        .OrderByDescending(m => m.Departure)
                        .First().Day
                };
            }

        }
    }
}