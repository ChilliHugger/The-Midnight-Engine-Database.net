using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;

namespace TME.Scenario.Default.Base
{
    public class Item : Entity, IMappableInternal
    {
        public Loc Location { get; private set; } = Loc.Zero;

        internal Item(EntityType type) : base(type)
        {
        }
        
        public override bool Load(ISerializeContext ctx)
        {
            if (!base.Load(ctx))
            {
                return false;
            }
            
            Location = ctx.Reader.ReadLoc();

            return true;
        }

        void IMappableInternal.UpdateLocation(Loc location)
        {
            Location = location;
        }
    }
}
