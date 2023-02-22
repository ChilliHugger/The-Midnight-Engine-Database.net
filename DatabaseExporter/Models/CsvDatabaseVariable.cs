using CsvHelper.Configuration;
using TME.Types;

namespace DatabaseExporter.Models
{
    // ReSharper disable ClassNeverInstantiated.Global
    public class CsvDatabaseVariable
    {
        public int Version { get; set; }
        public string Symbol { get; set; } 
        public string Value { get; set; }
    }

    public sealed class InVariableMap : ClassMap<CsvDatabaseVariable>
    {
        public InVariableMap()
        {
            Map(m => m.Version).Index(0);
            Map(m => m.Symbol).Index(1);
            Map(m => m.Value).Index(2);
        }
    }
    
    public sealed class OutVariableMap : ClassMap<DatabaseVariable>
    {
        public OutVariableMap()
        {
            Map().Constant(1).Index(0).Name("Version");
            Map(m => m.Symbol).Index(1);
            Map(m => m.Value).Index(2);
        }
    }
    
    
}