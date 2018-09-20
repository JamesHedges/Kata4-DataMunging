using System;
using System.Collections.Generic;

namespace Kata04.TabularFile
{
    public class TabularFileDescription
    {
        private readonly List<DataField> _dataFields;
        private List<DataField> _fields;

        //private FieldParser _fieldParser;

        public  TabularFileDescription(string filePath, bool hasHeading, bool hasSummary, bool ignoreBlankLines, bool ignoreSpecialLines, List<DataField> fields)
        {
            FilePath = filePath;
            HasHeadings = hasHeading;
            HasSummaryLine = hasSummary;
            IgnoreBlankLines = ignoreBlankLines;
            IgnoreSpecialLines = ignoreSpecialLines;
            _dataFields = fields;
       }

        public IReadOnlyList<DataField> DataFields => _dataFields ?? new List<DataField>();

        public bool HasHeadings { get; private set; }
        public bool HasSummaryLine { get; private set; }
        public bool IgnoreBlankLines { get; private set; }
        public bool IgnoreSpecialLines { get; private set; }
        public string FilePath { get; }

        public void AddField(string propertyName, Type dataType, int startIndex, int length)
        {
            _dataFields.Add(new DataField
            {
                PropertyName = propertyName,
                DataType = dataType,
                StartIndex = startIndex,
                Length = length
            });
        }

        public void AddField(string propertyName, string dataFieldName, Type dataType, int startIndex, int length)
        {
            _dataFields.Add(new DataField
            {
                DataFieldName = dataFieldName,
                PropertyName = propertyName,
                DataType = dataType,
                StartIndex = startIndex,
                Length = length
            });
        }



        //private List<DataField> BuildDataMap()
        //{
        //    return new List<DataField>
        //    {
        //        { new DataField { DataFieldName = "Place", PropertyName = "Place", DataType = typeof(int), StartIndex = 0, Length = 5}},
        //        { new DataField { DataFieldName = "Team", DataType = typeof(string), StartIndex = 7, Length = 15}},
        //        { new DataField { DataFieldName = "P", PropertyName = "P", DataType = typeof(int), StartIndex = 23, Length = 6}},
        //        { new DataField { DataFieldName = "W", PropertyName = "Win", DataType = typeof(int), StartIndex = 29, Length = 4}},
        //        { new DataField { DataFieldName = "L", PropertyName = "Loss", DataType = typeof(int), StartIndex = 33, Length = 4}},
        //        { new DataField { DataFieldName = "D", PropertyName = "D", DataType = typeof(int), StartIndex = 37, Length = 5}},
        //        { new DataField { DataFieldName = "F", PropertyName = "For", DataType = typeof(int), StartIndex = 42, Length = 4}},
        //        { new DataField { DataFieldName = "A", PropertyName = "Against", DataType = typeof(int), StartIndex = 48, Length = 4}},
        //        { new DataField { DataFieldName = "Pts", PropertyName = "Points", DataType = typeof(int), StartIndex = 54, Length = 4}},
        //    };
        //}
    }
}