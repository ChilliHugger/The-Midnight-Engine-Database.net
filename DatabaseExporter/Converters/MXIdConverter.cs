using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using TME.Types;

namespace DatabaseExporter.Converters
{
    public class MXIdConverter : DefaultTypeConverter
    {
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value is MXId id)
            {
                return id.RawId.ToString();
            }
            return "";
        }
    }
}