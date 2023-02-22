using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace DatabaseExporter.Converters
{
    public class MultiLineStringConverter : DefaultTypeConverter
    {
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value is string text)
            {
                return text.Replace("\n\r", "{crlf}")
                        .Replace("\n", "{cr}")
                        .Replace("\r", "{lf}");
            }
            return value.ToString();
        }
    }
}