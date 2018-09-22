using Shouldly;
using Xunit;

namespace Kata04.TabularFile.Unit.Tests
{
    public class DataFieldTests
    {
        [Fact]
        public void DataFieldEndIndex_SetStartAndLength_ReturnsCorrectLength()
        {
            int expectedEndIndex = 9;
            int testStart = 0;
            int testLength = 10;

            DataField testField = new DataField()
            {
                StartIndex = testStart,
                Length = testLength
            };

            testField.EndIndex.ShouldBe(expectedEndIndex);
        }

        [Fact]
        public void DataFieldName_SetProperty_DataFieldNameSame()
        {
            string expectedFieldName = "TestProp";
            string testProperty = "TestProp";

            DataField testField = new DataField()
            {
                PropertyName = testProperty,
            };

            testField.DataFieldName.ShouldBe(expectedFieldName);
        }

        [Fact]
        public void DataFieldName_SetDataFieldName_DataFieldNameAsSet()
        {
            string testDataField = "TestField";
            string expectedFieldName = testDataField;
            string testProperty = "TestProp";

            DataField testField = new DataField()
            {
                PropertyName = testProperty,
                DataFieldName = testDataField
            };

            testField.DataFieldName.ShouldBe(expectedFieldName);
        }
    }
}