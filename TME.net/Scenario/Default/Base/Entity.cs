using System;
using TME.Default.Interfaces;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Serialize;
using TME.Types;

namespace TME.Scenario.Default.Base
{
    public class Entity : IEntity, ISerializable
    {
        public Entity()
        {
            Id = 0;
        }

        public Entity(EntityType type)
        {
            Type = type;
        }

        public ulong RawFlags { get; protected set; }

        public EntityType Type { get; protected set; } = EntityType.None;

        public uint RawId => (uint)Id.RawId;

        public MXId Id { get; protected set; }

        public object? UserData { get; protected set; }

        public string Symbol { get; protected set; } = "";


        public bool IsFlags(EntityFlags mask) => HasFlags((ulong)mask);

        public bool HasFlags(ulong mask) => (RawFlags & mask) == mask;

        public bool IsSymbol(string value) => Symbol.Equals(value, StringComparison.OrdinalIgnoreCase);

        public bool IsDisabled => IsFlags(EntityFlags.Disabled);

#region Serialize
        public virtual bool Load(ISerializeContext ctx)
        {
            Id = new MXId(Type,(uint)ctx.Reader.ReadInt32());
            Symbol = ctx.Reader.ReadString();
            RawFlags = ctx.Reader.ReadUInt32();

            return true;
        }

        public virtual bool Save()
        {
            throw new NotImplementedException();
        }
#endregion

        public override string ToString()
        {
            var idx = Id.RawId.ToString("0000");
            return $"{idx} : '{Symbol}'";
        }
    }
}
