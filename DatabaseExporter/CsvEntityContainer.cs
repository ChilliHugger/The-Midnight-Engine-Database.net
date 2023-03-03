using System.Collections.Generic;
using System.Linq;
using TME.Interfaces;
using TME.Scenario.ddr;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.info;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;

namespace DatabaseExporter;

public class CsvEntityContainer : IEntityContainer, ISerializableSave
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

    public bool Save(ISerializeContext context)
    {
        return context switch
        {
            {Section: DataSection.Counts} => SaveCounts(context),
            {Section: DataSection.Entities} => SaveMainInfo(context),
            {Section: DataSection.ObjectInfo} => SaveObjectInfo(context),
            _ => true
        };
    }

    private bool SaveCounts(ISerializeContext context)
    {
        context.Writer.Int32(Lords.Count);
        context.Writer.Int32(Regiments.Count);
        context.Writer.Int32(RouteNodes.Count);
        context.Writer.Int32(Strongholds.Count);
        context.Writer.Int32(Waypoints.Count);
        context.Writer.Int32(Things.Count);
        context.Writer.Int32(Missions.Count);
        context.Writer.Int32(Victories.Count);
        
        context.Writer.Int32(Directions.Count);
        context.Writer.Int32(Units.Count);
        context.Writer.Int32(Races.Count);
        context.Writer.Int32(Genders.Count);
        context.Writer.Int32(Terrains.Count);
        context.Writer.Int32(Areas.Count);
        context.Writer.Int32(Commands.Count);

        return true;
    }

    private bool SaveMainInfo(ISerializeContext context)
    {
        Write(Lords, context);
        Write(Regiments, context);
        Write(RouteNodes, context);
        Write(Strongholds, context);
        Write(Waypoints, context);
        Write(Things, context);
        Write(Missions, context);
        Write(Victories, context);

        Write(Directions, context);
        Write(Units, context);
        Write(Races, context);
        Write(Genders, context);
        Write(Terrains, context);
        Write(Areas, context);
        Write(Commands, context);

        return true;
    }

    private bool SaveObjectInfo(ISerializeContext context)
    {
        // chunk 4
        if (context.Scenario is RevengeScenario && context.Version > 10)
        {
            context.Writer.Int32(ObjectTypes.Count);
            Write(ObjectTypes, context);

            context.Writer.Int32(ObjectPowers.Count);
            Write(ObjectPowers, context);
        }

        return true;
    }
    
    private static void Write<T>(IEnumerable<T> list, ISerializeContext context)
        where T: IEntity
    {
        foreach (var item in list.OfType<ISerializableSave>())
        {
            item.Save(context);
        }
    }
}
