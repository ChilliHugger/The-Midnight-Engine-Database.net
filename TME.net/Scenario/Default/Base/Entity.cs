using System;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;
using TME.Types;

namespace TME.Scenario.Default.Base
{
    public partial class Entity : IEntityInternal, ISerializable
    {
        #region Properties
        public ulong RawFlags { get; internal set; }

        public EntityType Type { get; internal set; }
        public uint RawId => (uint)Id.RawId;
        public MXId Id { get; internal set; }
        public object? UserData { get; internal set; }
        public string Symbol { get; internal set; } = "";
        public bool IsFlags(EntityFlags mask) => HasFlags((ulong)mask);
        public bool HasFlags(ulong mask) => (RawFlags & mask) == mask;
        public bool IsSymbol(string value) => Symbol.Equals(value, StringComparison.OrdinalIgnoreCase);
        public bool IsDisabled => IsFlags(EntityFlags.Disabled);
        #endregion
        
        internal Entity()
        {
            Id = 0;
            Type = EntityType.None;
        }

        internal Entity(EntityType type)
        {
            Id = 0;
            Type = type;
        }
        
        public override string ToString()
        {
            var idx = Id.RawId.ToString("0000");
            return $"{idx} : '{Symbol}'";
        }
        
        public void SetFlags(ulong flags,bool value)
        {
            if (value)
            {
                RawFlags |= flags;
            }
            else
            {
                RawFlags &= ~flags;
            }
        }
    }
}
