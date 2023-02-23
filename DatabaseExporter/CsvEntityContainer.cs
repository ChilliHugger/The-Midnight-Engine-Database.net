using System.Collections.Generic;
using TME.Interfaces;
using TME.Scenario.Default.info;
using TME.Scenario.Default.Interfaces;

namespace DatabaseExporter;

public class CsvEntityContainer : IEntityContainer
{
    public IReadOnlyDictionary<string, IEntity> SymbolCache { get; set; }
    public IReadOnlyList<ICharacter> Lords { get; set; }
    public IReadOnlyList<IRouteNode> RouteNodes { get; set; }
    public IReadOnlyList<IRegiment> Regiments { get; set; }
    public IReadOnlyList<IStronghold> Strongholds { get; set; }
    public IReadOnlyList<IWaypoint> Waypoints { get; set; }
    public IReadOnlyList<IObject> Things { get; set; }
    public IReadOnlyList<IMission> Missions { get; set; }
    public IReadOnlyList<IVictory> Victories { get; set; }
    public IReadOnlyList<DirectionInfo> Directions { get; set; }
    public IReadOnlyList<UnitInfo> Units { get; set; }
    public IReadOnlyList<RaceInfo> Races { get; set; }
    public IReadOnlyList<GenderInfo> Genders { get; set; }
    public IReadOnlyList<TerrainInfo> Terrains { get; set; }
    public IReadOnlyList<AreaInfo> Areas { get; set; }
    public IReadOnlyList<CommandInfo> Commands { get; set; }
    public IReadOnlyList<ObjectTypeInfo> ObjectTypes { get; set; }
    public IReadOnlyList<ObjectPowerInfo> ObjectPowers { get; set; }
}