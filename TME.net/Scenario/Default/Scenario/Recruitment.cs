using System;
using System.Diagnostics.CodeAnalysis;
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
            Key = ctx.Reader.UInt32();
            By = ctx.Reader.UInt32();
            return true;
        }

        public bool Load(IBundleReader bundle)
        {
            Key = bundle.UInt32(nameof(Key));
            By = bundle.UInt32(nameof(By));
            return true;
        }
        
        public bool Save(ISerializeContext ctx)
        {
            ctx.Writer.UInt32(Key);
            ctx.Writer.UInt32(By);
            return true;
        }
        #endregion
    }
}
