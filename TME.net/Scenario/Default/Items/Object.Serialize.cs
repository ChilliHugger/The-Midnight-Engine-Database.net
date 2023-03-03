using System;
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

            return true;
        }
        
        #endregion
    }
}