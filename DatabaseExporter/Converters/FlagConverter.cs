using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;

namespace DatabaseExporter.Converters
{
    public class FlagConverter<TEnum> : DefaultTypeConverter
    {
        
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if(text.ToLower().Trim()=="none") return EntityFlags.None;
            return EntityFlags.None;
        }

        public override string ConvertToString(object input, IWriterRow row, MemberMapData memberMapData)
        {
            switch (input)
            {
                case Enum enumValue:
                {
                    var temp = Convert.ToUInt64(enumValue, CultureInfo.InvariantCulture);
                    return ConvertToString(temp);
                }
                case ulong ulongValue:
                    return ConvertToString(ulongValue);
                case uint uintValue:
                    return ConvertToString(uintValue);
                default:
                    return "";
            }
        }

        private string ConvertToString(ulong value)
        {
            if (value == 0) return "";
            var results = GetAllSelectedItems<TEnum>(value);
            var output = string.Join("+", results);
            return output.ToUpper();
        }

        private IEnumerable<T> GetAllSelectedItems<T>(ulong value)
        {
            return from object item in Enum.GetValues(typeof(T))
                let itemAsLong = Convert.ToUInt64(item, CultureInfo.InvariantCulture)
                where itemAsLong != 0
                where itemAsLong == (value & itemAsLong)
                select (T) item;
        }
        
    }
}