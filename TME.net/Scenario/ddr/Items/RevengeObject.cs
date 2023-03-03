using System.Diagnostics.CodeAnalysis;
using TME.Scenario.ddr.Interfaces;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Items;
using TME.Serialize;

namespace TME.Scenario.ddr.Items
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class RevengeObject : Object, IRevengeThing
    {
        public ObjectPower ObjectPower { get; internal set; }
        public ObjectType ObjectType { get; internal set; }
        
        public override bool Load(ISerializeContext ctx)
        {
            if (!base.Load(ctx)) return false;

            ObjectType = (ctx.Version > 10)
                ? ctx.Reader.Enum<ObjectType>()
                : ObjectType.None;
            ObjectPower = (ctx.Version > 10)
                ? ctx.Reader.Enum<ObjectPower>()
                : ObjectPower.None;

            // TODO: This should be actual entry in the database
            SetFlags(ObjectFlags.Special, CheckIsSpecial());
            
            return true;
        }

        public override bool Save(ISerializeContext ctx)
        {
            if (!base.Save(ctx)) return false;

            ctx.Writer.Enum(ObjectType);
            ctx.Writer.Enum(ObjectPower);
            
            return true;
        }
        
        public override bool Load(IBundleReader bundle)
        {
            if (!base.Load(bundle)) return false;

            ObjectType = bundle.Enum<ObjectType>(nameof(ObjectType));
            ObjectPower = bundle.Enum<ObjectPower>(nameof(ObjectPower));
            
            return true;
        }
        
        private bool CheckIsSpecial()
        {
            return IsSymbol("OB_CROWN_VARENAND") ||
                   IsSymbol("OB_CROWN_CARUDRIUM") ||
                   IsSymbol("OB_SPELL_THIGRORN") ||
                   IsSymbol("OB_RUNES_FINORN") ||
                   IsSymbol("OB_CROWN_IMIRIEL");
        }
        
    }
}