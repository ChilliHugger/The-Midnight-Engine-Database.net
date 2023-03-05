using System;
using TME.Scenario.ddr;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;

namespace TME.Scenario.Default.Items
{
    public partial class Object
    {
        #region Serialize

        public override bool Load(ISerializeContext ctx)
        {
            if (!base.Load(ctx)) return false;


            Name = ctx.Reader.String();
            CarriedBy = ctx.ReadEntity() as IItem;
            Description = ctx.Reader.String();
            Kills = ctx.Reader.ThingType();
            UseDescription = ctx.Reader.UInt32();
            
            if (ctx.Scenario?.Info.Symbol == RevengeScenario.Tag || ctx.Version > 19)
            {
                ObjectType = (ctx.Version > 10)
                    ? ctx.Reader.Enum<ObjectType>()
                    : ObjectType.None;
                ObjectPower = (ctx.Version > 10)
                    ? ctx.Reader.Enum<ObjectPower>()
                    : ObjectPower.None;

                // TODO: This should be actual entry in the database
                // SetFlags(ObjectFlags.Special, CheckIsSpecial());
            }

            return true;
        }

        public override bool Save(ISerializeContext ctx)
        {
            if (!base.Save(ctx)) return false;
            
             ctx.Writer.String(Name);
             ctx.Writer.MXId(CarriedBy);
             ctx.Writer.String(Description);
             ctx.Writer.Enum(Kills);
             ctx.Writer.UInt32(UseDescription);

             // Revenge / Version 20
             ctx.Writer.Enum(ObjectType);
             ctx.Writer.Enum(ObjectPower);
   
             return true;
        }
        
        public override bool Load(IBundleReader bundle)
        {
            if (!base.Load(bundle)) return false;
            
            Name = bundle.String(nameof(Name));
            CarriedBy = bundle.Entity<IItem>(nameof(CarriedBy));
            Description = bundle.String(nameof(Description));
            Kills = bundle.ThingType(nameof(Kills));
            UseDescription = bundle.UInt32(nameof(UseDescription));
            
            ObjectType = bundle.Enum<ObjectType>(nameof(ObjectType));
            ObjectPower = bundle.Enum<ObjectPower>(nameof(ObjectPower));

            return true;
        }
        
        // private bool CheckIsSpecial()
        // {
        //     return IsSymbol("OB_CROWN_VARENAND") ||
        //            IsSymbol("OB_CROWN_CARUDRIUM") ||
        //            IsSymbol("OB_SPELL_THIGRORN") ||
        //            IsSymbol("OB_RUNES_FINORN") ||
        //            IsSymbol("OB_CROWN_IMIRIEL");
        // }
        
        #endregion
    }
}