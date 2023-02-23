using System.Diagnostics.CodeAnalysis;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Serialize;
using TME.Types;

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
        
        public override bool Load(Bundle bundle)
        {
            if(!base.Load(bundle)) return false;

            Name = bundle.String(nameof(Name));
       
            return true;
        }
    }
}
