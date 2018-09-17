using Xunit;
using Shouldly;
using Kata04.Football;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Kata04.Tests
{
    public class FootballDataReaderUnitTests
    {
        [Fact]
        public async Task ReadOneLine()
        {
            string[] data =
            {
                "Header",
                //"       Team            P     W    L   D    F      A     Pts",
                "    1. Arsenal         38    26   9   3    79  -  36    87",
                "    9. Tottenham       38    14   8  16    49  -  53    50",
                "   -------------------------------------------------------",
                "   20. Leicester       38     5  13  20    30  -  64    28",
            };

            FootballDataReader weatherDataReader = new FootballDataReader(GetTestFileDescription());

            using (var stream = CreateMemoryReader(data))
            {
                var result = await weatherDataReader.ProcessFile(stream);

                result.FootballData.Count().ShouldBe(3);
                result.HeaderText.ShouldBe("Header");
            }
        }

        private StreamReader CreateMemoryReader(string[] data)
        {
            var memStream = new MemoryStream();
            var writer = new StreamWriter(memStream);
            foreach (var dat in data)
            {
                writer.Write(dat.ToCharArray());
                writer.Write('\n');
            }
            writer.Flush();
            memStream.Position = 0;

            return new StreamReader(memStream, System.Text.Encoding.UTF8, true);
        }

        private FootballFileDescription GetTestFileDescription()
        {
            var fileDesc = new FootballFileDescription("in memeory");
            return fileDesc;
        }
    }
}

