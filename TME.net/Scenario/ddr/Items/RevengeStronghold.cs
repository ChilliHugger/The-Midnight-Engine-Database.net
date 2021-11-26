using System.Diagnostics.CodeAnalysis;
using TME.Scenario.ddr.Interfaces;
using TME.Scenario.Default.Items;
using TME.Serialize;

namespace TME.Scenario.ddr.Items
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class RevengeStronghold : Stronghold, IRevengeStronghold
    {
        public uint Energy { get; internal set; }

        public override bool Load(ISerializeContext ctx)
        {
            if (!base.Load(ctx)) return false;

            Energy = ctx.Reader.ReadUInt32();

            return true;
        }
    }
    
}