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

            LongName = ctx.Reader.ReadString();
            ShortName = ctx.Reader.ReadString();
            Looking = ctx.Reader.ReadDirection();
            Time = ctx.Reader.ReadTime();

            Units.FirstOrDefault()?.Load(ctx);
            Units.LastOrDefault()?.Load(ctx);

            BattleInfo.Load(ctx);

            Reckless = ctx.Reader.ReadUInt32();
            Energy = ctx.Reader.ReadUInt32();
            Strength = ctx.Reader.ReadUInt32();
            Cowardly = ctx.Reader.ReadUInt32();
            Courage = ctx.Reader.ReadUInt32();
            Fear = ctx.Reader.ReadUInt32();

            Recruitment.Load(ctx);

            Race = ctx.Reader.ReadRace();

            var carried = ctx.ReadEntity<IObject>();
            Carrying = carried == null
                ? new List<IObject>().AsReadOnly()
                : new List<IObject> {carried}.AsReadOnly();
            
            KilledBy = ctx.ReadEntity<IObject>();

            Gender = ctx.Reader.ReadGender();

            Loyalty = ctx.Reader.ReadRace();

            Liege = ctx.ReadEntity<ICharacter>();

            Foe = ctx.ReadEntity<ICharacter>();

            WaitStatus = ctx.Reader.ReadWaitStatus();

            Orders = ctx.Reader.ReadOrders();

            Despondency = ctx.Reader.ReadUInt32();

            Traits = ctx.Reader.ReadEnum<LordTraits>();

            Following = ctx.Version > 2
                ? ctx.ReadEntity<ICharacter>()
                : null;

            Followers = ctx.Version > 4
                ? ctx.Reader.ReadUInt32()
                : 0;

            // temp bug fix
            if ((int) Energy < 0)
                Energy = 0;

            return true;
        }

        public override bool Save()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}