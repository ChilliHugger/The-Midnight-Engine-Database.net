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
    public partial class Object : Item, IObject
    {
        public new ObjectFlags Flags => (ObjectFlags) RawFlags;
        public ThingType Kills { get; internal set; } = ThingType.None;
        public string Name { get; internal set; } = "";
        public string Description { get; internal set; } = "";
        public uint UseDescription { get; internal set;  }
        public IItem? CarriedBy { get; internal set; }
        
        #region Flags
        public bool CanDrop => IsFlags(ObjectFlags.Drop);
        public bool CanFight => IsFlags(ObjectFlags.Fight);
        public bool CanPickup => IsFlags(ObjectFlags.Pickup);
        public bool CanRemove => IsFlags(ObjectFlags.Remove);
        public bool CanSee => IsFlags(ObjectFlags.See);
        public bool HelpsRecruitment => IsFlags(ObjectFlags.Recruitment);
        public bool IsCarried => CarriedBy != null;
        public bool IsRandomStart => IsFlags(ObjectFlags.RandomStart);
        public bool IsSpecial => IsFlags(ObjectFlags.Special);
        public bool IsUnique => IsFlags(ObjectFlags.Unique);
        public bool IsWeapon => IsFlags(ObjectFlags.Weapon);
        #endregion
        
        public Object() : base(EntityType.Object)
        {
        }
    }
}