using System.Collections.Generic;
using TME.Default.Interfaces;
using TME.Scenario.Default.Interfaces;

namespace TME.Interfaces
{
    public interface IEntityContainer
    {
        IEnumerable<ILord> Lords { get; }
        IEnumerable<IRouteNode> RouteNodes { get; }
        IEnumerable<IRegiment> Regiments { get; } 
        IEnumerable<IStronghold> Strongholds { get; }
    }
}