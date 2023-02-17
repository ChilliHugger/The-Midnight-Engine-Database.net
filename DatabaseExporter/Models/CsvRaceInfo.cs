// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace DatabaseExporter.Models
{
    public class CsvRaceInfo : CsvInfo
    {
        public string DefaultSoldiersName { get; set; }
        public uint Success { get; set; }
        public uint InitialMovement { get; set; }
        public uint DiagonalModifier { get; set; }
        public float RidingMultiplier { get; set; }
        public uint MovementMax { get; set; }
        public uint BaseRestAmount { get; set; }
        public uint StrongholdStartups { get; set; }
        public int MistTimeAffect { get; set; }
        public int MistDespondencyAffect { get; set; }
        public int BaseEnergyCost { get; set; }
        public int BaseEnergyCostHorse { get; set; }
    }
    
    public sealed class CsvRaceInfoMap : CsvClassMap<CsvRaceInfo>
    {
        public CsvRaceInfoMap()
        {
            // CsvRecord
            Map(m => m.Version).Index(0);
            Map(m => m.Id).Index(1);
            Map(m => m.Symbol).Index(2);
            // CsvEntity
            Map(m => m.Flags).Convert(m=>ConvertFlags(m.Value.EntityFlags)).Index(3);
            // CsvInfo
            Map(m => m.Name).Index(4);
            // CsvRaceInfo
            Map(m => m.DefaultSoldiersName).Index(5);
            Map(m => m.Success).Index(6);
            Map(m => m.InitialMovement).Index(7);
            Map(m => m.DiagonalModifier).Index(8);
            Map(m => m.RidingMultiplier).Index(9);
            Map(m => m.MovementMax).Index(10);
            Map(m => m.BaseRestAmount).Index(11);
            Map(m => m.StrongholdStartups).Index(12);
            Map(m => m.MistTimeAffect).Index(13);
            Map(m => m.MistDespondencyAffect).Index(14);
            Map(m => m.BaseEnergyCost).Index(15);
            Map(m => m.BaseEnergyCostHorse).Index(16);
        }
    }
}