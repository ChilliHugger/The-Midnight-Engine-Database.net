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
    public class CsvTerrainInfo : CsvInfo
    {
        public string Preposition { get; set; } 
        public string Description { get; set; }
        public uint Success { get; set; }
        public uint Visibility { get; set; }
        public uint Obstruction { get; set; }
        public int MovementCost { get; set; }
        
        public override Bundle ToBundle(CsvImportConverter converter)
        {
            return new Bundle {
                {nameof(Entity.Id), converter.ToId(EntityType.TerrainInfo,Id)},
                {nameof(Entity.Symbol), Symbol},
                {nameof(Entity.Flags), converter.ToFlags<TerrainInfoFlags>(Flags)},
                {nameof(TerrainInfo.Name), Name},
                {nameof(TerrainInfo.Preposition), Preposition},
                {nameof(TerrainInfo.Description), Description},
                {nameof(TerrainInfo.Success), Success},
                {nameof(TerrainInfo.Visibility), Visibility},
                {nameof(TerrainInfo.Obstruction), Obstruction},
                {nameof(TerrainInfo.MovementCost), MovementCost},
            };
        }
    }
    
    public sealed class OutTerrainInfoMap : ClassMap<TerrainInfo>
    {
        public OutTerrainInfoMap()
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
            Map(m => m.Preposition).Index(5);
            Map(m => m.Description).Index(6);
            Map(m => m.Success).Index(7);
            Map(m => m.Visibility).Index(8);
            Map(m => m.Obstruction).Index(9);
            Map(m => m.MovementCost).Index(10);
        }
    }
}