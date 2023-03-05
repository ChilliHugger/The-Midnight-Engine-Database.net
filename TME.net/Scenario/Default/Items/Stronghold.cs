using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;

namespace TME.Scenario.Default.Items
{
    public partial class Stronghold : Item, IStronghold
    {
        public Race OccupyingRace { get; internal set; } = Race.None;
        public Race Race { get; internal set; } = Race.None;
        public UnitType UnitType { get; internal set; } = UnitType.None;
        public uint Total { get; internal set; }
        public uint Min { get; internal set; }
        public uint Max { get; internal set; }
        public uint StrategicalSuccess { get; internal set; }
        public uint OwnerSuccess { get; internal set; }
        public uint EnemySuccess { get; internal set; }
        public uint Influence { get; internal set; }
        public uint Respawn { get; internal set; }
        public ICharacter? Occupier { get; internal set; }
        public ICharacter? Owner { get; internal set; }
        public Terrain Terrain { get; internal set; } = Terrain.None;
        public uint Killed { get; internal set; }
        public uint Lost { get; internal set; }

        // TODO: Check in battle code that this is correct and not Race.None
        public Race LoyaltyRace => Owner?.Race ?? OccupyingRace; 
        
        // Revenge
        public uint Energy { get; internal set; }
        public Race Loyalty => Occupier?.Race ?? Race.None;
        
        public Stronghold() : base(EntityType.Stronghold)
        {
        }
    }
}