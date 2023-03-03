using System.Diagnostics.CodeAnalysis;
using TME.Scenario.ddr.Interfaces;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Items;
using TME.Serialize;

namespace TME.Scenario.ddr.Items
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class RevengeStronghold : Stronghold, IRevengeStronghold
    {
        public uint Energy { get; internal set; }

        public Race Loyalty => Occupier?.Race ?? Race.None;

        public override bool Load(ISerializeContext ctx)
        {
            if (!base.Load(ctx)) return false;

            Energy = ctx.Version > 9
                ? ctx.Reader.UInt32()
                : 180;

            return true;
        }
        
        public override bool Save(ISerializeContext ctx)
        {
            if (!base.Save(ctx)) return false;

            ctx.Writer.UInt32(Energy);
          
            return true;
        }
        
        public override bool Load(IBundleReader bundle)
        {
            if (!base.Load(bundle)) return false;

            Energy = bundle.UInt32(nameof(Energy));

            return true;
        }
    }
    
}