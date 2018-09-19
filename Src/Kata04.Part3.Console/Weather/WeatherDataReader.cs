using System.IO;
using System.Threading.Tasks;
using Kata04.TabularFile;

namespace Kata04.Part3.Weather
{
    public class WeatherDataReader
    {

        private WeatherFileDescription _fileDescription;
        private readonly FieldParser _fieldParser;

        public WeatherDataReader(WeatherFileDescription fileDescription)
        {
            _fileDescription = fileDescription;
            _fieldParser = new FieldParser(_fileDescription.DataFields);
        }

        public async Task<WeatherFileData> ProcessFile(StreamReader reader)
        {
            string line;
            WeatherFileData weatherFileData = new WeatherFileData();
            bool isFirstLine = true;

            while((line = await reader.ReadLineAsync()) != null)
            {
                if (IsHeaderLine(isFirstLine))
                {
                    isFirstLine = false;
                    weatherFileData.HeaderText = ProcessHeaderLine(line);
                }
                else if (IsSummaryLine(reader))
                {
                    weatherFileData.SummaryText = ProcessSummaryLine(line);
                }
                else
                {
                    if (!IsIgnored(line))
                    {
                        weatherFileData.AddWeatherData(ProcessWeatherData(line));
                    }
                }
            }

            return weatherFileData;
        }

        private bool IsIgnored(string line)
        {
            return string.IsNullOrEmpty(line) && _fileDescription.IgnoreBlankLines;
        }

        private bool IsHeaderLine(bool isFirstLine)
        {
            return isFirstLine && _fileDescription.HasHeadings;
        }

        private bool IsSummaryLine(StreamReader reader)
        {
            return _fileDescription.HasSummaryLine && reader.Peek() == -1;
        }

        private string ProcessHeaderLine(string line)
        {
            return line;
        }

        private string ProcessSummaryLine(string line)
        {
            return line;
        }

        private WeatherData ProcessWeatherData(string line)
        {
            return _fieldParser.ParseString<WeatherData>(line);
        }
    }
}