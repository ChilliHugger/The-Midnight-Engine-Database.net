using TME.Scenario.Default.Flags;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace DatabaseExporter.Models
{
    public class CsvVictory : CsvEntity
    {
        public int Priority { get; set; }
        public CsvId Mission { get; set; }
        public CsvId String { get; set; }
        
        // for export
        public VictoryFlags VictoryFlags => (VictoryFlags) Flags;
    }
    
    public sealed class CsvVictoryMap : CsvClassMap<CsvVictory>
    {
        public CsvVictoryMap()
        {
            // CsvRecord
            Map(m => m.Version).Index(0);
            Map(m => m.Id).Index(1);
            Map(m => m.Symbol).Index(2);
            // CsvEntity
            Map(m => m.Flags).Convert(m=>ConvertFlags(m.Value.VictoryFlags)).Index(3);
            // CsvVictory
            Map(m => m.Priority).Index(4);
            Map(m => m.Mission.Symbol).Index(5).Name("Mission");
            Map(m => m.String.Symbol).Index(6).Name("String");
        }
    }
}