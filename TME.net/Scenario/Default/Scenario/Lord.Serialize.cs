using System;
using System.Collections.Generic;
using System.Linq;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;

namespace TME.Scenario.Default.Scenario
{
    public partial class Lord
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

            var carried = ctx.ReadEntity<IThing>();
            Carrying = carried == null
                ? new List<IThing>().AsReadOnly()
                : new List<IThing> {carried}.AsReadOnly();
            
            KilledBy = ctx.ReadEntity<IThing>();

            Gender = ctx.Reader.ReadGender();

            Loyalty = ctx.Reader.ReadRace();

            Liege = ctx.ReadEntity<ILord>();

            Foe = ctx.ReadEntity<ILord>();

            WaitStatus = ctx.Reader.ReadWaitStatus();

            Orders = ctx.Reader.ReadOrders();

            Despondency = ctx.Reader.ReadUInt32();

            Traits = ctx.Reader.ReadUInt32();

            Following = ctx.Version > 2
                ? ctx.ReadEntity<ILord>()
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