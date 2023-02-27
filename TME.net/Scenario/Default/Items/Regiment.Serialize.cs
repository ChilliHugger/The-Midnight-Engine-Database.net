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

        public override bool Load(IBundleReader bundle)
        {
            if (!base.Load(bundle)) return false;

            Race = bundle.Race(nameof(Race));
            UnitType = bundle.UnitType(nameof(UnitType));
            Total = bundle.UInt32(nameof(Total));
            Target = bundle.Entity<IEntity>(nameof(Target));
            Orders = bundle.Orders(nameof(Orders));
            Success = bundle.UInt32(nameof(Success));
            LoyaltyLord = bundle.Entity<ICharacter>(nameof(LoyaltyLord));
            Killed = 0;
            LastLocation = Location;
            Delay = bundle.UInt32(nameof(Delay));
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