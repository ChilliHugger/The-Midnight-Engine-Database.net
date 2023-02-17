using CsvHelper.Configuration;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace DatabaseExporter.Models
{
    public class CsvVictory : CsvEntity
    {
        public int Priority { get; set; }
        public CsvId Mission { get; set; }
        public CsvId String { get; set; }
    }
    
    public sealed class CsvVictoryMap : ClassMap<CsvVictory>
    {
        public CsvVictoryMap()
        {
            // CsvRecord
            Map(m => m.Version).Index(0);
            Map(m => m.Id).Index(1);
            Map(m => m.Symbol).Index(2);
            // CsvEntity
            Map(m => m.Flags).Index(3);
            // CsvVictory
            Map(m => m.Priority).Index(4);
            Map(m => m.Mission.Symbol).Index(5);
            Map(m => m.String.Symbol).Index(6);
        }
    }
}