using System.Collections.Generic;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Interfaces;

namespace TME.Interfaces
{
    public interface IMapQueryService
    {
        IReadOnlyList<ILord> LordsAtLocation(Loc location, bool inTunnel);
        IReadOnlyList<ILord> NotRecruitedLordsAtLocation(Loc location, bool inTunnel);
        IReadOnlyList<ILord> RecruitedLordsAtLocation(Loc location, bool inTunnel);
        IReadOnlyList<IRouteNode> RouteNodesAtLocation(Loc location);
        IReadOnlyList<IRegiment> RegimentsAtLocation(Loc location, bool inTunnel);
        IReadOnlyList<IStronghold> StrongholdsAtLocation(Loc location);
    }
}