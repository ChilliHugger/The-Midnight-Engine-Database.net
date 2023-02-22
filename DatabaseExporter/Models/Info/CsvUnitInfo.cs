// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using CsvHelper.Configuration;
using TME.Scenario.Default.info;

namespace DatabaseExporter.Models.Info
{
    public class CsvUnitInfo : CsvInfo
    {
        public uint Success { get; set; }
        public uint BaseRestModifier { get; set; }
    }
    
    public sealed class OutUnitInfoMap : ClassMap<UnitInfo>
    {
        public OutUnitInfoMap()
        {
            // CsvRecord
            Map().Constant(1).Index(0).Name("Version");
            Map(m => m.Id).Index(1);
            Map(m => m.Symbol).Index(2);
            // CsvEntity
            Map(m => m.Flags).Index(3);
            // CsvInfo
            Map(m => m.Name).Index(4);
            // CsvRaceInfo
            Map(m => m.Success).Index(5);
            Map(m => m.BaseRestModifier).Index(6).Name("Rest Modifier");
        }
    }
}