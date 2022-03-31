using CsvHelper.Configuration;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace DatabaseExporter.Models
{
    public class CsvTerrainInfo : CsvInfo
    {
        public string Preposition { get; set; } 
        public string Description { get; set; }
        public uint Success { get; set; }
        public uint Visibility { get; set; }
        public uint Obstruction { get; set; }
        public int MovementCost { get; set; }
    }
    
    public sealed class CsvTerrainInfoMap : ClassMap<CsvTerrainInfo>
    {
        public CsvTerrainInfoMap()
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
            Map(m => m.Preposition).Index(5);
            Map(m => m.Description).Index(6);
            Map(m => m.Success).Index(7);
            Map(m => m.Visibility).Index(8);
            Map(m => m.Obstruction).Index(9);
            Map(m => m.MovementCost).Index(10);
        }
    }
}