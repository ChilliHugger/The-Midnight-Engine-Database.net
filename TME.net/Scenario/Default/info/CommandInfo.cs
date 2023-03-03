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
            
            SuccessTime = ctx.Reader.UInt32();
            FailureTime = ctx.Reader.UInt32();
            
            return true;
        }
        
        public override bool Save(ISerializeContext ctx)
        {
            if (!base.Save(ctx)) return false;
            
            ctx.Writer.UInt32(SuccessTime);
            ctx.Writer.UInt32(FailureTime);

            return true;
        }
        
        public override bool Load(IBundleReader bundle)
        {
            if(!base.Load(bundle)) return false;

            SuccessTime = bundle.UInt32(nameof(SuccessTime));
            FailureTime = bundle.UInt32(nameof(FailureTime));
       
            return true;
        }
    }
}
