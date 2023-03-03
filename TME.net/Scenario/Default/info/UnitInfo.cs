using System;
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

            Success = ctx.Reader.UInt32();
            BaseRestModifier = (uint) Math.Max(ctx.Reader.Int32(),0); // temp bug fix
            return true;
        }
        
        public override bool Save(ISerializeContext ctx)
        {
            if (!base.Save(ctx)) return false;

            ctx.Writer.UInt32(Success);
            ctx.Writer.UInt32(BaseRestModifier);
 
            return true;
        }
        
        public override bool Load(IBundleReader bundle)
        {
            if(!base.Load(bundle)) return false;

            Success = bundle.UInt32(nameof(Success));
            BaseRestModifier = bundle.UInt32(nameof(BaseRestModifier));

            return true;
        }
    }
}
