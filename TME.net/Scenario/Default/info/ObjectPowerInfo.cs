using System.Diagnostics.CodeAnalysis;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Serialize;

namespace TME.Scenario.Default.info
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class ObjectPowerInfo : Info
    {
        public ObjectPowerInfo() : base(EntityType.ObjectPower)
        {
        }
        
        public override bool Load(ISerializeContext ctx)
        {
            if (!base.Load(ctx)) return false;
            return true;
        }
    }
}
