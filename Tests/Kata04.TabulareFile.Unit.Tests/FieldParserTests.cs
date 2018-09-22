using System.Collections.Generic;
using Shouldly;
using Xunit;

namespace Kata04.TabularFile.Unit.Tests
{
    public class FieldParserTests
    {
        public class TestDataClass
        {
            public string StringColumn { get; set; }
            public int IntColumn { get; set; }
            public decimal DecimalColumn { get; set; }
        }

        private List<DataField> _testDataClass;

        public FieldParserTests()
        {
            _testDataClass = new List<DataField>
            {
                new DataField {PropertyName = "StringColumn", DataType = typeof(string), StartIndex = 0, Length = 5},
                new DataField {PropertyName = "IntColumn", DataType = typeof(int), StartIndex = 5, Length = 5},
                new DataField {PropertyName = "DecimalColumn", DataType = typeof(decimal), StartIndex = 10, Length = 5},
            };
        }

        [Fact]
        public void ParseString_HasAllFields_ReturnsObject()
        {
            string parseSource = "";

            TestDataClass testData = ParseTestData(parseSource);

            testData.ShouldNotBeNull();
        }

        [Fact]
        public void ParseString_HasStringField_ReturnsTestDataClassWithString()
        {
            string parseSource = "One            ";

            TestDataClass testData = ParseTestData(parseSource);

            testData.StringColumn.ShouldBe("One");
        }

        [Fact]
        public void ParseString_HasIntField_ReturnsTestDataClassWithInt()
        {
            string parseSource = "     2         ";

            TestDataClass testData = ParseTestData(parseSource);

            testData.IntColumn.ShouldBe(2);
        }

        [Fact]
        public void ParseString_HasDecimalField_ReturnsTestDataClassWithDecimal()
        {
            string parseSource = "          3.33 ";

            TestDataClass testData = ParseTestData(parseSource);

            testData.DecimalColumn.ShouldBe<decimal>(3.33m);
        }

        [Fact]
        public void ParseString_WithAllThreeColumnTypes_ReturnsParsedObject()
        {
            string parseSource = "One  2    3.33 ";

            TestDataClass testData = ParseTestData(parseSource);

            testData.StringColumn.ShouldBe("One");
            testData.IntColumn.ShouldBe(2);
            testData.DecimalColumn.ShouldBe<decimal>(3.33m);
        }

        [Fact]
        public void ParseString_WithThatIsTooShort_ReturnsParsedObject()
        {
            string parseSource = "One  2    3.33";

            TestDataClass testData = ParseTestData(parseSource);

            testData.StringColumn.ShouldBe("One");
            testData.IntColumn.ShouldBe(2);
            testData.DecimalColumn.ShouldBe<decimal>(3.33m);
        }

        [Fact]
        public void ParseString_WithThatIsTooLong_ReturnsParsedObject()
        {
            string parseSource = "One  2    3.33                      ";

            TestDataClass testData = ParseTestData(parseSource);

            testData.StringColumn.ShouldBe("One");
            testData.IntColumn.ShouldBe(2);
        }

        [Fact]
        public void ParseString_WithThatHasThreeFieldsTakeFirstAndThird_ReturnsParsedObject()
        {
            string parseSource = "One  Three2";
            List<DataField> twoColumnFields = new List<DataField>
            {
                new DataField {PropertyName = "StringColumn", DataType = typeof(string), StartIndex = 0, Length = 5},
                new DataField {PropertyName = "Spacer", DataFieldName = string.Empty, DataType = typeof(string), StartIndex = 5, Length = 5},
                new DataField {PropertyName = "IntColumn", DataType = typeof(int), StartIndex = 10, Length = 5},
            };
            FieldParser testParser = new FieldParser(twoColumnFields);

            TestDataClass testData = testParser.ParseString<TestDataClass>(parseSource);

            testData.StringColumn.ShouldBe("One");
            testData.IntColumn.ShouldBe(2);
            testData.DecimalColumn.ShouldBe<decimal>(0m);
        }

        private TestDataClass ParseTestData(string parseSource)
        {
            FieldParser parser = new FieldParser(_testDataClass);

            return parser.ParseString<TestDataClass>(parseSource);
        }
    }
}