using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Kata04.Weather
{
    public class FieldParser
    {
        private readonly IReadOnlyList<DataField> _fields;

        public FieldParser(IReadOnlyList<DataField> fields)
        {
            _fields = fields;
        }

        public T ParseString<T>(string parseString) where T: new()
        {
            T target = new T();

            var paddedParseString = parseString.PadRight(_fields.Sum(f => f.Length));

            foreach(var field in _fields)
            {
                string fieldValue = ParseField(paddedParseString, field);
                SetFieldValue(target, field, fieldValue);
            }
            return target;
        }

        private void SetFieldValue<T>(T target, DataField field, string fieldValue) where T : new()
        {
            if (string.IsNullOrEmpty(fieldValue))
                return;

            Type tType = typeof(T);
            PropertyInfo tInfo;

            tInfo = tType.GetProperty(field.PropertyName);
            if (tInfo != null && (tInfo.MemberType == MemberTypes.Property || tInfo.MemberType == MemberTypes.Field))
            {
                var value = Convert.ChangeType(fieldValue, tInfo.PropertyType);
                tInfo.SetValue(target, value);
            }
        }

        private string ParseField(string parseString, DataField field)
        {
            return parseString.Substring(field.StartIndex, field.Length).Trim();
        }
    }
}
