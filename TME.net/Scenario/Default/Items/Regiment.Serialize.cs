using System;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;

namespace TME.Scenario.Default.Items
{
    public partial class Regiment
    {
        #region Serialize

        public override bool Load(ISerializeContext ctx)
        {
            if (!base.Load(ctx)) return false;

            Race = ctx.Reader.Race();
            UnitType = ctx.Reader.UnitType();
            Total = ctx.Reader.UInt32();
            Target = ctx.ReadEntity();
            Orders = ctx.Reader.Orders();
            Success = ctx.Reader.UInt32();
            LoyaltyLord = ctx.ReadEntity<ICharacter>();
            Killed = ctx.Reader.UInt32();
            LastLocation = (ctx.Version > 3)
                ? ctx.Reader.Loc()
                : Loc.Zero;
            Delay = (ctx.Version > 6)
                ? ctx.Reader.UInt32()
                : 0;

            Lost = 0;
            TargetLocation = Loc.Zero;

            return true;
        }

        public override bool Save()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}