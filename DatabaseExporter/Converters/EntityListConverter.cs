using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using TME.Scenario.Default.Interfaces;

namespace DatabaseExporter.Converters
{
    public class EntityListConverter<TList,TEntity>: DefaultTypeConverter
        where TList : IEnumerable<TEntity>
        where TEntity : IEntity
    {
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value is TList list)
            {
                var output = string.Join("|", list.Select(c => c.Symbol));
                return output;
            }
            return "";
        }
    }
}