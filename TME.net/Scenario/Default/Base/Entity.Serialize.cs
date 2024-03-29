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
            Id = ctx.Reader.MXId(Type);
            Symbol = ctx.Reader.String();
            RawFlags = ctx.Reader.UInt32();
            return true;
        }

        public virtual bool Save(ISerializeContext ctx)
        {
            ctx.Writer.UInt32(Id.RawId);
            ctx.Writer.String(Symbol);
            ctx.Writer.UInt32(RawFlags);
            return true;
        }

        public virtual bool Load(IBundleReader bundle)
        {
            Id = bundle.Id(nameof(Id));
            Symbol = bundle.String(nameof(Symbol));
            Flags = bundle.Flags<EntityFlags>(nameof(Flags));
            return true;
        }
        
        #endregion
    }
}