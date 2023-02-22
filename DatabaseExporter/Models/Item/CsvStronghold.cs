using CsvHelper.Configuration;
using TME.Scenario.ddr.Interfaces;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace DatabaseExporter.Models.Item
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
        
        // Revenge
        public uint Energy { get; set; }

    }
    
    public sealed class OutStrongholdMap<T> : ClassMap<T>
        where T : IStronghold
    {
        public OutStrongholdMap()
        {
            // CsvRecord
            Map().Constant(1).Index(0).Name("Version");
            Map(m => m.Id).Index(1);
            Map(m => m.Symbol).Index(2);
            // CsvEntity
            Map(m => m.Flags).Index(3);
            // CsvItem
            Map(m => m.Location).Index(4);
            // CsvStronghold
            Map(m => m.OccupyingRace).Index(5).Name("Occupying Race");
            Map(m => m.Race).Index(6);
            Map(m => m.UnitType).Index(7).Name("Type");
            Map(m => m.Total).Index(8);
            Map(m => m.Min).Index(9);
            Map(m => m.Max).Index(10);
            Map(m => m.StrategicalSuccess).Index(11).Name("Strategical Success");
            Map(m => m.OwnerSuccess).Index(12).Name("Owner Success");
            Map(m => m.EnemySuccess).Index(13).Name("Enemy Success");
            Map(m => m.Influence).Index(14);
            Map(m => m.Respawn).Index(15);
            Map(m => m.Occupier).Index(16);
            Map(m => m.Owner).Index(17);
            Map(m => m.Terrain).Index(18);
            Map(m => m.Killed).Ignore();
            Map(m => m.Lost).Ignore();
            
            // ddr
            if (typeof(T) == typeof(IRevengeStronghold))
            {
                Map<IRevengeStronghold>(m => m.Energy).Index(19);
                Map<IRevengeStronghold>(m => m.Loyalty).Index(20);
            }
        }
    }
}