using System.Collections.Generic;
using TME.Default.Interfaces;
using TME.Scenario.Default.Interfaces;
using TME.Types;

namespace TME.Interfaces
{
    public interface IDatabase
    {
        string Directory { get; set; }
        IMap GameMap { get; set; }

        bool Load();
        bool Unload();
        
        IEnumerable<ILord> Lords { get; }
        IEnumerable<IRouteNode> RouteNodes { get; }
        IEnumerable<IRegiment> Regiments { get; } 
        IEnumerable<IStronghold> Strongholds { get; }
    }
}
