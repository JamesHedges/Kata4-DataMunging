using System.Collections.Generic;
using Kata04.Tabular;

namespace Kata04.Football
{
    public class FootballFileDescription
    {
        private readonly List<DataField> _dataFields;
        private FieldParser _fieldParser;

        public FootballFileDescription(string filePath)
        {
            FilePath = filePath;
            _dataFields = BuildDataMap();
        }

        public IReadOnlyList<DataField> DataFields => _dataFields ?? new List<DataField>();

        public bool HasHeadings => true;
        public bool HasSummaryLine => false;
        public bool IgnoreBlankLines => true;
        public string FilePath { get; }

        private List<DataField> BuildDataMap()
        {
            return new List<DataField>
            {
                { new DataField { FieldName = "Place", PropertyName = "Place", DataType = typeof(int), StartIndex = 0, Length = 5}},
                { new DataField { FieldName = "Team", DataType = typeof(string), StartIndex = 7, Length = 15}},
                { new DataField { FieldName = "P", PropertyName = "P", DataType = typeof(int), StartIndex = 23, Length = 6}},
                { new DataField { FieldName = "W", PropertyName = "Win", DataType = typeof(int), StartIndex = 29, Length = 4}},
                { new DataField { FieldName = "L", PropertyName = "Loss", DataType = typeof(int), StartIndex = 33, Length = 4}},
                { new DataField { FieldName = "D", PropertyName = "D", DataType = typeof(int), StartIndex = 37, Length = 5}},
                { new DataField { FieldName = "F", PropertyName = "For", DataType = typeof(int), StartIndex = 42, Length = 4}},
                { new DataField { FieldName = "A", PropertyName = "Against", DataType = typeof(int), StartIndex = 48, Length = 4}},
                { new DataField { FieldName = "Pts", PropertyName = "Points", DataType = typeof(int), StartIndex = 54, Length = 4}},
            };
        }
    }
}