using System;
using TME.Serialize;
using TME.Types;

namespace TME.Scenario.Default.Base
{
    public partial class Entity
    {
        #region Serialize

        public virtual bool Load(ISerializeContext ctx)
        {
            Id = new MXId(Type, (uint) ctx.Reader.ReadInt32());
            Symbol = ctx.Reader.ReadString();
            RawFlags = ctx.Reader.ReadUInt32();

            return true;
        }

        public virtual bool Save()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}