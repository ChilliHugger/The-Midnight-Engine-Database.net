using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;

namespace TME.Scenario.Default.Base
{
    public class Item : Entity, IMappable, IMappableInternal
    {
        private Loc _loc;
        
        #region Properties
        public Loc Location => _loc;

        Loc IMappableInternal.Location {
            set => _loc = value;
        }

        #endregion

        internal Item(EntityType type) : base(type)
        {
        }

        #region Serialize
        public override bool Load(ISerializeContext ctx)
        {
            if (!base.Load(ctx)) return false;
            
            _loc = ctx.Reader.Loc();

            return true;
        }

        public override bool Save(ISerializeContext ctx)
        {
            if (!base.Save(ctx)) return false;
            
            ctx.Writer.Loc(Location);

            return true;
        }

        public override bool Load(IBundleReader bundle)
        {
            if (!base.Load(bundle)) return false;
            
            _loc = bundle.Loc(nameof(Location));

            return true;
        }
        
        #endregion

        #region Internal Helpers
        //void IMappableInternal.Location => Location = value;
        #endregion
    }
}
