using System.Diagnostics.CodeAnalysis;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Serialize;

namespace TME.Scenario.Default.info
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class CommandInfo : Info
    {
        public uint SuccessTime { get; internal set; }
        public uint FailureTime { get; internal set; }

        public CommandInfo() : base(EntityType.CommandInfo)
        {
        }
        
        public override bool Load(ISerializeContext ctx)
        {
            if (!base.Load(ctx)) return false;
            
            SuccessTime = ctx.Reader.ReadUInt32();
            FailureTime = ctx.Reader.ReadUInt32();
            
            return true;
        }
    }
}
