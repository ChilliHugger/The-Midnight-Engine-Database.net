using System;
using System.Collections.Generic;
using System.Linq;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;

namespace TME.Scenario.Default.Items
{
    public partial class Character
    {
        #region Serialize

        public override bool Load(ISerializeContext ctx)
        {
            if (!base.Load(ctx)) return false;

            LongName = ctx.Reader.String();
            ShortName = ctx.Reader.String();
            Looking = ctx.Reader.Direction();
            Time = ctx.Reader.Time();

            Units.FirstOrDefault()?.Load(ctx);
            Units.LastOrDefault()?.Load(ctx);

            BattleInfo.Load(ctx);

            Reckless = ctx.Reader.UInt32();
            Energy = (uint) Math.Max(ctx.Reader.Int32(),0); // temp fix
            Strength = ctx.Reader.UInt32();
            Cowardly = ctx.Reader.UInt32();
            Courage = ctx.Reader.UInt32();
            Fear = ctx.Reader.UInt32();

            Recruitment.Load(ctx);

            Race = ctx.Reader.Race();

            var carried = ctx.ReadEntity<IObject>();
            Carrying = carried == null
                ? new List<IObject>().AsReadOnly()
                : new List<IObject> {carried}.AsReadOnly();
            
            KilledBy = ctx.ReadEntity<IObject>();

            Gender = ctx.Reader.Gender();

            Loyalty = ctx.Reader.Race();

            Liege = ctx.ReadEntity<ICharacter>();

            Foe = ctx.ReadEntity<ICharacter>();

            WaitStatus = ctx.Reader.WaitStatus();

            Orders = ctx.Reader.Orders();

            Despondency = ctx.Reader.UInt32();

            Traits = ctx.Reader.Enum<LordTraits>();

            Following = ctx.Version > 2
                ? ctx.ReadEntity<ICharacter>()
                : null;

            Followers = ctx.Version > 4
                ? ctx.Reader.UInt32()
                : 0;
            
            return true;
        }

        public override bool Save()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}