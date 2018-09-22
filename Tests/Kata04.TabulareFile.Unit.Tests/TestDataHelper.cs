namespace Kata04.TabularFile.Unit.Tests
{
    public class TestDataHelper
    {
        public int TestDataLines => 3;
        public string Header => "Header";
        public string Summary => "                                                     Summary  22    666.77";
        public bool HasIgnoreLine => true;
        
        public string[] TestData =
        {
            "Header",
            "                                                                         ",
            "",
            //"ID                                    Name                Age Amount",
            //          11111111112222222222333333333344444444445555555555666666666677777777778
            //012345678901234567890123456789012345678901234567890123456789012345678901234567890
            "    3c238804-cc87-4c2f-8bfb-263f572cc132             Tester1  22    111.22",
            "    3c238804-cc87-4c2f-8bfb-263f572cc132             Tester2  44    222.11",
            "    3c238804-cc87-4c2f-8bfb-263f572cc132             Tester3  33    333.44",
            "   -------------------------------------------------------",
            "                                                     Summary  22    666.77",
        };

        public TabularFileDescription GetTestFileDescription(bool hasHeading, bool hasSummary, bool ignoreBlankLines, bool ignoreSpecialLines)
        {
            return new BuildTabularFileDescription("in memeory")
                .HasHeadings(hasHeading)
                .HasSummaryLine(hasSummary)
                .IgnoreBlankLines(ignoreBlankLines)
                .IgnoreSpecialLines(ignoreSpecialLines)
                .AddField("Name", typeof(string), 0, 39)
                .AddField("Name", typeof(string), 40, 20)
                .AddField("Age", typeof(int), 60, 4)
                .AddField("Amount", typeof(decimal), 64, 10)
                .Build();
        }

        public TabularFileDescription GetTestFileDescription()
        {
            return GetTestFileDescription(true, true, true, true);
        }
    }
    }