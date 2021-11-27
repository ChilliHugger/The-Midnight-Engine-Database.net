using TME.Scenario.Default.Base;
using TME.Scenario.Default.LocationInfoBuilders;

namespace TME.Interfaces
{
    public interface ILocationArmyCountInfoBuilder
    {
        ILocationArmyCountInfoBuilder Location(Loc location);
        ILocationArmyCountInfoBuilder Tunnel(bool tunnel);
        LocationArmyCountInfo Build();
    }
}