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

            Race = ctx.Reader.ReadRace();
            UnitType = ctx.Reader.ReadUnitType();
            Total = ctx.Reader.ReadUInt32();
            Target = ctx.ReadEntity();
            Orders = ctx.Reader.ReadOrders();
            Success = ctx.Reader.ReadUInt32();
            LoyaltyLord = ctx.ReadEntity<ICharacter>();
            Killed = ctx.Reader.ReadUInt32();
            LastLocation = (ctx.Version > 3)
                ? ctx.Reader.ReadLoc()
                : Loc.Zero;
            Delay = (ctx.Version > 6)
                ? ctx.Reader.ReadUInt32()
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