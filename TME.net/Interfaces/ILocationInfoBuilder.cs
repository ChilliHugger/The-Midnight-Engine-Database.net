using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.LocationInfoBuilders;

namespace TME.Interfaces
{
    public interface ILocationInfoBuilder
    {
        ILocationInfoBuilder Location(Loc location);
        ILocationInfoBuilder Direction(Direction direction);
        ILocationInfoBuilder Tunnel(bool tunnel);
        LocationInfo Build();
    }
}