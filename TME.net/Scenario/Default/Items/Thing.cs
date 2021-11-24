using System.Diagnostics.CodeAnalysis;
using TME.Extensions;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;

namespace TME.Scenario.Default.Items
{
    [SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public partial class Thing : Item, IThingInternal
    {
        public ThingType Kills { get; internal set; } = ThingType.None;
        public string Name { get; internal set; } = "";
        public string Description { get; internal set; } = "";
        public uint UseDescription { get; internal set;  }
        public IItem? CarriedBy { get; internal set; }
        
        public bool IsUnique => HasFlags(ThingFlags.Unique.Raw());
        public bool IsCarried => CarriedBy != null;

        public Thing() : base(EntityType.Thing)
        {
        }
        
        public void UpdateCarriedBy(IItem? carriedBy)
        {
            CarriedBy = carriedBy;
        }
    }
}