using TME.Scenario.Default.Base;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.LocationInfoBuilders;

namespace TME.Interfaces
{
    public interface ILocationLordInfoBuilder
    {
        ILocationLordInfoBuilder Location(Loc location);
        ILocationLordInfoBuilder Lord(ICharacter lord);
        ILocationLordInfoBuilder Tunnel(bool tunnel);
        LocationLordInfo Build();
    }
}