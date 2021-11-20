using System;
using System.Diagnostics.CodeAnalysis;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;

namespace TME.Scenario.Default.Scenario
{
    [SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
    public partial class Thing : Item, IThingInternal
    {
        public ThingType Kills { get; internal set; } = ThingType.None;
        public string Name { get; internal set; } = "";
        public string Description { get; internal set; } = "";
        public uint UseDescription { get; internal set;  }
        public IItem? CarriedBy { get; internal set; }
        
        public bool IsUnique => HasFlags((ulong)ThingFlags.Unique);

        public Thing() : base(EntityType.Thing)
        {
        }
        
        public void UpdateCarriedBy(IItem? carriedBy)
        {
            CarriedBy = carriedBy;
        }
    }
}