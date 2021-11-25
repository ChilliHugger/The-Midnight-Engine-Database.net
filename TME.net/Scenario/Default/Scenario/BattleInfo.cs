using System;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;

namespace TME.Scenario.Default.Scenario
{
    public class BattleInfo : IBattleInfo
    {
        public Loc Location { get; set; }
        public uint Slew { get; set; }

        #region Serialize
        public bool Load(ISerializeContext ctx)
        {
            Location = ctx.Reader.ReadLoc();
            Slew = ctx.Reader.ReadUInt32();
            return true;
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
