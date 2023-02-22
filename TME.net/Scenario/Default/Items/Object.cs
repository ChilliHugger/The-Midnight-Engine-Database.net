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
    public partial class Object : Item, IObjectInternal
    {
        public new ThingFlags Flags => (ThingFlags) RawFlags;
        public ThingType Kills { get; internal set; } = ThingType.None;
        public string Name { get; internal set; } = "";
        public string Description { get; internal set; } = "";
        public uint UseDescription { get; internal set;  }
        public IItem? CarriedBy { get; internal set; }
        
        #region Flags
        public bool CanDrop => IsFlags(ThingFlags.Drop);
        public bool CanFight => IsFlags(ThingFlags.Fight);
        public bool CanPickup => IsFlags(ThingFlags.Pickup);
        public bool CanRemove => IsFlags(ThingFlags.Remove);
        public bool CanSee => IsFlags(ThingFlags.See);
        public bool HelpsRecruitment => IsFlags(ThingFlags.Recruitment);
        public bool IsCarried => CarriedBy != null;
        public bool IsRandomStart => IsFlags(ThingFlags.RandomStart);
        public bool IsSpecial => IsFlags(ThingFlags.Special);
        public bool IsUnique => IsFlags(ThingFlags.Unique);
        public bool IsWeapon => IsFlags(ThingFlags.Weapon);
        #endregion
        
        public Object() : base(EntityType.Thing)
        {
        }
        
        public void UpdateCarriedBy(IItem? carriedBy)
        {
            CarriedBy = carriedBy;
        }
    }
}