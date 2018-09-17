using System;
using System.Collections.Generic;
using System.Text;

namespace Kata04.Tabular
{
    public class DataField
    {

        public string FieldName { get; set; }
        string _propertyName;

        public string PropertyName
        {
            get => string.IsNullOrEmpty(_propertyName) ? FieldName : _propertyName;
            set => _propertyName = value;
        }

        public Type DataType { get; set; }
        public int StartIndex { get; set; }
        public int Length { get; set; }

        public int EndIndex => StartIndex + Length - 1;
    }
}
