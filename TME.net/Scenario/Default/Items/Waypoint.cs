using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;

namespace TME.Scenario.Default.Items
{
    public class Waypoint : Item, IWaypointInternal
    {
        public Waypoint() : base(EntityType.Waypoint)
        {
        }
    }
}