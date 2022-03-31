using CsvHelper.Configuration;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace DatabaseExporter.Models
{
    public class CsvCommandInfo : CsvInfo
    {
        public uint SuccessTime { get; set; }
        public uint FailureTime { get; set; }
    }
    
    public sealed class CsvCommandInfoMap : ClassMap<CsvCommandInfo>
    {
        public CsvCommandInfoMap()
        {
            // CsvRecord
            Map(m => m.Version).Index(0);
            Map(m => m.Id).Index(1);
            Map(m => m.Symbol).Index(2);
            // CsvEntity
            Map(m => m.Flags).Index(3);
            // CsvInfo
            Map(m => m.Name).Index(4);
            // CsvCommandInfo
            Map(m => m.SuccessTime).Index(5);
            Map(m => m.FailureTime).Index(6);
        }
    }
}