using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace DatabaseExporter.Converters
{
    public class FlagConverter<TEnum> : DefaultTypeConverter
    {
        
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if(text.ToLower().Trim()=="none") return 0;
            return 0;
        }

        public override string ConvertToString(object input, IWriterRow row, MemberMapData memberMapData)
        {
            switch (input)
            {
                case Enum enumValue:
                {
                    var temp = Convert.ToUInt32(enumValue, CultureInfo.InvariantCulture);
                    return ConvertToString(temp);
                }
                case uint value:
                    return ConvertToString(value);
                default:
                    return "";
            }
        }

        private string ConvertToString(uint value)
        {
            if (value == 0) return "";
            var results = GetAllSelectedItems<TEnum>(value);
            var output = string.Join("+", results);
            return output.ToUpper();
        }

        private IEnumerable<T> GetAllSelectedItems<T>(uint value)
        {
            return from object item in Enum.GetValues(typeof(T)) 
                let itemAsInt = Convert.ToUInt32(item, CultureInfo.InvariantCulture) 
                where itemAsInt != 0 
                where itemAsInt == (value & itemAsInt) 
                select (T) item;
        }
        
    }
}