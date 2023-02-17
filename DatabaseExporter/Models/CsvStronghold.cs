using CsvHelper.Configuration;
using TME.Scenario.Default.Enums;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace DatabaseExporter.Models
{
    public class CsvStronghold : CsvItem
    {
        public Race OccupyingRace { get; set; }
        public Race Race { get; set; }
        public UnitType UnitType { get; set; }
        public uint TotalTroops { get; set; }
        public uint MinTroops { get; set; }
        public uint MaxTroops { get; set; }
        public uint StrategicalSuccess { get; set; }
        public uint OwnerSuccess { get; set; }
        public uint EnemySuccess { get; set; }
        public uint Influence { get; set; }
        public uint Respawn { get; set; }
        public CsvId Occupier { get; set; }
        public CsvId Owner { get; set; }
        public Terrain Terrain { get; set; }
        public uint Killed { get; set; }
        public uint Lost { get; set; }
    }
    
    public sealed class CsvStrongholdMap : ClassMap<CsvStronghold>
    {
        public CsvStrongholdMap()
        {
            // CsvRecord
            Map(m => m.Version).Index(0);
            Map(m => m.Id).Index(1);
            Map(m => m.Symbol).Index(2);
            // CsvEntity
            Map(m => m.Flags).Index(3);
            // CsvItem
            Map(m => m.Location).Index(4);
            // CsvStronghold
            Map(m => m.OccupyingRace).Index(5);
            Map(m => m.Race).Index(6);
            Map(m => m.UnitType).Index(7);
            Map(m => m.TotalTroops).Index(8);
            Map(m => m.MinTroops).Index(9);
            Map(m => m.MaxTroops).Index(10);
            Map(m => m.StrategicalSuccess).Index(11);
            Map(m => m.OwnerSuccess).Index(12);
            Map(m => m.EnemySuccess).Index(13);
            Map(m => m.Influence).Index(14);
            Map(m => m.Respawn).Index(15);
            Map(m => m.Occupier.Symbol).Index(16).Name("Occupier");
            Map(m => m.Owner.Symbol).Index(17).Name("Owner");
            Map(m => m.Terrain).Index(18);
            Map(m => m.Killed).Index(19);
            Map(m => m.Lost).Index(20);
        }
    }
}