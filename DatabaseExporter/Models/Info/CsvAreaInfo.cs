// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using CsvHelper.Configuration;
using DatabaseExporter.Converters;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.info;

namespace DatabaseExporter.Models.Info
{
    public class CsvAreaInfo : CsvInfo
    {
        public string Prefix { get; set; } 
    }
    
    public sealed class InAreaInfoMap : ClassMap<CsvAreaInfo>
    {
        public InAreaInfoMap()
        {
            // CsvRecord
            Map(m => m.Id).Index(0).Name("Version");
            Map(m => m.Id).Index(1);
            Map(m => m.Symbol).Index(2);
            // CsvEntity
            Map(m => m.Flags).Index(3).Name("Flags");
            // CsvInfo
            Map(m => m.Name).Index(4);
            // CsvAreaInfo
            Map(m => m.Prefix).Index(5);
        }
    }
    
    public sealed class OutAreaInfoMap : ClassMap<AreaInfo>
    {
        public OutAreaInfoMap()
        {
            // CsvRecord
            Map().Constant(1).Name("Version");
            Map(m => m.Id).Index(1);
            Map(m => m.Symbol).Index(2);
            // CsvEntity
            Map(m => m.Flags).Index(3);
            // CsvInfo
            Map(m => m.Name).Index(4);
            // CsvAreaInfo
            Map(m => m.Prefix).Index(5);
        }
    }
}