using System.Diagnostics.CodeAnalysis;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Serialize;

namespace TME.Scenario.Default.info
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class TerrainInfo : Info
    {
        public string Preposition { get; internal set; } = "";
        public string Description { get; internal set; } = "";
        public uint Success { get; internal set; }
        public uint Visibility { get; internal set; }
        public uint Obstruction { get; internal set; }
        public int MovementCost { get; internal set; }

        public bool IsPlural => IsFlags(TerrainInfoFlags.Plural);
        public bool IsBlock => IsFlags(TerrainInfoFlags.Block);
        public bool IsInteresting => IsFlags(TerrainInfoFlags.Interesting);
        public bool IsArmyVisible => IsFlags(TerrainInfoFlags.Army);

        public TerrainInfo() : base(EntityType.TerrainInfo)
        {
        }
        
        public override bool Load(ISerializeContext ctx)
        {
            if (!base.Load(ctx)) return false;

            Preposition = ctx.Reader.ReadString();
            Description = ctx.Reader.ReadString();
            Success = ctx.Reader.ReadUInt32();
            Visibility = ctx.Reader.ReadUInt32();
            Obstruction = ctx.Reader.ReadUInt32();
            MovementCost = ctx.Reader.ReadInt32();

            return true;
        }
    }
}
