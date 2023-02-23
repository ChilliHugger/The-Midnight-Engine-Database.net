using System.Diagnostics.CodeAnalysis;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Serialize;

namespace TME.Scenario.Default.info
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class GenderInfo : Info
    {
        public string PersonalPronoun { get; internal set; } = "";        // He / She
        public string PossessivePronoun { get; internal set; } = "";      // His / Her
        public string SingularPronoun { get; internal set; } = "";        // Him / Her

        public GenderInfo() : base(EntityType.GenderInfo)
        {
        }
        
        public override bool Load(ISerializeContext ctx)
        {
            if (!base.Load(ctx)) return false;
            
            PersonalPronoun = ctx.Reader.ReadString();
            PossessivePronoun = ctx.Reader.ReadString();
            SingularPronoun = ctx.Reader.ReadString();
            
            return true;
        }
        
        public override bool Load(Bundle bundle)
        {
            if(!base.Load(bundle)) return false;

            PersonalPronoun = bundle.String(nameof(PersonalPronoun));
            PossessivePronoun = bundle.String(nameof(PossessivePronoun));
            SingularPronoun = bundle.String(nameof(SingularPronoun));
       
            return true;
        }
    }
}
