using CsvHelper.Configuration;
using TME.Scenario.Default.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace DatabaseExporter.Models.Item
{
    public class CsvVictory : CsvEntity
    {
        public int Priority { get; set; }
        public CsvId Mission { get; set; }
        public CsvId String { get; set; }
    }
    
    public sealed class OutVictoryMap : ClassMap<IVictory>
    {
        public OutVictoryMap()
        {
            // CsvRecord
            Map().Constant(1).Index(0).Name("Version");
            Map(m => m.Id).Index(1);
            Map(m => m.Symbol).Index(2);
            // CsvEntity
            Map(m => m.Flags).Index(3);
            // CsvVictory
            Map(m => m.Priority).Index(4);
            Map(m => m.Mission).Index(5);
            Map(m => m.String)
                .TypeConverterOption.Format("StringId")
                .Index(6);
        }
    }
}