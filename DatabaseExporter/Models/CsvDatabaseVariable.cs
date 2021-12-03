using CsvHelper.Configuration;

namespace DatabaseExporter.Models
{
    // ReSharper disable ClassNeverInstantiated.Global
    public class CsvDatabaseVariable
    {
        public int Version { get; set; }
        public string Symbol { get; set; } 
        public string Value { get; set; }
    }

    public sealed class CsvVariableMap : ClassMap<CsvDatabaseVariable>
    {
        public CsvVariableMap()
        {
            Map(m => m.Version).Index(0);
            Map(m => m.Symbol).Index(1);
            Map(m => m.Value).Index(2);
        }
    }
}