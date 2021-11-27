using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;

namespace TME.Scenario.Default.LocationInfoBuilders
{
    [SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class LocationInfo
    {
        public Loc Location { get; internal set; } = Loc.Zero;
        public bool Tunnel { get; internal set; }
        public Direction Looking { get; internal set; } = Direction.None;
        public Loc LocationLookingAt { get; internal set; } = Loc.Zero;
        public Loc LocationInFront { get; internal set; } = Loc.Zero;
        public MapLoc MapLoc { get; internal set; } = MapLoc.None;
        public ThingType FightThing { get; internal set; } = ThingType.None;
        public int FearAdjuster { get; internal set; }
        public int MoralAdjuster { get; internal set; }
        public int StrongholdAdjuster { get; internal set; }

        public IReadOnlyList<IRouteNode> Routenodes { get; internal set; } = new List<IRouteNode>();

        public ICharacter? StubbornFollowerMove { get; internal set; }
        public ICharacter? StubbornFollowerBattle { get; internal set; }
        public IObject? ObjectToTake { get; internal set; }
        public ICharacter? SomeoneToGiveTo { get; internal set; }
        
        public ICharacter? Owner { get; internal set; }
        public uint FriendArmies { get; internal set; }
        public uint FoeArmies { get; internal set; }
        
        // public LocationInfoFlags LocationFlags { get; internal set; } = LocationInfoFlags.None;
        // Owner
        // Friend Army Count
        // Foe Army Count
        
        // LocationLordInfo
        // LocationArmyInfo
    }
}