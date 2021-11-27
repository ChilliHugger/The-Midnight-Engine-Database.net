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


            Name = ctx.Reader.ReadString();
            CarriedBy = ctx.ReadEntity<IItem>();
            Description = ctx.Reader.ReadString();
            Kills = ctx.Reader.ReadThingType();
            UseDescription = ctx.Reader.ReadUInt32();

            return true;
        }

        public override bool Save()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}