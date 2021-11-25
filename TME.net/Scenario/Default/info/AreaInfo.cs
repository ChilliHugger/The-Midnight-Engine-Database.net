using System.Diagnostics.CodeAnalysis;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Serialize;

namespace TME.Scenario.Default.info
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class AreaInfo : Info
    {
        public string Prefix { get; internal set; } = "";

        public AreaInfo() : base(EntityType.AreaInfo) 
        {
        }

        public override bool Load(ISerializeContext ctx)
        {
            if (!base.Load(ctx)) return false;

            Prefix = ctx.Reader.ReadString();
            
            return true;
        }
    }
}
