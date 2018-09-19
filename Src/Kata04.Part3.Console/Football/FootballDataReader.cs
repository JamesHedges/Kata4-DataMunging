using System.IO;
using System.Threading.Tasks;
using Kata04.TabularFile;

namespace Kata04.Part3.Football
{
    public class FootballDataReader
    {

        private FootballFileDescription _fileDescription;
        private readonly FieldParser _fieldParser;

        public FootballDataReader(FootballFileDescription fileDescription)
        {
            _fileDescription = fileDescription;
            _fieldParser = new FieldParser(_fileDescription.DataFields);
        }

        public async Task<FootballFileData> ProcessFile(StreamReader reader)
        {
            string line;
            FootballFileData footballFileData = new FootballFileData();
            bool isFirstLine = true;

            while((line = await reader.ReadLineAsync()) != null)
            {
                if (IsHeaderLine(isFirstLine))
                {
                    isFirstLine = false;
                    footballFileData.HeaderText = ProcessHeaderLine(line);
                }
                else if (IsSummaryLine(reader))
                {
                    footballFileData.SummaryText = ProcessSummaryLine(line);
                }
                else
                {
                    if (!IsIgnored(line))
                    {
                        footballFileData.AddFootballData(ProcessFootballData(line));
                    }
                }
            }

            return footballFileData;
        }

        private bool IsIgnored(string line)
        {
            return (string.IsNullOrEmpty(line) || line.TrimStart().StartsWith("----")) && _fileDescription.IgnoreBlankLines;
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

        private FootballData ProcessFootballData(string line)
        {
            return _fieldParser.ParseString<FootballData>(line);
        }
    }
}