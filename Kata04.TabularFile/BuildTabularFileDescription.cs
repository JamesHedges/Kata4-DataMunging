using System;
using System.Collections.Generic;

namespace Kata04.TabularFile
{
    public class BuildTabularFileDescription : IBuildTabularFileDescription
    {
        private string _filePath;
        private bool _hasHeading;
        private bool _hasSummary;
        private bool _ignoreBlankLines;
        private bool _ignoreSpecialLines;
        private List<DataField> _fields;

        public BuildTabularFileDescription(string filePath)
        {
            _filePath = filePath;
            _fields = new List<DataField>();
        }

        public IBuildTabularFileDescription HasHeadings(bool hasHeading)
        {
            _hasHeading = hasHeading;
            return this;
        }

        public IBuildTabularFileDescription HasSummaryLine(bool hasSummary)
        {
            _hasSummary = hasSummary;
            return this;
        }

        public IBuildTabularFileDescription IgnoreBlankLines(bool ignoreBlankLines)
        {
            _ignoreBlankLines = ignoreBlankLines;
            return this;
        }

        public IBuildTabularFileDescription IgnoreSpecialLines(bool ignoreSpecialLines)
        {
            _ignoreSpecialLines = ignoreSpecialLines;
            return this;
        }

        public IBuildTabularFileDescription AddField(string propertyName, Type dataType, int startIndex, int length)
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

        public IBuildTabularFileDescription AddField(string propertyName, string dataFieldName, Type dataType, int startIndex,
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