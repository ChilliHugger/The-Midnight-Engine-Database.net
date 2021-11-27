using System.Collections.Generic;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Interfaces;

namespace TME.Interfaces
{
    public interface IMapQueryService
    {
        IReadOnlyList<ICharacter> LordsAtLocation(Loc location, bool inTunnel);
        IReadOnlyList<ICharacter> NotRecruitedLordsAtLocation(Loc location, bool inTunnel);
        IReadOnlyList<ICharacter> RecruitedLordsAtLocation(Loc location, bool inTunnel);
        IReadOnlyList<IRouteNode> RouteNodesAtLocation(Loc location);
        IReadOnlyList<IRegiment> RegimentsAtLocation(Loc location, bool inTunnel);
        IReadOnlyList<IStronghold> StrongholdsAtLocation(Loc location);
    }
}