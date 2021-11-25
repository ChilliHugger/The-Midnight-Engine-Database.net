using System.Collections.Generic;
using TME.Scenario.Default.info;
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
        IReadOnlyList<IMission> Missions { get; }
        IReadOnlyList<IVictory> Victories { get; }
        
        IReadOnlyList<DirectionInfo> Directions { get; }
        IReadOnlyList<UnitInfo> Units { get; }
        IReadOnlyList<RaceInfo> Races { get; }
        IReadOnlyList<GenderInfo> Genders { get; }
        IReadOnlyList<TerrainInfo> Terrains { get; }
        IReadOnlyList<AreaInfo> Areas { get; }
        IReadOnlyList<CommandInfo> Commands { get; }
        IReadOnlyList<ObjectTypeInfo> ObjectTypes { get; }
        IReadOnlyList<ObjectPowerInfo> ObjectPowers { get; }


    }
}