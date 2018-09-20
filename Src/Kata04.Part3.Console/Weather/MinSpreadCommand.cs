using MediatR;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kata04.TabularFile;
using Microsoft.Extensions.Logging;

namespace Kata04.Part3.Weather
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
        private readonly IKata04Config _kata04Config;

        public MinSpreadHandler(ILogger logger, IKata04Config kata04Config)
        {
            _logger = logger;
            _kata04Config = kata04Config;
        }

        public async Task<MinSpreadResponse> Handle(MinSpreadCommand request, CancellationToken cancellationToken)
        {
            var fileDesc = BuildFileDescription(_kata04Config.WeatherFilePath);

            TabularDataReader<WeatherData> reader = new TabularDataReader<WeatherData>(fileDesc);
            using (var stream = new StreamReader(_kata04Config.WeatherFilePath))
            {
                var result = await reader.ProcessFile(stream);

                return new MinSpreadResponse
                {
                    MinRangeDayNumber = result.FileData
                        .Select(w => new { Day = w.Day, Departure = w.MxT - w.MnT })
                        .OrderByDescending(m => m.Departure)
                        .First().Day
                };
            }
        }

        private static TabularFileDescription BuildFileDescription(string filePath)
        {
            return new BuildTabularFileDescription(filePath)
                .HasHeadings(true)
                .HasSummaryLine(true)
                .IgnoreBlankLines(true)
                .IgnoreSpecialLines(true)
                .AddField( "Day", typeof(int), 0, 4 )
                .AddField( "MxT", typeof(int), 4, 4 )
                .AddField( "MnT", typeof(int), 9, 5 )
                .AddField( "AvT", typeof(int), 15, 5 )
                .AddField( "HDDay", typeof(int), 20, 8 )
                .AddField( "AvDP", typeof(decimal), 28, 6 )
                .AddField( "OneHrP", typeof(int?), 34, 5 )
                .AddField( "TPcpn", typeof(decimal), 39, 6 )
                .AddField( "WxType", typeof(string), 45, 8 )
                .AddField( "PDir", typeof(int), 53, 4 )
                .AddField( "AvSP", typeof(decimal), 57, 5 )
                .AddField( "Dir", typeof(int), 62, 4 )
                .AddField( "MxS", typeof(int), 66, 4 )
                .AddField( "SkyC", typeof(decimal), 71, 4 )
                .AddField( "MxR", typeof(int), 75, 4 )
                .AddField( "MnR", typeof(int), 79, 4 )
                .AddField( "AvSLP", typeof(decimal), 83, 6)
                .Build();
        }
    }
}