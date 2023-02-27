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
        public new TerrainInfoFlags Flags => (TerrainInfoFlags) RawFlags;
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

            Preposition = ctx.Reader.String();
            Description = ctx.Reader.String();
            Success = ctx.Reader.UInt32();
            Visibility = ctx.Reader.UInt32();
            Obstruction = ctx.Reader.UInt32();
            MovementCost = ctx.Reader.Int32();

            return true;
        }
        
        public override bool Load(IBundleReader bundle)
        {
            if(!base.Load(bundle)) return false;

            Preposition = bundle.String(nameof(Preposition));
            Description = bundle.String(nameof(Description));
            Success = bundle.UInt32(nameof(Success));
            Visibility = bundle.UInt32(nameof(Visibility));
            Obstruction = bundle.UInt32(nameof(Obstruction));
            MovementCost = bundle.Int32(nameof(MovementCost));
       
            return true;
        }
    }
}
