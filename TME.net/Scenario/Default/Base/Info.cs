using System.Diagnostics.CodeAnalysis;
using TME.Scenario.Default.Enums;
using TME.Serialize;

namespace TME.Scenario.Default.Base
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "MemberCanBeProtected.Global")]
    public class Info : Entity
    {
        public string Name { get; internal set; } = "";

        protected Info(EntityType type) : base(type)
        {
        }

        public override bool Load(ISerializeContext ctx)
        {
            if (!base.Load(ctx)) return false;

            Name = ctx.Reader.ReadString();
            
            return true;
        }
    }
}
