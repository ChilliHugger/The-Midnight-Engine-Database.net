using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace DatabaseExporter.Converters
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class MultiLineStringConverter : DefaultTypeConverter
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

        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return text.Replace("{crlf}", "\n\r")
                .Replace("{cr}", "\n")
                .Replace("{lf}", "\r");
        }
    }
}