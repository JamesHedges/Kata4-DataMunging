using System;

namespace Kata04.TabularFile
{
    public class DataField
    {

        public string PropertyName { get; set; }
        string _dataFieldName;

        public string DataFieldName
        {
            get => string.IsNullOrEmpty(_dataFieldName) ? PropertyName : _dataFieldName;
            set => _dataFieldName = value;
        }

        public Type DataType { get; set; }
        public int StartIndex { get; set; }
        public int Length { get; set; }

        public int EndIndex => StartIndex + Length - 1;
    }
}
