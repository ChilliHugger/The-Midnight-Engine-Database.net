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

        public override bool Save()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}