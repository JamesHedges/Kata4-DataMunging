using Xunit;
using Shouldly;
using Kata04.Weather;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Kata04.Tests
{
    public class WeatherDataReaderUnitTests
    {
        [Fact]
        public async Task ReadOneLine()
        {
            string[] data = 
            {
                "Header",
                "",
                //"  Dy MxT   MnT   AvT   HDDay  AvDP 1HrP TPcpn WxType PDir AvSp Dir MxS SkyC MxR MnR AvSLP",
                "   1  88    59    74          53.8       0.00 F       280  9.6 270  17  1.6  93 23 1004.5",
                "   2  79    63    71          46.5       0.00         330  8.7 340  23  3.3  70 28 1004.5",
                //"  mo  82.9  60.5  71.7    16  58.8       0.00              6.9          5.3"
                "Footer"
            };

            WeatherDataReader weatherDataReader = new WeatherDataReader(GetTestFileDescription());

            using (var stream = CreateMemoryReader(data))
            {
                var result = await weatherDataReader.ProcessFile(stream);

                result.WeatherData.Count().ShouldBe(2);
                result.HeaderText.ShouldBe("Header");
                result.SummaryText.ShouldBe("Footer");
            }
        }

        private StreamReader CreateMemoryReader(string[] data)
        {
            var memStream = new MemoryStream();
            var writer = new StreamWriter(memStream);
            foreach(var dat in data)
            {
                writer.Write(dat.ToCharArray());
                writer.Write('\n');
            }
            writer.Flush();
            memStream.Position = 0;

            return new StreamReader(memStream, System.Text.Encoding.UTF8, true);
        }

        private WeatherFileDescription GetTestFileDescription()
        {
            var fileDesc = new WeatherFileDescription("in memeory");
            return fileDesc;
        }
    }
}
