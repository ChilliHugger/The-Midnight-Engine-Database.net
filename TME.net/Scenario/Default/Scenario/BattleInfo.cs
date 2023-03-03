using System;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;

namespace TME.Scenario.Default.Scenario
{
    public class BattleInfo 
    {
        public Loc Location { get; set; }
        public uint Slew { get; set; }

        #region Serialize
        public bool Load(ISerializeContext ctx)
        {
            Location = ctx.Reader.Loc();
            Slew = ctx.Reader.UInt32();
            return true;
        }

        public bool Save(ISerializeContext ctx)
        {
            ctx.Writer.Loc(Location);
            ctx.Writer.UInt32(Slew);
            return true;
        }
        #endregion
    }
}
