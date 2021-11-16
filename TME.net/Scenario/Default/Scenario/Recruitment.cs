using System;
using TME.Default.Interfaces;
using TME.Serialize;

namespace TME.Scenario.Default.Scenario
{
    public class Recruitment : IRecruitment
    {
        public uint Key { get; protected set; }
        public uint By { get; protected set; }

        #region Serialize
        public bool Load(ISerializeContext ctx)
        {
            Key = ctx.Reader.ReadUInt32();
            By = ctx.Reader.ReadUInt32();
            return true;
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
