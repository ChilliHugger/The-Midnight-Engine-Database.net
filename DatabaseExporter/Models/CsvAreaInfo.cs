// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace DatabaseExporter.Models
{
    public class CsvAreaInfo : CsvInfo
    {
        public string Prefix { get; set; } 
    }
    
    public sealed class CsvAreaInfoMap : CsvClassMap<CsvAreaInfo>
    {
        public CsvAreaInfoMap()
        {
            // CsvRecord
            Map(m => m.Version).Index(0);
            Map(m => m.Id).Index(1);
            Map(m => m.Symbol).Index(2);
            // CsvEntity
            Map(m => m.Flags).Convert(m=>ConvertFlags(m.Value.EntityFlags)).Index(3);
            // CsvInfo
            Map(m => m.Name).Index(4);
            // CsvAreaInfo
            Map(m => m.Prefix).Index(5);
        }
    }
}