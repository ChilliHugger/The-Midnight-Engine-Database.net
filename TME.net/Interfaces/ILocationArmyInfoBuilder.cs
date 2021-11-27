using TME.Scenario.Default.Base;
using TME.Scenario.Default.LocationInfoBuilders;

namespace TME.Interfaces
{
    public interface ILocationArmyInfoBuilder
    {
        ILocationArmyInfoBuilder Location(Loc location);
        ILocationArmyInfoBuilder Tunnel(bool tunnel);
        LocationArmyInfo Build();
    }
}