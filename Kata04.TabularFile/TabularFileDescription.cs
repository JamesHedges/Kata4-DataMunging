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
    }
}