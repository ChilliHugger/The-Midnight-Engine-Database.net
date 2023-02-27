// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using DatabaseExporter.Converters;
using TME.Scenario.ddr.Interfaces;
using TME.Scenario.ddr.Items;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.Items;
using TME.Serialize;
namespace DatabaseExporter.Models.Item
{
    public class CsvStronghold : CsvItem
    {
        [Name("Occupying Race")]
        public string OccupyingRace { get; set; }
        public string Race { get; set; }
        [Name("Type")]
        public string Type { get; set; }
        public uint Total { get; set; }
        public uint Min { get; set; }
        public uint Max { get; set; }
        [Name("Strategical Success")]
        public uint StrategicalSuccess { get; set; }
        [Name("Owner Success")]
        public uint OwnerSuccess { get; set; }
        [Name("Enemy Success")]
        public uint EnemySuccess { get; set; }
        public uint Influence { get; set; }
        public uint Respawn { get; set; }
        public string Occupier { get; set; }
        public string Owner { get; set; }
        public string Terrain { get; set; }

        // Revenge
        [Optional]
        public string Loyalty { get; set; }
        [Optional]
        public uint Energy { get; set; }

        public override Bundle ToBundle(CsvImportConverter converter)
        {
            return new Bundle {
                {nameof(Entity.Id), converter.ToId(EntityType.Stronghold,Id)},
                {nameof(Entity.Symbol), Symbol},
                {nameof(Entity.Flags), converter.ToFlags<EntityFlags>(Flags)},
                {nameof(Stronghold.Location), converter.ToLoc(Location)},
               
                {nameof(Stronghold.OccupyingRace), converter.ToEnum<Race>(OccupyingRace)},
                {nameof(Stronghold.Race), converter.ToEnum<Race>(Race)},
                {nameof(Stronghold.UnitType), converter.ToEnum<UnitType>(Type)},
                {nameof(Stronghold.Total), Total},
                {nameof(Stronghold.Min), Min},
                {nameof(Stronghold.Max), Max},
                {nameof(Stronghold.StrategicalSuccess), StrategicalSuccess},
                {nameof(Stronghold.OwnerSuccess), OwnerSuccess},
                {nameof(Stronghold.EnemySuccess), EnemySuccess},
                {nameof(Stronghold.Influence), Influence},
                {nameof(Stronghold.Respawn), Respawn},
                {nameof(Stronghold.Occupier), converter.ToEntity<ICharacter>(Occupier)},
                {nameof(Stronghold.Owner), converter.ToEntity<ICharacter>(Owner)},
                {nameof(Stronghold.Terrain), converter.ToEnum<Terrain>(Terrain)},
                {nameof(Stronghold.Killed), 0},
                {nameof(Stronghold.Lost), 0},
                {nameof(RevengeStronghold.Loyalty), converter.ToEnum<Race>(Loyalty)},
                {nameof(RevengeStronghold.Energy), Energy},
            };
        }
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
            else
            {
                Map().Index(19).Constant(0).Name("Energy");
                Map().Index(20).Constant("").Name("Loyalty");
            }
        }
    }
}