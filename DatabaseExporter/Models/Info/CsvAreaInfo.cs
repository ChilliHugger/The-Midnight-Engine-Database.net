// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global

using CsvHelper.Configuration;
using DatabaseExporter.Converters;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.info;
using TME.Serialize;

namespace DatabaseExporter.Models.Info
{
    public class CsvAreaInfo : CsvInfo
    {
        public string Prefix { get;  set; }
        
        public override Bundle ToBundle(CsvImportConverter converter)
        {
            return new Bundle {
                {nameof(Entity.Id), converter.ToId(EntityType.AreaInfo,Id)},
                {nameof(Entity.Symbol), Symbol},
                {nameof(Entity.Flags), converter.ToFlags<EntityFlags>(Flags)},
                {nameof(AreaInfo.Name), Name},
                {nameof(AreaInfo.Prefix), Prefix}
            };
        }
    }
    
    public sealed class OutAreaInfoMap : ClassMap<AreaInfo>
    {
        public OutAreaInfoMap()
        {
            // Record
            Map().Constant(1).Name("Version");
            Map(m => m.Id).Index(1);
            Map(m => m.Symbol).Index(2);
            // Entity
            Map(m => m.Flags).Index(3);
            // Info
            Map(m => m.Name).Index(4);
            // AreaInfo
            Map(m => m.Prefix).Index(5);
        }
    }
}