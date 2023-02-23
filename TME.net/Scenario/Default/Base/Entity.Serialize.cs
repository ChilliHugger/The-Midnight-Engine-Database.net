using System;
using TME.Scenario.Default.Flags;
using TME.Serialize;
using TME.Types;

namespace TME.Scenario.Default.Base
{
    public partial class Entity
    {
        #region Serialize

        public virtual bool Load(ISerializeContext ctx)
        {
            Id = ctx.Reader.ReadMXId(Type);
            Symbol = ctx.Reader.ReadString();
            RawFlags = ctx.Reader.ReadUInt32();
            return true;
        }

        public virtual bool Save()
        {
            throw new NotImplementedException();
        }

        public virtual bool Load(Bundle bundle)
        {
            Id = bundle.Id(nameof(Id));
            Symbol = bundle.String(nameof(Symbol));
            Flags = bundle.Flags<EntityFlags>(nameof(Flags));
            return true;
        }
        
        #endregion
    }
}