using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using TME.Scenario.Default.Interfaces;

namespace DatabaseExporter.Converters
{
    public class EntityConverter<T> : DefaultTypeConverter
        where T : IEntity
    {
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value is T entity)
            {
                return entity.Symbol;
            }
            return "";
        }
        
    }
}