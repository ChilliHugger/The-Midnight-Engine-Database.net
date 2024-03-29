﻿using System;
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
    public partial class Entity : IEntity, ISerializable, IBundle
    {
        public uint RawFlags { get; internal set; }
        public EntityType Type { get; internal set; }
        public string Symbol { get; internal set; }
        public MXId Id { get; internal set; }
        public object? UserData { get; internal set; }
        
        #region Helpers
        public uint RawId => Id.RawId;
        public bool IsFlags<T>(T mask) where T : Enum => HasFlags(mask.Raw());
        public bool HasFlags(uint mask) => (RawFlags & mask) == mask;
        public bool IsSymbol(string value) => Symbol.Equals(value, StringComparison.OrdinalIgnoreCase);
        public bool IsDisabled => IsFlags(EntityFlags.Disabled);
        public EntityFlags Flags
        {
            get => (EntityFlags) RawFlags;
            internal set => RawFlags = value.Raw();
        }

        #endregion
        
        internal Entity()
        {
            Id = MXId.None;
            Flags = EntityFlags.None;
            Type = EntityType.None;
            Symbol = "";
        }

        internal Entity(EntityType type)
        {
            Id = MXId.None;
            Flags = EntityFlags.None;
            Type = type;
            Symbol = "";
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
