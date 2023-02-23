using CsvHelper.Configuration;
using DatabaseExporter.Converters;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.Items;
using TME.Serialize;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace DatabaseExporter.Models.Item
{
    public class CsvRegiment : CsvItem
    {
        public string Race { get; set; } 
        public string Type { get; set; }
        public uint Total { get; set; }
        public string Target { get; set; } 
        public string Orders { get; set; }
        public uint Success { get; set; }
        public string Loyalty { get; set; }
        public uint Delay { get; set; }
        
        public override Bundle ToBundle(CsvImportConverter converter)
        {
            return new Bundle {
                {nameof(Entity.Id), converter.ToId(EntityType.Regiment,Id)},
                {nameof(Entity.Symbol), Symbol},
                {nameof(Entity.Flags), converter.ToFlags<RegimentFlags>(Flags)},
                {nameof(Regiment.Location), converter.ToLoc(Location)},
            };
        }
    }
    
    public sealed class OutRegimentMap : ClassMap<IRegiment>
    {
        public OutRegimentMap()
        {
            // CsvRecord
            Map().Constant(1).Index(0).Name("Version");
            Map(m => m.Id).Index(1);
            Map(m => m.Symbol).Index(2);
            // CsvEntity
            Map(m => m.Flags).Index(3);
            // CsvItem
            Map(m => m.Location).Index(4);
            // CsvRegiment
            Map(m => m.ArmyType).Ignore();
            Map(m => m.Race).Index(5);
            Map(m => m.UnitType).Index(6).Name("Type");
            Map(m => m.Total).Index(7);
            Map(m => m.Target).Index(8);
            Map(m => m.Orders).Index(9);
            Map(m => m.Success).Index(10);
            Map(m => m.LoyaltyLord).Index(11).Name("Loyalty");
            Map(m => m.Killed).Ignore();
            Map(m => m.LastLocation).Ignore();
            Map(m => m.Delay).Index(12);
        }
    }
}