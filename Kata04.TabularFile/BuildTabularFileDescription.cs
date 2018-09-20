using System;
using System.Collections.Generic;

namespace Kata04.TabularFile
{
    public class BuildTabularFileDescription
    {
        private readonly string _filePath;
        private bool _hasHeading;
        private bool _hasSummary;
        private bool _ignoreBlankLines;
        private bool _ignoreSpecialLines;
        private readonly List<DataField> _fields;

        public BuildTabularFileDescription(string filePath)
        {
            _filePath = filePath;
            _fields = new List<DataField>();
        }

        public BuildTabularFileDescription HasHeadings(bool hasHeading)
        {
            _hasHeading = hasHeading;
            return this;
        }

        public BuildTabularFileDescription HasSummaryLine(bool hasSummary)
        {
            _hasSummary = hasSummary;
            return this;
        }

        public BuildTabularFileDescription IgnoreBlankLines(bool ignoreBlankLines)
        {
            _ignoreBlankLines = ignoreBlankLines;
            return this;
        }

        public BuildTabularFileDescription IgnoreSpecialLines(bool ignoreSpecialLines)
        {
            _ignoreSpecialLines = ignoreSpecialLines;
            return this;
        }

        public BuildTabularFileDescription AddField(string propertyName, Type dataType, int startIndex, int length)
        {
            _fields.Add(new DataField()
            {
                PropertyName = propertyName,
                DataType = dataType,
                StartIndex = startIndex,
                Length = length
            });
            return this;
        }

        public BuildTabularFileDescription AddField(string propertyName, string dataFieldName, Type dataType, int startIndex,
            int length)
        {
            _fields.Add(new DataField()
            {
                PropertyName = propertyName,
                DataFieldName = dataFieldName,
                DataType = dataType,
                StartIndex = startIndex,
                Length = length
            });
            return this;
        }

        public TabularFileDescription Build()
        {
            return new TabularFileDescription(_filePath, _hasHeading, _hasSummary, _ignoreBlankLines, _ignoreSpecialLines, _fields);
        }
    }
}