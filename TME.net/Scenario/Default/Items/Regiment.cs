using System.Diagnostics.CodeAnalysis;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;

namespace TME.Scenario.Default.Items
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public partial class Regiment : Item, IRegiment
    {
        public new RegimentFlags Flags => (RegimentFlags) RawFlags;
        public IItem? Parent { get; internal set; } 
        public ArmyType ArmyType { get; internal set; } = ArmyType.Regiment;
        public UnitType UnitType { get; internal set; } = UnitType.None;
        public Race Race { get; internal set; } = Race.None;
        public Race LoyaltyRace => LoyaltyLord?.Loyalty ?? Race;
        public bool Friendly => false;
        public IEntity? Target { get; internal set; }
        public Orders Orders { get; internal set; } = Orders.None;
        public ICharacter? LoyaltyLord { get; internal set; }
        public uint Total { get; internal set; }
        public uint Success { get; internal set; }
        public uint Killed { get; internal set; }
        public uint Delay { get; internal set; }
        public uint Lost { get; internal set; }
        public uint Turns { get; internal set; }
        public Loc LastLocation { get; internal set; } = Loc.Zero;
        public Loc TargetLocation { get; internal set; } = Loc.Zero;

        public bool IsInTunnel => IsFlags(RegimentFlags.Tunnel);
        
        public Regiment() : base(EntityType.Regiment)
        {
        }
    }
}