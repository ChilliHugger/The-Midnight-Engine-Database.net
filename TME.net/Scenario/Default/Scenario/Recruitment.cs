using System;
using System.Diagnostics.CodeAnalysis;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;

namespace TME.Scenario.Default.Scenario
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class Recruitment 
    {
        public uint Key { get; internal set; }
        public uint By { get; internal set; }

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
