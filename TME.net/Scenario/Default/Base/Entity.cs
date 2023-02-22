using System;
using System.Diagnostics.CodeAnalysis;
using TME.Extensions;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;
using TME.Types;

namespace TME.Scenario.Default.Base
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public partial class Entity : IEntityInternal, ISerializable
    {
        #region Properties
        public uint RawFlags { get; internal set; }
        public EntityFlags Flags => (EntityFlags) RawFlags;
        public EntityType Type { get; internal set; }
        public uint RawId => (uint)Id.RawId;
        public MXId Id { get; internal set; }
        public object? UserData { get; internal set; }
        public string Symbol { get; internal set; } = "";
        public bool IsFlags<T>(T mask) where T : Enum => HasFlags(mask.Raw());
        public bool HasFlags(uint mask) => (RawFlags & mask) == mask;
        public bool IsSymbol(string value) => Symbol.Equals(value, StringComparison.OrdinalIgnoreCase);
        public bool IsDisabled => IsFlags(EntityFlags.Disabled);
        #endregion
        
        internal Entity()
        {
            Id = 0;
            RawFlags = 0;
            Type = EntityType.None;
        }

        internal Entity(EntityType type)
        {
            Id = 0;
            RawFlags = 0;
            Type = type;
        }
        
        public override string ToString()
        {
            var idx = Id.RawId.ToString("0000");
            return $"{idx} : '{Symbol}'";
        }
        
        #region Internal Helpers
        public void SetFlags<T>(T flags,bool value)
            where T : Enum
        {
            if (value)
            {
                RawFlags |= flags.Raw();
            }
            else
            {
                RawFlags &= ~flags.Raw();
            }
        }

        public void SetId(MXId id) => Id = id;

        #endregion
    }
}
