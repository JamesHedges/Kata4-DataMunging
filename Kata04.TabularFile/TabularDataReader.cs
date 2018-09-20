using System.IO;
using System.Threading.Tasks;

namespace Kata04.TabularFile
{
    public class TabularDataReader<T> where T : new()
    {

        private TabularFileDescription _fileDescription;
        private readonly FieldParser _fieldParser;

        public TabularDataReader(TabularFileDescription fileDescription)
        {
            _fileDescription = fileDescription;
            _fieldParser = new FieldParser(_fileDescription.DataFields);
        }

        public async Task<TabularFileData<T>> ProcessFile(StreamReader reader)
        {
            string line;
            TabularFileData<T> fileData = new TabularFileData<T>();
            bool isFirstLine = true;

            while ((line = await reader.ReadLineAsync()) != null)
            {
                if (IsHeaderLine(isFirstLine))
                {
                    isFirstLine = false;
                    fileData.HeaderText = ProcessHeaderLine(line);
                }
                else if (IsSummaryLine(reader))
                {
                    fileData.SummaryText = ProcessSummaryLine(line);
                }
                else
                {
                    if (!IsIgnored(line) && !IsIgnoredBlankLine(line))
                    {
                        fileData.AddData(ProcessTabularFileDataLine(line));
                    }
                }
            }

            return fileData;
        }

        private bool IsIgnoredBlankLine(string line)
        {
            return _fileDescription.IgnoreBlankLines && string.IsNullOrEmpty(line);
        }

        private bool IsIgnored(string line)
        {
            return _fileDescription.IgnoreSpecialLines && line.TrimStart().StartsWith("-----");
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

        private T ProcessTabularFileDataLine(string line)
        {
            return _fieldParser.ParseString<T>(line);
        }
    }
}