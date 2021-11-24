using System.Diagnostics.CodeAnalysis;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;

namespace TME.Scenario.Default.Items
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public partial class Stronghold : Item, IStronghold
    {
        public Race OccupyingRace { get; internal set; } = Race.None;
        public Race Race { get; internal set; } = Race.None;
        public UnitType UnitType { get; internal set; } = UnitType.None;
        public uint Total { get; internal set; }
        public uint Min { get; private set; }
        public uint Max { get; private set; }
        public uint StrategicalSuccess { get; private set; }
        public uint OwnerSuccess { get; private set; }
        public uint EnemySuccess { get; private set; }
        public uint Influence { get; private set; }
        public uint Respawn { get; private set; }
        public ILord? Occupier { get; internal set; }
        public ILord? Owner { get; internal set; }
        public Terrain Terrain { get; internal set; } = Terrain.None;
        public uint Killed { get; internal set; }
        public uint Lost { get; internal set; }

        public Stronghold() : base(EntityType.Stronghold)
        {
        }
    }
}