// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using CsvHelper.Configuration;
using DatabaseExporter.Converters;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.info;

namespace DatabaseExporter.Models.Info
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
    
    public sealed class OutRaceInfoMap : ClassMap<RaceInfo>
    {
        public OutRaceInfoMap()
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
            Map(m => m.DefaultSoldiersName).Index(5).Name("Default Soldiers Name");
            Map(m => m.Success).Index(6);
            Map(m => m.InitialMovement).Index(7).Name("Initial Movement");
            Map(m => m.DiagonalModifier).Index(8).Name("Diagonal Movement");
            Map(m => m.RidingMovementMultiplier).Index(9).Name("Riding Multiplier");
            Map(m => m.MovementMax).Index(10).Name("Movement Max");
            Map(m => m.BaseRestAmount).Index(11).Name("Rest Amount");
            Map(m => m.StrongholdStartups).Index(12).Name("Stronghold Startups");
            Map(m => m.MistTimeAffect).Index(13).Name("Mist Time Adjustment");
            Map(m => m.MistDespondencyAffect).Index(14).Name("Mist Despondency Adjustment");
            Map(m => m.BaseEnergyCost).Index(15).Name("Energy Amount");
            Map(m => m.BaseEnergyCostHorse).Index(16).Name("Energy Amount Riding");
        }
    }
}