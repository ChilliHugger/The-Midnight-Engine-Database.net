using AutoMapper.Configuration;
using TME.Scenario.Default.Interfaces;

namespace DatabaseExporter.Mapping
{
    public abstract class MappingConfiguration : MapperConfigurationExpression
    {
        protected const int CsvExportVersion = 1;

        protected static string GetSymbol(IEntity node)
        {
            return node?.Symbol ?? "";
        }

        // protected static string ConvertString(string value)
        // {
        //     return value.Replace("\n\r", "{crlf}")
        //             .Replace("\n", "{cr}")
        //             .Replace("\r", "{lf}")
        //         ;
        // }
    }
}
