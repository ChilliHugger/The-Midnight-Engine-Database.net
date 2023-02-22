using System.Collections.Generic;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using TME.Scenario.Default.Interfaces;

namespace DatabaseExporter.Converters
{
    public class UnitListConverter : DefaultTypeConverter
    {
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value is IList<IUnit> list)
            {
                return "";
            }
            return "";
        }
    }
}