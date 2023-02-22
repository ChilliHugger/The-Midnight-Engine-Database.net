using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using TME.Scenario.Default.Base;

namespace DatabaseExporter.Converters
{
    public class LocationConverter : DefaultTypeConverter
    {
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value is Loc loc)
            {
                return $"{loc.X},{loc.Y}";
            }

            return "";
        }
    }
}