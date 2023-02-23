// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global

using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using DatabaseExporter.Converters;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.info;
using TME.Serialize;

namespace DatabaseExporter.Models.Info
{
    public class CsvRaceInfo : CsvInfo
    {
        [Name("Default Soldiers Name")]
        public string DefaultSoldiersName { get; set; }
        public uint Success { get; set; }
        [Name("Initial Movement")]
        public uint InitialMovement { get; set; }
        [Name("Diagonal Movement")]
        public uint DiagonalModifier { get; set; }
        [Name("Riding Multiplier")]
        public float RidingMultiplier { get; set; }
        [Name("Movement Max")]
        public uint MovementMax { get; set; }
        [Name("Rest Amount")]
        public uint BaseRestAmount { get; set; }
        [Name("Stronghold Startups")]
        public uint StrongholdStartups { get; set; }
        [Name("Mist Time Adjustment")]
        public int MistTimeAffect { get; set; }
        [Name("Mist Despondency Adjustment")]
        public int MistDespondencyAffect { get; set; }
        [Name("Energy Amount")]
        public int BaseEnergyCost { get; set; }
        [Name("Energy Amount Riding")]
        public int BaseEnergyCostHorse { get; set; }
        
        public override Bundle ToBundle(CsvImportConverter converter)
        {
            return new Bundle {
                {nameof(Entity.Id), converter.ToId(EntityType.RaceInfo,Id)},
                {nameof(Entity.Symbol), Symbol},
                {nameof(Entity.Flags), converter.ToFlags<EntityFlags>(Flags)},
                {nameof(RaceInfo.Name), Name},
                {nameof(RaceInfo.DefaultSoldiersName), DefaultSoldiersName},
                {nameof(RaceInfo.Success), Success},
                {nameof(RaceInfo.InitialMovement), InitialMovement},
                {nameof(RaceInfo.DiagonalModifier), DiagonalModifier},
                {nameof(RaceInfo.RidingMultiplier), (uint)(RidingMultiplier * RaceInfo.RidingMultiplierFactor)},
                {nameof(RaceInfo.MovementMax), MovementMax},
                {nameof(RaceInfo.BaseRestAmount), BaseRestAmount},
                {nameof(RaceInfo.StrongholdStartups), StrongholdStartups},
                {nameof(RaceInfo.MistTimeAffect), MistTimeAffect},
                {nameof(RaceInfo.MistDespondencyAffect), MistDespondencyAffect},
                {nameof(RaceInfo.BaseEnergyCost), BaseEnergyCost},
                {nameof(RaceInfo.BaseEnergyCostHorse), BaseEnergyCostHorse},
            };
        }
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