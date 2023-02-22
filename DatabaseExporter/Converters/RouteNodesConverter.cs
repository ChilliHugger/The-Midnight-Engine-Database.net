using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using TME.Extensions;
using TME.Scenario.Default.Interfaces;

namespace DatabaseExporter.Converters
{
    public class RouteNodesConverter : DefaultTypeConverter
    {
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value is IRouteNodes routeNodes)
            {
                var result = routeNodes.Nodes
                    .Select(r => r?.Symbol)
                    .WhereNotNull();
                return string.Join(",", result);
            }
            return "";
        }
    }
}