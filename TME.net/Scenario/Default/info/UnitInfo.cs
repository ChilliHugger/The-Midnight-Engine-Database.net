using System.Diagnostics.CodeAnalysis;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Serialize;

namespace TME.Scenario.Default.info
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class UnitInfo : Info
    {
        public uint Success { get; internal set; }
        public uint BaseRestModifier { get; internal set; }

        public UnitInfo() : base(EntityType.UnitInfo)
        {
        }
        
        public override bool Load(ISerializeContext ctx)
        {
            if (!base.Load(ctx)) return false;

            Success = ctx.Reader.ReadUInt32();
            BaseRestModifier = ctx.Reader.ReadUInt32();
            
            return true;
        }
    }
}
