using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace DatabaseExporter.Models
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
        
        // for export
        public RegimentFlags RegimentFlags => (RegimentFlags) Flags;
    }
    
    public sealed class CsvRegimentMap : CsvClassMap<CsvRegiment>
    {
        public CsvRegimentMap()
        {
            // CsvRecord
            Map(m => m.Version).Index(0);
            Map(m => m.Id).Index(1);
            Map(m => m.Symbol).Index(2);
            // CsvEntity
            Map(m => m.Flags).Convert(m=>ConvertFlags(m.Value.RegimentFlags)).Index(3);
            // CsvItem
            Map(m => m.Location).Index(4);
            // CsvRegiment
            Map(m => m.ArmyType).Index(5);
            Map(m => m.Race).Index(6);
            Map(m => m.UnitType).Index(7);
            Map(m => m.Total).Index(8);
            Map(m => m.Target.Symbol).Index(9).Name("Target");
            Map(m => m.Orders).Index(10);
            Map(m => m.Success).Index(11);
            Map(m => m.LoyaltyLord.Symbol).Index(12).Name("Loyalty");
            Map(m => m.Killed).Index(13);
            Map(m => m.LastLocation).Index(14);
            Map(m => m.Delay).Index(15);
        }
    }
}