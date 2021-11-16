using TME.Serialize;

namespace TME.Scenario.Default.Base
{
    public class Item : Entity
    {
        public Loc Location { get;  set; }

        public override bool Load(ISerializeContext ctx)
        {
            if (!base.Load(ctx))
            {
                return false;
            }
            
            Location = ctx.Reader.ReadLoc();

            return true;
        }
    }
}
