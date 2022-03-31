using CsvHelper.Configuration;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace DatabaseExporter.Models
{
    public class CsvUnitInfo : CsvInfo
    {
        public uint Success { get; set; }
        public uint BaseRestModifier { get; set; }
    }
    
    public sealed class CsvUnitInfoMap : ClassMap<CsvUnitInfo>
    {
        public CsvUnitInfoMap()
        {
            // CsvRecord
            Map(m => m.Version).Index(0);
            Map(m => m.Id).Index(1);
            Map(m => m.Symbol).Index(2);
            // CsvEntity
            Map(m => m.Flags).Index(3);
            // CsvInfo
            Map(m => m.Name).Index(4);
            // CsvRaceInfo
            Map(m => m.Success).Index(5);
            Map(m => m.BaseRestModifier).Index(6);
        }
    }
}