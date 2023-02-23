using System.Collections.Generic;
using CsvHelper.TypeConversion;
using TME.Interfaces;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;
using TME.Types;

namespace DatabaseExporter.Converters;

public class CsvConverterFactory
{
    private readonly IStrings _strings;
    private readonly IEntityResolver _entityResolver;

    public CsvConverterFactory(
        IStrings strings,
        IEntityResolver entityResolver)
    {
        _strings = strings;
        _entityResolver = entityResolver;
    }
    
    public void InjectConverters(TypeConverterCache cache)
    {
            var enumConverter = new EnumConverter(_entityResolver);
           
            cache.AddConverter<Race>(enumConverter);
            cache.AddConverter<UnitType>(enumConverter);
            cache.AddConverter<Gender>(enumConverter);
            cache.AddConverter<Direction>(enumConverter);
            cache.AddConverter<Terrain>(enumConverter);
            cache.AddConverter<Command>(enumConverter);
            
            cache.AddConverter<MXId>(new MXIdConverter());
            cache.AddConverter<Loc>(new LocationConverter());
            cache.AddConverter<IRouteNodes>(new RouteNodesConverter());
            cache.AddConverter<uint>(new StringIdConverter(_strings));
            cache.AddConverter<IEntity>(new EntityConverter<IEntity>());
            cache.AddConverter<ICharacter>(new EntityConverter<ICharacter>());
            cache.AddConverter<IStronghold>(new EntityConverter<IStronghold>());
            cache.AddConverter<IObject>(new EntityConverter<IObject>());
            cache.AddConverter<IMission>(new EntityConverter<IMission>());
            cache.AddConverter<IItem>(new EntityConverter<IItem>());
            cache.AddConverter<IList<IEntity>>(new EntityListConverter<IList<IEntity>,IEntity>());
            cache.AddConverter<IReadOnlyList<IObject>>(new EntityListConverter<IReadOnlyList<IObject>,IObject>());
            
            cache.AddConverter<LordTraits>(new FlagConverter<LordTraits>());
            cache.AddConverter<EntityFlags>(new FlagConverter<EntityFlags>());
            cache.AddConverter<LordFlags>(new FlagConverter<LordFlags>());
            cache.AddConverter<RegimentFlags>(new FlagConverter<RegimentFlags>());
            cache.AddConverter<ThingFlags>(new FlagConverter<ThingFlags>());
            cache.AddConverter<MissionFlags>(new FlagConverter<MissionFlags>());
            cache.AddConverter<VictoryFlags>(new FlagConverter<VictoryFlags>());
            cache.AddConverter<TerrainInfoFlags>(new FlagConverter<TerrainInfoFlags>());
    }
}