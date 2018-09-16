using System.Collections.Generic;

namespace Kata04.Weather
{
    public class WeatherFileDescription
    {
        private readonly List<DataField> _dataFields;
        private FieldParser _fieldParser;

        public WeatherFileDescription(string filePath)
        {
            FilePath = filePath;
            _dataFields = BuildDataMap();
        }

        public IReadOnlyList<DataField> DataFields => _dataFields ?? new List<DataField>();

        public bool HasHeadings => true;
        public bool HasSummaryLine => true;
        public bool IgnoreBlankLines => true;
        public string FilePath { get; }

        private List<DataField> BuildDataMap()
        {
            return new List<DataField>
            {
                { new DataField { FieldName = "Day", DataType = typeof(int), StartIndex = 0, Length = 4}},
                { new DataField { FieldName = "MxT", DataType = typeof(int), StartIndex = 4, Length = 4}},
                { new DataField { FieldName = "MnT", DataType = typeof(int), StartIndex = 8, Length = 6}},
                { new DataField { FieldName = "AvT", DataType = typeof(int), StartIndex = 14, Length = 6}},
                { new DataField { FieldName = "HDDay", DataType = typeof(int), StartIndex = 20, Length = 8}},
                { new DataField { FieldName = "AvDP", DataType = typeof(decimal), StartIndex = 28, Length = 6}},
                { new DataField { FieldName = "OneHrP", DataType = typeof(int?), StartIndex = 34, Length = 5}},
                { new DataField { FieldName = "TPcpn", DataType = typeof(decimal), StartIndex = 39, Length = 6}},
                { new DataField { FieldName = "WxType", DataType = typeof(string), StartIndex = 45, Length = 8}},
                { new DataField { FieldName = "PDir", DataType = typeof(int), StartIndex = 53, Length = 4}},
                { new DataField { FieldName = "AvSP", DataType = typeof(decimal), StartIndex = 57, Length = 5}},
                { new DataField { FieldName = "Dir", DataType = typeof(int), StartIndex = 62, Length = 4}},
                { new DataField { FieldName = "MxS", DataType = typeof(int), StartIndex = 66, Length = 4}},
                { new DataField { FieldName = "SkyC", DataType = typeof(decimal), StartIndex = 70, Length = 5}},
                { new DataField { FieldName = "MxR", DataType = typeof(int), StartIndex = 75, Length = 4}},
                { new DataField { FieldName = "MnR", DataType = typeof(int), StartIndex = 79, Length = 4}},
                { new DataField { FieldName = "AvSLP", DataType = typeof(decimal), StartIndex = 83, Length = 6}}
            };
        }
    }
}