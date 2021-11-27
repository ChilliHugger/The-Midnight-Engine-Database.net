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
                ? ctx.Reader.ReadEnum<ObjectType>()
                : ObjectType.None;
            ObjectPower = (ctx.Version > 10)
                ? ctx.Reader.ReadEnum<ObjectPower>()
                : ObjectPower.None;

            // TODO: This should be actual entry in the database
            SetFlags(ThingFlags.Special, CheckIsSpecial());
            
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