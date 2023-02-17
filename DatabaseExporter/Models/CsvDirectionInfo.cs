// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace DatabaseExporter.Models
{
    public class CsvDirectionInfo : CsvInfo
    {
    }
    
    public sealed class CsvDirectionInfoMap : CsvClassMap<CsvDirectionInfo>
    {
        public CsvDirectionInfoMap()
        {
            // CsvRecord
            Map(m => m.Version).Index(0);
            Map(m => m.Id).Index(1);
            Map(m => m.Symbol).Index(2);
            // CsvEntity
            Map(m => m.Flags).Convert(m=>ConvertFlags(m.Value.EntityFlags)).Index(3);
            // CsvInfo
            Map(m => m.Name).Index(4);
        }
    }
}