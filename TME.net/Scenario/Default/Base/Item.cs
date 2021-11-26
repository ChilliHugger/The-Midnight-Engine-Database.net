using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;

namespace TME.Scenario.Default.Base
{
    public class Item : Entity, IMappableInternal
    {
        #region Properties
        public Loc Location { get; private set; } = Loc.Zero;
        #endregion

        internal Item(EntityType type) : base(type)
        {
        }

        #region Serialize
        public override bool Load(ISerializeContext ctx)
        {
            if (!base.Load(ctx))
            {
                return false;
            }
            
            Location = ctx.Reader.ReadLoc();

            return true;
        }
        #endregion

        #region Internal Helpers
        void IMappableInternal.UpdateLocation(Loc location) => Location = location;
        #endregion
    }
}
