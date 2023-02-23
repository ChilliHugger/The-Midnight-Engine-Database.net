using CsvHelper.Configuration;
using DatabaseExporter.Converters;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace DatabaseExporter.Models.Item
{
    public class CsvVictory : CsvEntity
    {
        public int Priority { get; set; }
        public string Mission { get; set; }
        public string String { get; set; }
        
        public override Bundle ToBundle(CsvImportConverter converter)
        {
            return new Bundle {
                {nameof(Entity.Id), converter.ToId(EntityType.Victory,Id)},
                {nameof(Entity.Symbol), Symbol},
                {nameof(Entity.Flags), converter.ToFlags<VictoryFlags>(Flags)},
            };
        }
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