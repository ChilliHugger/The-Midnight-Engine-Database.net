using System.Collections.Generic;
using TME.Scenario.Default.Interfaces;

namespace TME.Interfaces
{
    public interface IEntityContainer
    {
        IReadOnlyList<ILord> Lords { get; }
        IReadOnlyList<IRouteNode> RouteNodes { get; }
        IReadOnlyList<IRegiment> Regiments { get; } 
        IReadOnlyList<IStronghold> Strongholds { get; }
        IReadOnlyList<IWaypoint> Waypoints { get; }
        IReadOnlyList<IThing> Things { get; }
    }
}