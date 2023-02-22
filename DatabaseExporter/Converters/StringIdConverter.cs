using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using TME.Interfaces;
using TME.Scenario.Default.Enums;
using TME.Types;

namespace DatabaseExporter.Converters
{
    public class StringIdConverter : DefaultTypeConverter
    {
        private readonly IStrings _strings;

        public StringIdConverter(IStrings strings)
        {
            _strings = strings;
        }
        
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if (memberMapData.TypeConverterOptions.Formats?.Contains("StringId") == true)
            {
                if (value is uint id)
                {
                    return id == 0
                        ? ""
                        : _strings.GetById(new MXId(EntityType.String, id))?.Symbol;
                }
            }
            return value.ToString();
        }
    }
}