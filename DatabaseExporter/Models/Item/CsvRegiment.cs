using CsvHelper.Configuration;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace DatabaseExporter.Models.Item
{
    public class CsvRegiment : CsvItem
    {
        public ArmyType ArmyType { get; set; } 
        public Race Race { get; set; } 
        public UnitType UnitType { get; set; }
        public uint Total { get; set; }
        public CsvId Target { get; set; } 
        public Orders Orders { get; set; }
        public uint Success { get; set; }
        public CsvId LoyaltyLord { get; set; }
        public uint Killed { get; set; }
        public Loc LastLocation { get; set; } 
        public uint Delay { get; set; }
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
            Map(m => m.Target.Symbol).Index(8).Name("Target");
            Map(m => m.Orders).Index(9);
            Map(m => m.Success).Index(10);
            Map(m => m.LoyaltyLord).Index(11).Name("Loyalty");
            Map(m => m.Killed).Ignore();
            Map(m => m.LastLocation).Ignore();
            Map(m => m.Delay).Index(12);
        }
    }
}