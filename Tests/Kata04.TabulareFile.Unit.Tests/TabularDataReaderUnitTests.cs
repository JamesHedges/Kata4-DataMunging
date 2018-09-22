using System;
using System.IO;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Kata04.TabularFile.Unit.Tests
{
    public class TabularDataReaderTests
    {
        [Fact]
        public async Task ReadDataLines()
        {
            TestDataHelper dataHelper = new TestDataHelper();
            TabularDataReader<TestData> weatherDataReader = new TabularDataReader<TestData>(dataHelper.GetTestFileDescription());

            using (var stream = CreateMemoryReader(dataHelper.TestData))
            {
                var result = await weatherDataReader.ProcessFile(stream);

                result.FileData.Count.ShouldBe(dataHelper.TestDataLines);
            }
        }

        [Fact]
        public async Task ReadHeaderLine()
        {
            TestDataHelper dataHelper = new TestDataHelper();
            TabularDataReader<TestData> weatherDataReader = new TabularDataReader<TestData>(dataHelper.GetTestFileDescription());

            using (var stream = CreateMemoryReader(dataHelper.TestData))
            {
                var result = await weatherDataReader.ProcessFile(stream);

                result.HeaderText.ShouldBe(dataHelper.Header);
            }
        }

        [Fact]
        public async Task ReadSummaryLine()
        {
            TestDataHelper dataHelper = new TestDataHelper();
            TabularDataReader<TestData> weatherDataReader = new TabularDataReader<TestData>(dataHelper.GetTestFileDescription());

            using (var stream = CreateMemoryReader(dataHelper.TestData))
            {
                var result = await weatherDataReader.ProcessFile(stream);

                result.SummaryText.ShouldBe(dataHelper.Summary);
            }
        }

        [Fact]
        public async Task FileWithBlankLine_NoIgnoreBlankLines_ThrowsException()
        {
            TestDataHelper dataHelper = new TestDataHelper();
            var description = dataHelper.GetTestFileDescription(true, true, false, true);
            TabularDataReader<TestData> weatherDataReader = new TabularDataReader<TestData>(description);

            using (var stream = CreateMemoryReader(dataHelper.TestData))
            {
                Should.Throw<ArgumentOutOfRangeException>( () =>  weatherDataReader.ProcessFile(stream));
            }
        }

        [Fact]
        public async Task FileWithBlankLine_NoIgnoreSpecialLines_ThrowsException()
        {
            TestDataHelper dataHelper = new TestDataHelper();
            var description = dataHelper.GetTestFileDescription(true, true, true, false);
            TabularDataReader<TestData> weatherDataReader = new TabularDataReader<TestData>(description);

            using (var stream = CreateMemoryReader(dataHelper.TestData))
            {
                Should.Throw<ArgumentOutOfRangeException>( () =>  weatherDataReader.ProcessFile(stream));
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
    }
}
