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
    public class CsvUnitInfo : CsvInfo
    {
        public uint Success { get; set; }
        [Name("Rest Modifier")]
        public uint BaseRestModifier { get; set; }
        
        public override Bundle ToBundle(CsvImportConverter converter)
        {
            return new Bundle {
                {nameof(Entity.Id), converter.ToId(EntityType.UnitInfo,Id)},
                {nameof(Entity.Symbol), Symbol},
                {nameof(Entity.Flags), converter.ToFlags<EntityFlags>(Flags)},
                {nameof(UnitInfo.Name), Name},
                {nameof(UnitInfo.Success), Success},
                {nameof(UnitInfo.BaseRestModifier), BaseRestModifier},
            };
        }
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